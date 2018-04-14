using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using WCFServiceLibrary.Enum;

namespace WCFServiceLibrary
{
     public partial class TestAttempt
    {
         
         [DataMember]
         public string StudentName { get; set; }
         [DataMember]
         public TestAttemptStatus Status { get; set; }
         [DataMember]
         public TimeSpan AttemptTime { get; set; }

         [DataMember]
         public int AllQuestionCount { get; set; }
         [DataMember]
         public int RightQuestionCount { get; set; }
         [DataMember]
         public double PersentResult { get; set; }

         [DataMember]
         public string GradeFix
         {
             get
             {
                 if (Grade != null)
                 {
                     return ((int)Grade + 1).ToString();
                 }
                 else
                 {
                     return string.Empty;
                 }
                 
             }
             set { var dummy = value; }
         }
    }
}
