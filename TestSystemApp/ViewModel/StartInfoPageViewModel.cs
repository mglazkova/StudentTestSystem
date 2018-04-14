using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestSystemApp.Common;
using TestSystemApp.DAL;
using TestSystemApp.Model;
using TestSystemApp.View;

namespace TestSystemApp.ViewModel
{
    public class StartInfoPageViewModel:BaseViewModel
    {
        public override void LoadData()
        {
            QuestionCount = NavigationHelper.CurrrentTest.QuestionCount;
            TimeLimit = new TimeSpan(0, NavigationHelper.CurrrentTest.MinuteTimeLimit, 0).ToString();
        }

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
                    RaisePropertyChanged("CurrentStudent");
                }
            }
        }
		#endregion 

        #region QuestionCountProperty
        private int _questionCount;
        public int QuestionCount
        {
            get { return _questionCount; }
            set
            {
                if (_questionCount != value)
                {
                    _questionCount = value;
                    RaisePropertyChanged("QuestionCount");
                }
            }
        }
        #endregion 

        #region TimeLimitProperty
        private string _timeLimit;
        public string TimeLimit
        {
            get { return _timeLimit; }
            set
            {
                if (_timeLimit != value)
                {
                    _timeLimit = value;
                    RaisePropertyChanged("TimeLimit");
                }
            }
        }
        #endregion 

        #region TestStartCommand

        private RelayCommand _testStartCommand;
        public ICommand TestStartCommand
        {
            get
            {
                return _testStartCommand ?? (_testStartCommand =
                    new RelayCommand(ExecuteTestStartCommand, CanExecuteTestStartCommand));
            }
        }

        private void ExecuteTestStartCommand(Object parameter)
        {
            NavigationHelper.TestSessionId = ServiceDataSource.StartTest(CurrentStudent.Id);
            if(NavigationHelper.TestSessionId!=Guid.Empty)
            {
                var questionsFormView = new QuestionsFormView();
                var questionsViewModel = new QuestionsViewModel() { CurrentStudent = CurrentStudent };
                questionsViewModel.LoadData();                
                questionsFormView.DataContext = questionsViewModel;

                NavigationHelper.NavigateTo(questionsFormView);
            }      
            else
            {
                MessageBox.Show("Ошибка запуска теста", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanExecuteTestStartCommand(Object parameter)
        {
            return true;
        }

        #endregion 


    }
}
