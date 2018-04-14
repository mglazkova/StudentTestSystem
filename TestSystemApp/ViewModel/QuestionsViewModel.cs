using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TestSystemApp.Common;
using TestSystemApp.DAL;
using TestSystemApp.Model;
using TestSystemApp.View;

namespace TestSystemApp.ViewModel
{
    public class QuestionsViewModel:BaseViewModel
    {
        private int _currentQuestionIndex=0;
        private TimeSpan _time;
        private readonly DispatcherTimer _timer; 
        public QuestionsViewModel()
        {            
            _currentQuestionIndex = 0;                 

            //Время таймера
            _time = TimeSpan.FromMinutes(NavigationHelper.CurrrentTest.MinuteTimeLimit);
            RemainingTime = _time.ToString("c");

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                RemainingTime = _time.ToString("c");
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop();
                    ShowTestResult(true);

                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            _timer.Start();  
    

        }       

        private void ShowTestResult(bool timeIsUp=false)
        {
            var attemptId=ServiceDataSource.FinishTest(timeIsUp);

            var resultFormView = new ResultFormView();
            var resultFormViewModel = new ResultFormViewModel()
            {
                AttemptId = attemptId
            };
            resultFormViewModel.LoadData();
            resultFormView.DataContext = resultFormViewModel;

            NavigationHelper.NavigateTo(resultFormView);
        }

        private void LoadNextQuestion()
        {
            CurrentQuestion = QuestionArray[_currentQuestionIndex];
            if (_currentQuestionIndex == QuestionArray.Count() - 1)
            {
                IsLastQuestion = true;
            }
            _currentQuestionIndex++;
            CurrentQuestionNumber = _currentQuestionIndex;
        }

        private void CheckAnswersQuestion()
        {
            var correctAnswer = ServiceDataSource.SubmitQuestionAnswers(CurrentQuestion.Id, CurrentQuestion.Answers.Where(a => a.IsSelected).Select(a => a.Id).ToArray());
            if (correctAnswer)
            {
                RightAnswerCount++;                
            }
            else
            {
                WrongAnswerCount++;
            }
        }

        /// <summary>
        /// Метод для загрузки тестовых данных
        /// </summary>
        public override void LoadData()
        {
            var rnd = new Random(DateTime.Now.Second);
            var questionCollection = new List<QuestionModel>();
            var fillQuestions = ServiceDataSource.GetQuestion4Atempt();
            foreach(var dbQuestion in fillQuestions)
            {
                var questModel = new QuestionModel {Id=dbQuestion.Id, Name=dbQuestion.Content,Answers=new ObservableCollection<AnswerModel>()};
                //Перемешивааем варианты ответов в произвольном порядке                
                var result = dbQuestion.Answers.OrderBy(i=>Guid.NewGuid());
                foreach (var dbAnswer in result)
                {
                    questModel.Answers.Add(new AnswerModel { Name = dbAnswer.Content, Id = dbAnswer.Id});
                }
                questionCollection.Add(questModel);
            }   
            QuestionArray = questionCollection.ToArray();
            LoadNextQuestion();
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

        #region IsLastQuestionProperty
		private bool _isLastQuestion;
        public bool IsLastQuestion
        {
            get { return _isLastQuestion; }
            set
            {
                if (_isLastQuestion != value)
                {
                    _isLastQuestion = value;
                    RaisePropertyChanged("IsLastQuestion");
                }
            }
        }
		#endregion 

        #region QuestionColletionProperty
		private QuestionModel[] _questions;
        public QuestionModel[] QuestionArray
        {
            get { return _questions; }
            set
            {
                if (_questions != value)
                {
                    _questions = value;
                    RaisePropertyChanged("QuestionArray");
                }
            }
        }
		#endregion 

        #region CurrentQuestionProperty
		private QuestionModel _currentQuestionModel;
        public QuestionModel CurrentQuestion
        {
            get { return _currentQuestionModel; }
            set
            {
                if (_currentQuestionModel != value)
                {
                    _currentQuestionModel = value;
                    RaisePropertyChanged("CurrentQuestion");
                }
            }
        }
		#endregion 

        #region RemainingTimeProperty
		private string _remainingTime;
        public string RemainingTime
        {
            get { return _remainingTime; }
            set
            {
                if (_remainingTime != value)
                {
                    _remainingTime = value;
                    RaisePropertyChanged("RemainingTime");
                }
            }
        }
		#endregion 

        #region WrongAnswerCountProperty
		private int _wrongAnswerCount;
        public int WrongAnswerCount
        {
            get { return _wrongAnswerCount; }
            set
            {
                if (_wrongAnswerCount != value)
                {
                    _wrongAnswerCount = value;
                    RaisePropertyChanged("WrongAnswerCount");
                }
            }
        }
		#endregion 

        #region RightAnswerCountProperty
		private int _rightAnswerCount;
        public int RightAnswerCount
        {
            get { return _rightAnswerCount; }
            set
            {
                if (_rightAnswerCount != value)
                {
                    _rightAnswerCount = value;
                    RaisePropertyChanged("RightAnswerCount");
                }
            }
        }
		#endregion 

        #region CurrentQuestionNumberProperty
		private int _currentQuestionNumber;
        public int CurrentQuestionNumber
        {
            get { return _currentQuestionNumber; }
            set
            {
                if (_currentQuestionNumber != value)
                {
                    _currentQuestionNumber = value;
                    RaisePropertyChanged("CurrentQuestionNumber");
                }
            }
        }
		#endregion 

        #region SelectAnswerCommand

        private RelayCommand __selectAnswerCommand;
        public ICommand SelectAnswerCommand
        {
            get
            {
                return __selectAnswerCommand ?? (__selectAnswerCommand =
                    new RelayCommand(ExecuteSelectAnswerCommand, CanExecuteSelectAnswerCommand));
            }
        }

        private void ExecuteSelectAnswerCommand(Object parameter)
        {
            CheckAnswersQuestion();
            if (IsLastQuestion)
            {
                _timer.Stop();               
                ShowTestResult();
            }
            else
            {
                LoadNextQuestion();
            }
          
        }

        private bool CanExecuteSelectAnswerCommand(Object parameter)
        {
            return CurrentQuestion.Answers.Count(a => a.IsSelected)!=0;
        }

        #endregion 
    }
}
