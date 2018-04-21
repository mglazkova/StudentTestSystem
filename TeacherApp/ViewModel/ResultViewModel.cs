using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using TeacherApp.Common;
using TeacherApp.DAL;
using TeacherApp.Helper;
using WCFServiceLibrary;
using WCFServiceLibrary.Enum;

namespace TeacherApp.ViewModel
{
    public class ResultViewModel : BaseViewModel
    {
        private ICollectionView _cv;

        public ResultViewModel()
        {
            StatusFilterCollection = new ObservableCollection<StatusFilterItem>()
            {
                new StatusFilterItem(){Name = "Все"},
                new StatusFilterItem(){Name = "Попытка прервана",Status = TestAttemptStatus.Aborted},
                new StatusFilterItem(){Name = "Тест завершен",Status = TestAttemptStatus.Finished},
                new StatusFilterItem(){Name = "Тест в процессе",Status = TestAttemptStatus.InProgress},
                
            };

            SelectedFilter = StatusFilterCollection.FirstOrDefault();
        }

        #region ResultListProperty
        private ObservableCollection<TestAttempt> _resultList;
        public ObservableCollection<TestAttempt> ResultList
        {
            get { return _resultList; }
            set
            {
                if (_resultList != value)
                {
                    _resultList = value;
                    RaisePropertyChanged("ResultList");
                }
            }
        }
		#endregion 

        #region StatusFilterCollectionProperty
		private ObservableCollection<StatusFilterItem> _statusFilterItems;
        public ObservableCollection<StatusFilterItem> StatusFilterCollection
        {
            get { return _statusFilterItems; }
            set
            {
                if (_statusFilterItems != value)
                {
                    _statusFilterItems = value;
                    RaisePropertyChanged("StatusFilterCollection");
                }
            }
        }
		#endregion 

        #region SelectedFilterProperty
		private StatusFilterItem _selectedFilterItem;
        public StatusFilterItem SelectedFilter
        {
            get { return _selectedFilterItem; }
            set
            {
                if (_selectedFilterItem != value)
                {
                    _selectedFilterItem = value;
                    if (_cv != null)
                    {
                        _cv.Refresh();
                    }
                    
                    RaisePropertyChanged("SelectedFilter");
                }
            }
        }
		#endregion 


        #region SearchStringProperty
		private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set
            {
                if (_searchString != value)
                {
                    _searchString = value;
                    RaisePropertyChanged("SearchString");
                }
            }
        }
		#endregion 


        #region RefreshCommand

        private RelayCommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand =
                    new RelayCommand(ExecuteRefreshCommand, CanExecuteRefreshCommand));
            }
        }

        private void ExecuteRefreshCommand(Object parameter)
        {
            LoadData();
        }

        private bool CanExecuteRefreshCommand(Object parameter)
        {
            return true;
        }

        #endregion

        #region StudentSearchCommand

        private RelayCommand _studentSearchCommand;
        public ICommand StudentSearchCommand
        {
            get
            {
                return _studentSearchCommand ?? (_studentSearchCommand =
                    new RelayCommand(ExecuteStudentSearchCommand, CanExecuteStudentSearchCommand));
            }
        }

        private void ExecuteStudentSearchCommand(Object parameter)
        {
            var cv = CollectionViewSource.GetDefaultView(ResultList);
            cv.Filter = SearchValid;
            cv.Refresh();
        }

        private bool CanExecuteStudentSearchCommand(Object parameter)
        {
            return !String.IsNullOrEmpty(SearchString);
        }

        #endregion 

        public override void LoadData()
        {
            NavigationHelper.IsBusy = true;
            BackgroundProcessFactory.RunAsync(null, (o, e) =>
            {
                e.Result = DBDataSource.GeTestAttempts();
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
                    var attemts = e.Result as List<TestAttempt>;
                    if (attemts != null)
                    {
                        ResultList = new ObservableCollection<TestAttempt>(attemts);

                        _cv = CollectionViewSource.GetDefaultView(ResultList);
                        _cv.Filter = FilterValid;
                        _cv.Refresh();
                    }
                }
            });        
        }

        public bool FilterValid(object elemet)
        {
            var attempt = elemet as TestAttempt;
            if (SelectedFilter.Status == null)
            {
                return true;
            }
            return attempt.Status == SelectedFilter.Status;
        }

        public bool SearchValid(object elemet)
        {
            var attempt = elemet as TestAttempt;
            return attempt.StudentName.ToLower().Contains(SearchString.ToLower());
        }
    }
}
