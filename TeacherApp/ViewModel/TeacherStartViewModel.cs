using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeacherApp.Common;
using TeacherApp.View;

namespace TeacherApp.ViewModel
{
    public class TeacherStartViewModel : BaseViewModel
    {

        #region TestEditCommand

        private RelayCommand _testEditCommand;
        public ICommand TestEditCommand
        {
            get
            {
                return _testEditCommand ?? (_testEditCommand =
                    new RelayCommand(ExecuteTestEditCommand, CanExecuteTestEditCommand));
            }
        }

        private void ExecuteTestEditCommand(Object parameter)
        {
            var testEditVew = new TeacherFunctionsView();

            var vm = new TeacherFunctionsViewModel();
            vm.LoadData();
            testEditVew.DataContext = vm;

            NavigationHelper.NavigateTo(testEditVew);

        }

        private bool CanExecuteTestEditCommand(Object parameter)
        {
            return true;
        }

        #endregion 

        #region ResultViewCommand

        private RelayCommand _resultViewCommand;
        public ICommand ResultViewCommand
        {
            get
            {
                return _resultViewCommand ?? (_resultViewCommand =
                    new RelayCommand(ExecuteResultViewCommand, CanExecuteResultViewCommand));
            }
        }

        private void ExecuteResultViewCommand(Object parameter)
        {
            var resultView = new ResultView();

            var vm = new ResultViewModel();
            vm.LoadData();
            resultView.DataContext = vm;

            NavigationHelper.NavigateTo(resultView);          
        }

        private bool CanExecuteResultViewCommand(Object parameter)
        {
            return true;
        }

        #endregion 
    }
}
