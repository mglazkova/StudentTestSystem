using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeacherApp.Common;
using TeacherApp.DAL;
using TeacherApp.Helper;
using TeacherApp.View;
using WCFServiceLibrary;

namespace TeacherApp.ViewModel
{
    public class TeacherFunctionsViewModel : BaseViewModel
    {
        #region SetTestParamCommand

        private RelayCommand _setTestParamCommand;
        public ICommand SetTestParamCommand
        {
            get
            {
                return _setTestParamCommand ?? (_setTestParamCommand =
                    new RelayCommand(ExecuteSetTestParamCommand, CanExecuteSetTestParamCommand));
            }
        }

        private void ExecuteSetTestParamCommand(Object parameter)
        {
            var testParamVew = new TeacherParametersTest();
            var vm = new TestParametersViewModel();
            vm.LoadData();
            testParamVew.DataContext = vm;
            NavigationHelper.NavigateTo(testParamVew);
        }

        private bool CanExecuteSetTestParamCommand(Object parameter)
        {
            return true;
        }

        #endregion 

        #region QuestionEditCommand

        private RelayCommand _questionEditCommand;
        public ICommand QuestionEditCommand
        {
            get
            {
                return _questionEditCommand ?? (_questionEditCommand =
                    new RelayCommand(ExecuteQuestionEditCommand, CanExecuteQuestionEditCommand));
            }
        }

        private void ExecuteQuestionEditCommand(Object parameter)
        {
            var testQuestionVew = new TeacherAddAndEditQuestions();
            var vm = new QuestionVewModel();
            vm.LoadData();
            testQuestionVew.DataContext = vm;
            NavigationHelper.NavigateTo(testQuestionVew);
        }

        private bool CanExecuteQuestionEditCommand(Object parameter)
        {
            return true;
        }

        #endregion

        #region BackCommand

        private RelayCommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand =
                    new RelayCommand(ExecuteBackCommand, CanExecuteBackCommand));
            }
        }

        private void ExecuteBackCommand(Object parameter)
        {
            var startView = new TeacherStartView();
            var startViewModel = new TeacherStartViewModel();
            startViewModel.LoadData();
            startView.DataContext = startViewModel;

            NavigationHelper.NavigateTo(startView);
        }

        private bool CanExecuteBackCommand(Object parameter)
        {
            return true;
        }

        #endregion 

        public override void LoadData()
        {
            NavigationHelper.IsBusy = true;
            BackgroundProcessFactory.RunAsync(null, (o, e) =>
            {
                e.Result = DBDataSource.GetCurrentTest();
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
                    var test = e.Result as TestInstance;
                    if (test != null)
                    {
                        NavigationHelper.CurrrentTest = test;
                    }
                }  
            });            
        }
    }
}
