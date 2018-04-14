using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TeacherApp.Common;
using TeacherApp.DAL;
using TeacherApp.Helper;
using TeacherApp.View;
using WCFServiceLibrary;

namespace TeacherApp.ViewModel
{
    public class QuestionVewModel:BaseViewModel
    {
        public QuestionVewModel()
        {
            QuestionCollection = new ObservableCollection<Question>();
        }

        public override void LoadData()
        {
            NavigationHelper.IsBusy = true;
            BackgroundProcessFactory.RunAsync(null, (o, e) =>
            {
                e.Result = DBDataSource.GetQuestions();
            },
            (o, e) =>
            {
                NavigationHelper.IsBusy = false;
                if (e.Error != null)
                {
                    return;
                }
                else
                {
                    var questions = e.Result as List<Question>;
                    if (questions != null)
                    {
                        QuestionCollection = new ObservableCollection<Question>(questions);
                    }
                }
            });           
        }

        #region QuestionCollectionProperty
		private ObservableCollection<Question> _questionCollection;
        public ObservableCollection<Question> QuestionCollection
        {
            get { return _questionCollection; }
            set
            {
                if (_questionCollection != value)
                {
                    _questionCollection = value;
                    RaisePropertyChanged("QuestionCollection");
                }
            }
        }
		#endregion 

        #region SelectedQuestionProperty
		private Question _string;
        public Question SelectedQuestion
        {
            get { return _string; }
            set
            {
                if (_string != value)
                {
                    _string = value;
                    RaisePropertyChanged("SelectedQuestion");
                }
            }
        }
		#endregion 

        #region EditQuestionCommand

        private RelayCommand _editQuestionCommand;
        public ICommand EditQuestionCommand
        {
            get
            {
                return _editQuestionCommand ?? (_editQuestionCommand =
                    new RelayCommand(ExecuteEditQuestionCommand, CanExecuteEditQuestionCommand));
            }
        }

        private void ExecuteEditQuestionCommand(Object parameter)
        {
            var view = new TecherFormAddAndEditQuestions();
            var vm = new AddEditQuestionViewModel();

            vm.Question = new Question()
            {
                Answers = new List<Answer>(SelectedQuestion.Answers),
                Id = SelectedQuestion.Id,
                Content = SelectedQuestion.Content,
                Created = SelectedQuestion.Created,
                Modifiyed = SelectedQuestion.Modifiyed,
                Image = SelectedQuestion.Image
            };
            vm.AnswerCollection = new ObservableCollection<Answer>(vm.Question.Answers);

            vm.Window = view;
            view.DataContext = vm;

            var result = view.ShowDialog();
            if (result.HasValue && result.Value)
            {
                vm.Question.Answers = new List<Answer>(vm.AnswerCollection);
                var editResult = DBDataSource.EditQuestion(vm.Question);
                if (editResult)
                {
                    QuestionCollection = new ObservableCollection<Question>(DBDataSource.GetQuestions());
                }
            }
        }

        private bool CanExecuteEditQuestionCommand(Object parameter)
        {
            return SelectedQuestion != null;
        }

        #endregion 

        #region DeleteQuestionCommand

        private RelayCommand _deleteQuestionCommand;
        public ICommand DeleteQuestionCommand
        {
            get
            {
                return _deleteQuestionCommand ?? (_deleteQuestionCommand =
                    new RelayCommand(ExecuteDeleteQuestionCommand, CanExecuteDeleteQuestionCommand));
            }
        }

        private void ExecuteDeleteQuestionCommand(Object parameter)
        {
            var result = MessageBox.Show("Действительно удалить вопрос?", "Удаление вопроса",
               MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var resultTry=DBDataSource.DeleteQuestion(SelectedQuestion);
                if (resultTry)
                {
                    QuestionCollection.Remove(SelectedQuestion);
                }
            }
        }

        private bool CanExecuteDeleteQuestionCommand(Object parameter)
        {
            return SelectedQuestion!=null;
        }

        #endregion 

        #region NewQuestionCommand

        private RelayCommand _newQuestionCommand;
        public ICommand NewQuestionCommand
        {
            get
            {
                return _newQuestionCommand ?? (_newQuestionCommand =
                    new RelayCommand(ExecuteNewQuestionCommand, CanExecuteNewQuestionCommand));
            }
        }

        private void ExecuteNewQuestionCommand(Object parameter)
        {
            var view = new TecherFormAddAndEditQuestions();
            var vm = new AddEditQuestionViewModel();
            vm.Question = new Question();
            vm.IsNewQuestion = true;
            vm.Window = view;
            view.DataContext = vm;

            var result = view.ShowDialog();
            if (result.HasValue && result.Value)
            {
                vm.Question.Answers = new List<Answer>(vm.AnswerCollection);
                var questionid = DBDataSource.AddNewQuestion(vm.Question, NavigationHelper.CurrrentTest.Id);
                if (questionid > 0)
                {
                    //Добавление вопроса в UI
                    vm.Question.Id = questionid;
                    QuestionCollection.Add(vm.Question);
                }
            }
        }

        private bool CanExecuteNewQuestionCommand(Object parameter)
        {
            return true;
        }

        #endregion 

        #region OkCommand

        private RelayCommand _okCommand;
        public ICommand OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand =
                    new RelayCommand(ExecuteOkCommand, CanExecuteOkCommand));
            }
        }

        private void ExecuteOkCommand(Object parameter)
        {
            var functionsView = new TeacherFunctionsView();

            var vm = new TeacherFunctionsViewModel();
            vm.LoadData();
            functionsView.DataContext = vm;

            NavigationHelper.NavigateTo(functionsView);
        }

        private bool CanExecuteOkCommand(Object parameter)
        {
            return true;
        }

        #endregion 
    }
}
