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
using WCFServiceLibrary.Enum;

namespace TeacherApp.ViewModel
{
    public class TestParametersViewModel:BaseViewModel
    {
        private List<TestGradeLimit> _limits;

        public override void LoadData()
        {
            NavigationHelper.IsBusy = true;
            BackgroundProcessFactory.RunAsync(null, (o, e) =>
            {
                e.Result = DBDataSource.GetTestGradeLimit(NavigationHelper.CurrrentTest.Id);
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
                    _limits = e.Result as List<TestGradeLimit>;
                    if (_limits != null)
                    {
                        foreach (Grade grade in Enum.GetValues(typeof(Grade)))
                        {
                            var limit = _limits.FirstOrDefault(l => l.Grade == (int)grade);
                            if (limit != null)
                            {
                                switch (grade)
                                {
                                    case Grade.Poor:
                                        PoorMaxValue = limit.ToPer;
                                        break;
                                    case Grade.Satisfactory:
                                        SatisfyMinValue = limit.FromPer;
                                        SatisfyMaxValue = limit.ToPer;
                                        break;
                                    case Grade.Good:
                                        GoodMinValue = limit.FromPer;
                                        GoodMaxValue = limit.ToPer;
                                        break;
                                    case Grade.Excellent:
                                        ExcellentMinValue = limit.FromPer;
                                        ExcellentMaxValue = limit.ToPer;
                                        break;
                                }
                            }
                        }

                        MinuteLimitCount = NavigationHelper.CurrrentTest.MinuteTimeLimit;
                        QuestionCount = NavigationHelper.CurrrentTest.QuestionCount;
                    }
                }
            });
        }


        #region PoorMaxValueProperty
		private int _poorMaxValue;
        public int PoorMaxValue
        {
            get { return _poorMaxValue; }
            set
            {
                if (_poorMaxValue != value)
                {
                    _poorMaxValue = value;
                    RaisePropertyChanged("PoorMaxValue");
                }
            }
        }
		#endregion 

        #region SatisfyMinValueProperty
		private int _satisfyMinValue;
        public int SatisfyMinValue
        {
            get { return _satisfyMinValue; }
            set
            {
                if (_satisfyMinValue != value)
                {
                    _satisfyMinValue = value;
                    RaisePropertyChanged("SatisfyMinValue");
                }
            }
        }
		#endregion 

        #region SatisfyMaxValueProperty
		private int _satisfyMaxValue;
        public int SatisfyMaxValue
        {
            get { return _satisfyMaxValue; }
            set
            {
                if (_satisfyMaxValue != value)
                {
                    _satisfyMaxValue = value;
                    RaisePropertyChanged("SatisfyMaxValue");
                }
            }
        }
		#endregion 


        #region GoodMinValueProperty
		private int _goodMinValue;
        public int GoodMinValue
        {
            get { return _goodMinValue; }
            set
            {
                if (_goodMinValue != value)
                {
                    _goodMinValue = value;
                    RaisePropertyChanged("GoodMinValue");
                }
            }
        }
		#endregion 

        #region GoodMaxValueProperty
		private int _goodMaxValue;
        public int GoodMaxValue
        {
            get { return _goodMaxValue; }
            set
            {
                if (_goodMaxValue != value)
                {
                    _goodMaxValue = value;
                    RaisePropertyChanged("GoodMaxValue");
                }
            }
        }
		#endregion 

        #region ExcellentMinValueProperty
		private int _excellentMinValue;
        public int ExcellentMinValue
        {
            get { return _excellentMinValue; }
            set
            {
                if (_excellentMinValue != value)
                {
                    _excellentMinValue = value;
                    RaisePropertyChanged("ExcellentMinValue");
                }
            }
        }
		#endregion 

        #region ExcellentMaxValueProperty
		private int _excellentMaxValue;
        public int ExcellentMaxValue
        {
            get { return _excellentMaxValue; }
            set
            {
                if (_excellentMaxValue != value)
                {
                    _excellentMaxValue = value;
                    RaisePropertyChanged("ExcellentMaxValue");
                }
            }
        }
		#endregion 

        #region MinuteLimitCountProperty
		private int _minuteLimitCount;
        public int MinuteLimitCount
        {
            get { return _minuteLimitCount; }
            set
            {
                if (_minuteLimitCount != value)
                {
                    _minuteLimitCount = value;
                    RaisePropertyChanged("MinuteLimitCount");
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

        #region SaveCommand

        private RelayCommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand =
                    new RelayCommand(ExecuteSaveCommand, CanExecuteSaveCommand));
            }
        }

        private void ExecuteSaveCommand(Object parameter)
        {
            //КОД корректировки лимитов
            foreach (Grade grade in Enum.GetValues(typeof (Grade)))
            {
                var limit = _limits.FirstOrDefault(l => l.Grade == (int)grade);
                if (limit == null)
                {
                    limit=new TestGradeLimit() { TestId = NavigationHelper.CurrrentTest.Id };
                    limit.Grade = (int)grade;
                    _limits.Add(limit);
                }
                          
                switch (grade)
                {
                    case Grade.Poor:
                        limit.ToPer = PoorMaxValue;
                        break;
                    case Grade.Satisfactory:
                        limit.FromPer = SatisfyMinValue;
                        limit.ToPer = SatisfyMaxValue;
                        break;
                    case Grade.Good:
                        limit.FromPer = GoodMinValue;
                        limit.ToPer = GoodMaxValue;
                        break;
                    case Grade.Excellent:
                        limit.FromPer = ExcellentMinValue;
                        limit.ToPer = ExcellentMaxValue;
                        break;
                }
            }

            NavigationHelper.CurrrentTest.MinuteTimeLimit = MinuteLimitCount;
            NavigationHelper.CurrrentTest.QuestionCount = QuestionCount;

            //Работа с БД
            NavigationHelper.IsBusy = true;
            BackgroundProcessFactory.RunAsync(null, (o, e) =>
            {
                DBDataSource.SaveTestParam(NavigationHelper.CurrrentTest, _limits.ToArray());
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
                    var functionsView = new TeacherFunctionsView();

                    var vm = new TeacherFunctionsViewModel();
                    vm.LoadData();
                    functionsView.DataContext = vm;

                    NavigationHelper.NavigateTo(functionsView);
                }
            });
        }

        private bool CanExecuteSaveCommand(Object parameter)
        {
            return true;
        }

        #endregion 
    }
}
