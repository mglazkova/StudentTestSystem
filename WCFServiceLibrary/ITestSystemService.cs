
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
    [ServiceContract]
    public interface ITestSystemService
    {
        /// <summary>
        /// Возвращает тест, помеченный преподавателем, как текущий тест для прохождения
        /// </summary>
        /// <returns>Объект - экземпляр теста</returns>
        [OperationContract]
        TestInstance GetCurrentTest();

        /// <summary>
        /// Метод, регистрирующий нового студента, готовящегося проходить тест
        /// </summary>
        /// <param name="newStudent">Объект с заполненными полями по студенту</param>
        /// <returns>Идентификатор зарегистрированного студента в БД</returns>
        [OperationContract]
        int RigisterStudent(Student newStudent);

        /// <summary>
        /// Метод, запускающий старт выбранного теста для зарегистрированного студента
        /// </summary>
        /// <param name="testId">Идентификатор теста в БД</param>
        /// <param name="studentId">Идентификатор студента в БД</param>
        /// <returns></returns>
        [OperationContract]
        Guid StartTest(int testId, int studentId);

        /// <summary>
        /// Возвращает набор вопросов с вариантами ответов для попытки в количестве, соответствующем настройкам теста
        /// </summary>
        /// <param name="testId">Идентификатор теста</param>
        /// <param name="atemptKey">Идентификатор попытки прохождения теста</param>
        /// <returns></returns>
        [OperationContract]
        List<Question> GetQuestion4Atempt(int testId, Guid atemptKey);        

       /// <summary>
       /// Метод, фиксирующий факт завершения теста
       /// </summary>
        /// <param name="atemptKey">Идентификатор попытки прохождения теста</param>
       /// <param name="timeIsUp">Отметка о том, что время отведенное на тест истекло</param>      
        [OperationContract]
        int FinishTest(Guid atemptKey, bool timeIsUp);        

        /// <summary>
        /// Метод, отправляющий на сервер выбранный варианты ответов по вопросу. Возвращает результат  - верный ответ или нет
        /// </summary>
        /// <param name="atemptKey">Идентификатор попытки прохождения теста</param>
        /// <param name="questionId">Идентификатор вопроса</param>
        /// <param name="anwersIds">Набор идентификаторов выбранных отвеетов</param>
        /// <returns>Отметка о верном ответе на вопрос или не верным</returns>
        [OperationContract]
        bool SubmitQuestionAnswers(Guid atemptKey,int questionId,int[] anwersIds);

        /// <summary>
        /// Метод, возвращающий сводный результат по попытке тестирования
        /// </summary>
        /// <param name="attemptId">Идентификатор попытки прохождения теста</param>
        /// <returns>Объект, консолидирующий результаты прохождения теста</returns>
        [OperationContract]
        StudentTestResult GetStudentTestResult(int attemptId);
    }
}
