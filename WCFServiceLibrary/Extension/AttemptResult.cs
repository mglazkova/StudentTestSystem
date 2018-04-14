using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WCFServiceLibrary
{
    public partial class AttemptResult
    {
        public AttemptResult()
        {
            ResultAnswers = new List<Answer>();
        }
        [DataMember]
        public List<Answer> ResultAnswers { get;set; }
    }
}
