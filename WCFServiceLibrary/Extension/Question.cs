using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFServiceLibrary
{
    public partial class Question
    {
        public Question()
        {
            Answers = new List<Answer>();
        }

        [DataMember]
        public List<Answer> Answers{get;set;}

        public int AnswersCount
        {
            get { return Answers.Count; }
        }

        public int RightAnswersCount
        {
            get { return Answers.Count(a=>a.IsRight); }
        }
    }
}
