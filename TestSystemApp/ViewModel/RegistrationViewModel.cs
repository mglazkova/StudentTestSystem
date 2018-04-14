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
using TestSystemApp.TestServiceReference;
using TestSystemApp.View;

namespace TestSystemApp.ViewModel
{
    public class RegistrationViewModel:BaseViewModel
    {
        #region LastNameProperty
		private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    RaisePropertyChanged("LastName");
                }
            }
        }
		#endregion 

        #region FirstNameProperty
		private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    RaisePropertyChanged("FirstName");
                }
            }
        }
		#endregion 

        #region GroupNumberProperty
		private string _groupNumber;
        public string GroupNumber
        {
            get { return _groupNumber; }
            set
            {
                if (_groupNumber != value)
                {
                    _groupNumber = value;
                    RaisePropertyChanged("GroupNumber");
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
                    new RelayCommand(ExecuteOkCommandCommand, CanExecuteOkCommandCommand));
            }
        }

        private void ExecuteOkCommandCommand(Object parameter)
        {
            var studentId=ServiceDataSource.RigisterStudent(new Student { LastName = LastName, Name = FirstName, GroupNumber = GroupNumber });
            if(studentId>0)
            {
                var startInfoPageView = new StartInfoPageView();
                var startInfoPageViewModel = new StartInfoPageViewModel()
                {
                    CurrentStudent = new StudentModel
                    {
                        LastName = LastName,
                        FirstName = FirstName,
                        GroupNumber = GroupNumber,
                        Id = studentId
                    }
                };
                startInfoPageViewModel.LoadData();
                startInfoPageView.DataContext = startInfoPageViewModel;

                NavigationHelper.NavigateTo(startInfoPageView);
            }
            else
            {
                MessageBox.Show("Ошибка регистрации студента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }           
        }

        private bool CanExecuteOkCommandCommand(Object parameter)
        {
            return !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(GroupNumber);
        }

        #endregion 
    }
}
