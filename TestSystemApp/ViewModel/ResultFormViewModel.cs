using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestSystemApp.Common;
using TestSystemApp.Common.Converter;
using TestSystemApp.DAL;
using TestSystemApp.Model;
using TestSystemApp.TestServiceReference;

namespace TestSystemApp.ViewModel
{
    public class ResultFormViewModel:BaseViewModel
    {
        private const string CountResultFormat = "Правильных ответов {0} из {1}.";
        private const string PersentResultFormat = "{0}% правильных ответов.";
        private const string GradeResultFormat = "Оценка: {0}";

        public ResultFormViewModel()
        {           
        }

        public int AttemptId { get; set; }

        #region CurrentStudentProperty
        private StudentModel _currentStudent;
        public StudentModel CurrentStudent
        {
            get { return _currentStudent; }
            set
            {
                if (_currentStudent != value)
                {
                    _currentStudent = value;
                    StudentFullName = string.Format("{0} {1} - {2} группа", CurrentStudent.LastName,
                        CurrentStudent.FirstName, CurrentStudent.GroupNumber);
                    RaisePropertyChanged("CurrentStudent");
                }
            }
        }
        #endregion 

        #region TimeIsUpProperty
        private bool _timeIsUp;
        public bool TimeIsUp
        {
            get { return _timeIsUp; }
            set
            {
                if (_timeIsUp != value)
                {
                    _timeIsUp = value;
                    RaisePropertyChanged("TimeIsUp");
                }
            }
        }
        #endregion 

        #region AllQuestionCountProperty
		private int _allQuestionCount;
        public int AllQuestionCount
        {
            get { return _allQuestionCount; }
            set
            {
                if (_allQuestionCount != value)
                {
                    _allQuestionCount = value;
                    RaisePropertyChanged("AllQuestionCount");
                }
            }
        }
		#endregion 

        #region RightAnswerCountProperty
		private int _runAnswerCount;
        public int RightAnswerCount
        {
            get { return _runAnswerCount; }
            set
            {
                if (_runAnswerCount != value)
                {
                    _runAnswerCount = value;
                    RaisePropertyChanged("RightAnswerCount");
                }
            }
        }
		#endregion 

        #region CountResultMessageProperty
		private string _countResultMessage;
        public string CountResultMessage
        {
            get { return string.Format(CountResultFormat, RightAnswerCount, AllQuestionCount); }
            set
            {
                if (_countResultMessage != value)
                {
                    _countResultMessage = value;
                    RaisePropertyChanged("CountResultMessage");
                }
            }
        }
		#endregion 

        #region PersentResultMessageProperty
		private string _persentResultMessage;
        public string PersentResultMessage
        {
            get { return string.Format(PersentResultFormat, (AllQuestionCount != 0) ? Math.Round(((double)RightAnswerCount / (double)AllQuestionCount) * 100,2)  : 0); 
            }
            set
            {
                if (_persentResultMessage != value)
                {
                    _persentResultMessage = value;
                    RaisePropertyChanged("PersentResultMessage");
                }
            }
        }
		#endregion 

        #region GradeResultMessageProperty
		private string _gradeResultMessage;
        public string GradeResultMessage
        {
            get { return string.Format(GradeResultFormat, new Grade2TextConverter().Convert(Grade, null, null, null)); }
            set
            {
                if (_gradeResultMessage != value)
                {
                    _gradeResultMessage = value;
                    RaisePropertyChanged("GradeResultMessage");
                }
            }
        }
		#endregion 

        #region GradeProperty
        private Grade _grade;
        public Grade Grade
        {
            get { return _grade; }
            set
            {
                if (_grade != value)
                {
                    _grade = value;
                    RaisePropertyChanged("Grade");
                }
            }
        }
		#endregion 

        #region StudentFullNameProperty
		private string _studentFullName;
        public string StudentFullName
        {
            get { return _studentFullName; }
            set
            {
                if (_studentFullName != value)
                {
                    _studentFullName = value;
                    RaisePropertyChanged("StudentFullName");
                }
            }
        }
		#endregion 

        #region FinishTestCommand

        private RelayCommand _finishTestCommand;
        public ICommand FinishTestCommand
        {
            get
            {
                return _finishTestCommand ?? (_finishTestCommand =
                    new RelayCommand(ExecuteFinishTestCommand, CanExecuteFinishTestCommand));
            }
        }

        private void ExecuteFinishTestCommand(Object parameter)
        {
            App.Current.Shutdown();
        }

        private bool CanExecuteFinishTestCommand(Object parameter)
        {
            return true;
        }

        #endregion

        public override void LoadData()
        {
            if (AttemptId > 0)
            {
                var result = ServiceDataSource.GetStudentTestResult(AttemptId);
                if (result != null)
                {
                    if (result.Student != null)
                    {
                        CurrentStudent = new StudentModel() { FirstName = result.Student.Name, LastName = result.Student.LastName, GroupNumber = result.Student.GroupNumber };
                    }

                    AllQuestionCount = result.AllQuestionCount;
                    RightAnswerCount = result.RightQuestionCount;

                    Grade = result.Grade;
                    TimeIsUp = result.TimeIsUp;
                }
            }
        }
    }
}
