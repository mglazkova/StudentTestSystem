using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestSystemApp.Common;
using TestSystemApp.DAL;
using TestSystemApp.View;

namespace TestSystemApp.ViewModel
{
    public class StartViewModel : BaseViewModel
    {
        public override void LoadData()
        {
            var test = ServiceDataSource.GetCurrentTest();
            if(test!=null)
            {
                NavigationHelper.CurrrentTest = test;
                TestDescription = test.Description;
            }
        }


        #region TestDescriptionProperty
        private string _testDescription;
        public string TestDescription
        {
            get { return _testDescription; }
            set
            {
                if (_testDescription != value)
                {
                    _testDescription = value;
                    RaisePropertyChanged("TestDescription");
                }
            }
        }
        #endregion 

        #region OkCommand

        private RelayCommand _stringCommand;
        public ICommand OkCommand
        {
            get
            {
                return _stringCommand ?? (_stringCommand =
                    new RelayCommand(ExecuteOkCommand, CanExecuteOkCommand));
            }
        }

        private void ExecuteOkCommand(Object parameter)
        {
            var regView = new RegistrationFormView();
            var regViewModel = new RegistrationViewModel();
            regView.DataContext = regViewModel;

            NavigationHelper.NavigateTo(regView);
        }

        private bool CanExecuteOkCommand(Object parameter)
        {
            return true;
        }

        #endregion 
    }
}
