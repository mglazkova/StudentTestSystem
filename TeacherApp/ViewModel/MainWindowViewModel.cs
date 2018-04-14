using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace TeacherApp.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly DispatcherTimer _timer; 

        public MainWindowViewModel()
        {
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 10), DispatcherPriority.Normal, delegate
            {
                TestServiceOnline = true;
            }, Application.Current.Dispatcher);
            _timer.Start(); 
        }

        #region CurrentViewProperty
        private UIElement _currentView;
        public UIElement CurrentView
        {
            get { return _currentView; }
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    RaisePropertyChanged("CurrentView");
                }
            }
        }
        #endregion

        #region IsBusyProperty

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    RaisePropertyChanged("IsBusy");
                }
            }
        }
        #endregion

        #region TestServiceOnlineProperty

        private bool _testServiceOnline;
        public bool TestServiceOnline
        {
            get { return _testServiceOnline; }
            set
            {
                if (_testServiceOnline != value)
                {
                    _testServiceOnline = value;
                    RaisePropertyChanged("TestServiceOnline");
                }
            }
        }
        #endregion

           
    }
}
