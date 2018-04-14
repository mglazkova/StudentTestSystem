using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WCFServiceLibrary;
using WCFServiceLibrary.Enum;

namespace TeacherApp.DAL
{
    public static class DBDataSource
    {
        public static List<TestAttempt> GeTestAttempts(DateTime? timeFrom=null, DateTime? timeTo=null)
        {
            try
            {
                var attempts = new List<TestAttempt>();
                using (var ctx = new TestStudentDBEntities())
                {
                    var data = (from t in ctx.TestAttempts
                        join s in ctx.Students on t.StudentId equals s.Id
                    select new
                    {
                        Attept = t,s.LastName,s.Name,s.GroupNumber
                    }).ToArray();


                    foreach (var d  in data)
                    {
                        var at = d.Attept;
                        at.StudentName = string.Format("{0} {1} - {2} группа",d.LastName,d.Name,d.GroupNumber);

                        if (!at.IsTimeIsUp && at.FinishTime!=null)
                        {
                            at.AttemptTime = (at.FinishTime - at.StartTime).Value;
                        }

                        //Выясняем статус попытки
                        if (at.FinishTime != null)
                        {
                            at.Status = TestAttemptStatus.Finished;
                        }
                        else
                        {
                            var diffMin = (DateTime.Now - at.StartTime).TotalMinutes;
                            var timeLimitMin =ctx.TestInstances.Where(t => t.Id == at.TestId).Select(t => t.MinuteTimeLimit).FirstOrDefault();
                            at.Status = diffMin > timeLimitMin ? TestAttemptStatus.Aborted : TestAttemptStatus.InProgress;
                        }


                        at.AllQuestionCount = ctx.AttemptResults.Count(r => r.AttemptId == at.Id);
                        at.RightQuestionCount = ctx.AttemptResults.Count(r => r.AttemptId == at.Id && r.IsCorrect);

                        at.PersentResult = (at.AllQuestionCount != 0) ? ((double)at.RightQuestionCount / (double)at.AllQuestionCount) * 100 : 0;

                        
                        attempts.Add(at);
                    }
                }
                return attempts;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении результатов тестирования", "Ошибка работы с БД", MessageBoxButton.OK,MessageBoxImage.Error);
                return new List<TestAttempt>();
            }
        }

        public static List<Question> GetQuestions()
        {
            try
            {
                var questions = new List<Question>();
                using (var ctx = new TestStudentDBEntities())
                {
                    var answersDict = ctx.Answers.Where(a=>!a.IsDeleted).GroupBy(a => a.QuestionId).ToDictionary(a => a.Key, a => a);

                    foreach (var dbQuest in ctx.Questions.Where(q=>!q.IsDeleted))
                    {
                        if (answersDict.ContainsKey(dbQuest.Id))
                        {
                            dbQuest.Answers = new List<Answer>();
                            foreach (var answ in answersDict[dbQuest.Id])
                            {
                                //Специально стираем отметку о правильности ответа, отправляя его на клиента
                                dbQuest.Answers.Add(answ);
                            }
                            questions.Add(dbQuest);
                        }
                    }
                }
                return questions;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении вопросов", "Ошибка работы с БД", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                return new List<Question>();
            }
        }

        public static TestInstance GetCurrentTest()
        {
            try
            {
                using (var ctx = new TestStudentDBEntities())
                {
                    return ctx.TestInstances.FirstOrDefault(t => t.IsCurrent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении текущего теста", "Ошибка работы с БД", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                return null;
            }
        }

        public static int AddNewQuestion(Question question,int testId=0)
        {
            try
            {
                using (var ctx = new TestStudentDBEntities())
                {
                    question.Created = DateTime.Now;
                    ctx.Questions.Add(question);
                    ctx.SaveChanges();

                    if (question.Id > 0)
                    {
                        foreach (var answer in question.Answers)
                        {
                            answer.QuestionId = question.Id;
                            answer.Created = DateTime.Now;
                            ctx.Answers.Add(answer);
                            ctx.SaveChanges();
                        }

                        if (testId > 0)
                        {
                            var question2Test = new Question2Test() { QuestionId = question.Id, TestId = testId };
                            ctx.Question2Test.Add(question2Test);
                            ctx.SaveChanges();
                        }

                        return question.Id;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении вопроса в БД", "Ошибка работы с БД", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return 0;
            }
        }

        public static bool EditQuestion(Question question)
        {
            try
            {
                using (var ctx = new TestStudentDBEntities())
                {
                    question.Modifiyed = DateTime.Now;
                    //Редактирование самого вопроса
                    ctx.Entry(question).State=EntityState.Modified;
                    ctx.SaveChanges();

                    //Удаление из БД удалённых вариантов
                    var currentIds = question.Answers.Where(a => a.Id > 0).Select(a => a.Id);
                    var dbIds = ctx.Answers.Where(a => a.QuestionId == question.Id && !currentIds.Contains(a.Id)).ToArray();
                    foreach (var deletedAns in dbIds)
                    {
                        deletedAns.IsDeleted = true;
                        ctx.SaveChanges();
                    }


                    //Редактирование или добавление вариантов ответов
                    foreach (var answer in question.Answers)
                    {
                        if (answer.Id > 0)
                        {
                            ctx.Entry(answer).State = EntityState.Modified;
                        }
                        else
                        {
                            answer.Created = DateTime.Now;
                            ctx.Answers.Add(answer);
                        }
                        ctx.SaveChanges();
                    }
                    

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при редактировании вопроса", "Ошибка работы с БД", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                return false;
            }
        }

        public static bool DeleteQuestion(Question question)
        {
            try
            {
                using (var ctx = new TestStudentDBEntities())
                {
                    //Удаляем сначала ответы
                    foreach (var answer in question.Answers)
                    {
                        answer.IsDeleted = true;
                    }

                    question.IsDeleted = true;
                    EditQuestion(question);

                    //потом если всё ок, то привязку к тесту
                    var quest2TestRelation = ctx.Question2Test.Where(q => q.QuestionId == question.Id).ToArray();
                    foreach (var question2Test in quest2TestRelation)
                    {
                        ctx.Question2Test.Remove(question2Test);
                        ctx.SaveChanges();
                    }

                    

                    //И наконец удаляем сам вопрос
                    //ctx.Questions.Attach(question);
                    //ctx.Questions.Remove(question);
                    //ctx.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении вопросов", "Ошибка работы с БД", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                return false;
            }
        }

        public static List<TestGradeLimit> GetTestGradeLimit(int testId)
        {
            try
            {
                using (var ctx = new TestStudentDBEntities())
                {
                    return ctx.TestGradeLimits.Where(t => t.TestId == testId).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении шкалы оценок", "Ошибка работы с БД", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                return new List<TestGradeLimit>();
            }
        }

        public static void SaveTestParam(TestInstance test, TestGradeLimit[] limits)
        {
            try
            {
                using (var ctx = new TestStudentDBEntities())
                {
                    foreach (var limt in limits)
                    {
                        ctx.Entry(limt).State = limt.Id!=0? EntityState.Modified:EntityState.Added;
                        ctx.SaveChanges();
                    }

                    ctx.Entry(test).State=EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения параметров теста", "Ошибка работы с БД", MessageBoxButton.OK,MessageBoxImage.Error);
            }

        }
    }
}
