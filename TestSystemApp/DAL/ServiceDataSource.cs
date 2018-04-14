using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystemApp.Common;
using TestSystemApp.TestServiceReference;

namespace TestSystemApp.DAL
{
    public static class ServiceDataSource
    {
        public static int RigisterStudent(Student newStudent)
        {
            using(var service = new TestSystemServiceClient())
            {
                return service.RigisterStudent(newStudent);
            }
        }

        public static TestInstance GetCurrentTest()
        {
            using (var service = new TestSystemServiceClient())
            {
                return service.GetCurrentTest();
            }
        }

        public static Guid StartTest(int studentId)
        {
            using (var service = new TestSystemServiceClient())
            {
                return service.StartTest(NavigationHelper.CurrentTestId,studentId);
            }
        }

        public static Question[] GetQuestion4Atempt()
        {
            using (var service = new TestSystemServiceClient())
            {
                return service.GetQuestion4Atempt(NavigationHelper.CurrentTestId, NavigationHelper.TestSessionId);
            }
        }

        public static bool SubmitQuestionAnswers(int questionId, int[] anwersIds)
        {
            using (var service = new TestSystemServiceClient())
            {
                return service.SubmitQuestionAnswers(NavigationHelper.TestSessionId, questionId, anwersIds);
            }
        }

        public static int FinishTest(bool timeIsUp=false)
        {
            using (var service = new TestSystemServiceClient())
            {
                return service.FinishTest(NavigationHelper.TestSessionId, timeIsUp);
            }
        }

        public static StudentTestResult GetStudentTestResult(int attemptId)
        {
            using (var service = new TestSystemServiceClient())
            {
                return service.GetStudentTestResult(attemptId);
            }
        }
    }
}
