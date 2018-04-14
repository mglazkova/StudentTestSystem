using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemApp.Model
{
    public class QuestionModel : BaseModel
    {
        public QuestionModel()
        {
            Answers = new ObservableCollection<AnswerModel>();
        }

        #region AnswersProperty
        private ObservableCollection<AnswerModel> _answers;
        public ObservableCollection<AnswerModel> Answers
        {
            get { return _answers; }
            set
            {
                if (_answers != value)
                {
                    _answers = value;
                    RaisePropertyChanged("Answers");
                }
            }
        }
		#endregion 

        #region NameProperty
		private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
		#endregion 

        public int Id { get; set; }
    }
}
