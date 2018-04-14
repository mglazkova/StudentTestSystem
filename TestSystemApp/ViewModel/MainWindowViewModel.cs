using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TestSystemApp.ViewModel
{
    public class MainWindowViewModel:BaseViewModel
    {
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
    }
}
