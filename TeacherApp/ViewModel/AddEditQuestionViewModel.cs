using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TeacherApp.Common;
using WCFServiceLibrary;

namespace TeacherApp.ViewModel
{
    public class AddEditQuestionViewModel:BaseViewModel
    {
        public AddEditQuestionViewModel()
        {
            AnswerCollection = new ObservableCollection<Answer>();
        }

        public Window Window { get; set; }

        #region OperationResultProperty
		private bool _operationResult;
        public bool OperationResult
        {
            get { return _operationResult; }
            set
            {
                if (_operationResult != value)
                {
                    _operationResult = value;
                    RaisePropertyChanged("OperationResult");
                }
            }
        }
		#endregion 

        #region IsNewQuestionProperty
		private bool _isNewQuestion;
        public bool IsNewQuestion
        {
            get { return _isNewQuestion; }
            set
            {
                if (_isNewQuestion != value)
                {
                    _isNewQuestion = value;
                    RaisePropertyChanged("IsNewQuestion");
                }
            }
        }
		#endregion 

        #region QuestionProperty
		private Question _question;
        public Question Question
        {
            get { return _question; }
            set
            {
                if (_question != value)
                {
                    _question = value;
                    RaisePropertyChanged("Question");
                }
            }
        }
		#endregion 

        #region AnswerCollectionProperty
		private ObservableCollection<Answer> _answerCollection;
        public ObservableCollection<Answer> AnswerCollection
        {
            get { return _answerCollection; }
            set
            {
                if (_answerCollection != value)
                {
                    _answerCollection = value;
                    RaisePropertyChanged("AnswerCollection");
                }
            }
        }
		#endregion 

        #region SelectedAnswerProperty
		private Answer _selectedAnswer;
        public Answer SelectedAnswer
        {
            get { return _selectedAnswer; }
            set
            {
                if (_selectedAnswer != value)
                {
                    _selectedAnswer = value;
                    RaisePropertyChanged("SelectedAnswer");
                }
            }
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
            if (AnswerCollection.Count() == 0)
            {
                MessageBox.Show("Вы не можете добавить вопрос без вариантов ответа", "Нет вариантов ответа",
                    MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (AnswerCollection.Count(a => a.IsRight) == 0)
            {
                MessageBox.Show("Отметьте хотя бы один вариант верным", "Нет правильного ответа",
                    MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if(string.IsNullOrEmpty(Question.Content))
            {
                MessageBox.Show("Вы не можете оставить поле вопроса пустым", "Отсутствует текст вопроса",
                   MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else
            {
                Window.DialogResult = true;
                Window.Close();
            }
        }

        private bool CanExecuteOkCommand(Object parameter)
        {
            return true;
        }

        #endregion 

        #region CancelCommand

        private RelayCommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand =
                    new RelayCommand(ExecuteCancelCommand, CanExecuteCancelCommand));
            }
        }

        private void ExecuteCancelCommand(Object parameter)
        {
            Window.DialogResult = false;
            Window.Close();
        }

        private bool CanExecuteCancelCommand(Object parameter)
        {
            return true;
        }

        #endregion 

        #region AddAnswerCommand

        private RelayCommand _addAnswerCommand;
        public ICommand AddAnswerCommand
        {
            get
            {
                return _addAnswerCommand ?? (_addAnswerCommand =
                    new RelayCommand(ExecuteAddAnswerCommand, CanExecuteAddAnswerCommand));
            }
        }

        private void ExecuteAddAnswerCommand(Object parameter)
        {
            AnswerCollection.Add(new Answer(){Content = "Впишите вариант ответа",QuestionId = Question.Id});
        }

        private bool CanExecuteAddAnswerCommand(Object parameter)
        {
            return true;
        }

        #endregion 

        #region DeleteCommand

        private RelayCommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand =
                    new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand));
            }
        }

        private void ExecuteDeleteCommand(Object parameter)
        {
            var result = MessageBox.Show("Действительно удалить вариант ответа?", "Удаление ответа",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result==MessageBoxResult.Yes)
            {
                AnswerCollection.Remove(SelectedAnswer);
            }
            
        }

        private bool CanExecuteDeleteCommand(Object parameter)
        {
            return SelectedAnswer!=null;
        }

        #endregion 



    }
}
