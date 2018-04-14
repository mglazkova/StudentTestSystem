
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFServiceLibrary.DataContract;
using WCFServiceLibrary.Enum;

namespace WCFServiceLibrary
{
    public class TestSystemService : ITestSystemService
    {
        public TestInstance GetCurrentTest()
        {
             using(var ctx = new TestStudentDBEntities())
             {
                 return ctx.TestInstances.FirstOrDefault(t => t.IsCurrent);
             }
        }

        public int RigisterStudent(Student newStudent)
        {
            using(var ctx = new TestStudentDBEntities())
            {
                ctx.Students.Add(newStudent);
                ctx.SaveChanges();
                return newStudent.Id;                
            }
        }

        public Guid StartTest(int testId, int studentId)
        {
            using(var ctx = new TestStudentDBEntities())
            {
                var attempt = new TestAttempt
                {
                    StudentId = studentId,
                    TestId = testId,
                    StartTime = DateTime.Now,
                    AttemptKey = Guid.NewGuid(),                   
                };
                ctx.TestAttempts.Add(attempt);
                ctx.SaveChanges();

                if (attempt.Id > 0)
                {
                    return attempt.AttemptKey;
                }               
                return Guid.NewGuid();
            }
        }

        public List<Question> GetQuestion4Atempt(int testId, Guid atemptKey)
        {
            var questions = new List<Question>();
            using (var ctx = new TestStudentDBEntities())
            {
                var attempt = ctx.TestAttempts.FirstOrDefault(a => a.AttemptKey == atemptKey);
                if(attempt!=null)
                {
                    var test = ctx.TestInstances.FirstOrDefault(t => t.Id == testId);
                    if(test!=null)
                    {
                        var questionCount = test.QuestionCount;                     
                        var questionIds = ctx.Question2Test.Where(qt => qt.TestId == testId).OrderBy(qt => Guid.NewGuid()).Take(questionCount).Select(qt=>qt.QuestionId).ToArray();
                        var answersDict = ctx.Answers.Where(a => questionIds.Contains(a.QuestionId) && !a.IsDeleted).GroupBy(a => a.QuestionId).ToDictionary(a => a.Key, a => a);

                        foreach (var dbQuest in ctx.Questions.Where(q => questionIds.Contains(q.Id) && !q.IsDeleted))
                        {
                            if (answersDict.ContainsKey(dbQuest.Id))
                            {
                                dbQuest.Answers = new List<Answer>();
                                foreach(var answ in answersDict[dbQuest.Id])
                                {
                                    //Специально стираем отметку о правильности ответа, отправляя его на клиента
                                    answ.IsRight = false;
                                    dbQuest.Answers.Add(answ);
                                }                               
                                questions.Add(dbQuest);
                            }                           
                        }
                    }
                }
            }
            return questions;
        }

        public int FinishTest(Guid atemptKey, bool timeIsUp)
        {
            using (var ctx = new TestStudentDBEntities())
            {
                var attempt = ctx.TestAttempts.FirstOrDefault(a => a.AttemptKey == atemptKey);
                if(attempt!=null)
                {
                    attempt.FinishTime = DateTime.Now;
                    attempt.IsTimeIsUp = timeIsUp;
                    attempt.Grade = (int)GetGradeForAttempt(attempt.Id);
                    ctx.SaveChanges();
                    return attempt.Id;
                }
            }
            return 0;
        }


        public bool SubmitQuestionAnswers(Guid atemptKey, int questionId, int[] answersIds)
        {
            var isCorrect = false;
            using (var ctx = new TestStudentDBEntities())
            {
                var attempt = ctx.TestAttempts.FirstOrDefault(a => a.AttemptKey == atemptKey);
                if (attempt != null)
                {
                    var correctAnswIds = ctx.Answers.Where(a => a.QuestionId == questionId && a.IsRight).Select(a => a.Id).ToArray();
                    isCorrect = correctAnswIds.OrderBy(i => i).SequenceEqual(answersIds.OrderBy(i => i));

                    var questionResult = new AttemptResult { QuestionId = questionId, AttemptId = attempt.Id, IsCorrect = isCorrect,AnswerTime=DateTime.Now };
                    ctx.AttemptResults.Add(questionResult);
                    ctx.SaveChanges();

                    if(questionResult.Id>0)
                    {
                        foreach (var attemptAnswer in answersIds)
                       {
                           var attemptResultAnswer = new AttemptResultAnswer { AnswerId = attemptAnswer, ResultId = questionResult.Id };
                           ctx.AttemptResultAnswers.Add(attemptResultAnswer);
                           ctx.SaveChanges();
                       }
                    }
                }
            }            
            return isCorrect;
        }

        private Grade GetGradeForAttempt(int attemptId)
        {
            var grade = Grade.None;
            using (var ctx = new TestStudentDBEntities())
            {
                var attempt = ctx.TestAttempts.FirstOrDefault(a => a.Id == attemptId);
                if (attempt != null)
                {
                     var testId = attempt.TestId;                    

                    var allQuestionCount = ctx.TestInstances.Where(t => t.Id == testId).Select(t => t.QuestionCount).FirstOrDefault();
                    var correctAnswerCount = ctx.AttemptResults.Count(r => r.AttemptId == attempt.Id && r.IsCorrect);

                    var persentResult = (allQuestionCount != 0) ? ((double)correctAnswerCount / (double)allQuestionCount) * 100 : 0;
                   
                    var gradeLimits = ctx.TestGradeLimits.Where(t => t.TestId == testId).ToArray();
                    foreach (var limit in gradeLimits)
                    {
                        if(persentResult>=limit.FromPer && persentResult<=limit.ToPer)
                        {
                            grade = (Grade)limit.Grade;
                            break;
                        }
                    }

                    return grade;
                }
            }
            return grade;
        }

        public StudentTestResult GetStudentTestResult(int attemptId)
        {
            var obj = new StudentTestResult();
            using (var ctx = new TestStudentDBEntities())
            {
                var attempt = ctx.TestAttempts.FirstOrDefault(a => a.Id == attemptId);
                if (attempt != null)
                {
                    obj.Student = ctx.Students.FirstOrDefault(s => s.Id == attempt.StudentId);

                    var testId = attempt.TestId;     
                    var allQuestionCount = ctx.TestInstances.Where(t => t.Id == testId).Select(t => t.QuestionCount).FirstOrDefault();
                    var correctAnswerCount = ctx.AttemptResults.Count(r => r.AttemptId == attempt.Id && r.IsCorrect);

                    obj.AllQuestionCount = allQuestionCount;
                    obj.RightQuestionCount = correctAnswerCount;

                    obj.PersentResult = (allQuestionCount != 0) ? ((double)correctAnswerCount / (double)allQuestionCount) * 100 : 0;

                    obj.Grade = attempt.Grade!=null?(Grade)attempt.Grade:Grade.None;
                    obj.TimeIsUp = attempt.IsTimeIsUp;

                }

            }
            return obj;
        }

    }
}
