using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using WCFServiceLibrary.Enum;

namespace WCFServiceLibrary.DataContract
{
    [DataContract]
    public class StudentTestResult
    {
        [DataMember]
        public Student Student { get; set; }
        [DataMember]
        public bool TimeIsUp { get; set; }
        [DataMember]
        public int AllQuestionCount { get; set; }
        [DataMember]
        public int RightQuestionCount { get; set; }
        [DataMember]
        public Grade Grade { get; set; }
        [DataMember]
        public double PersentResult { get; set; }
    }
}
