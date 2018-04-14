using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeacherApp.ViewModel;
using WCFServiceLibrary;

namespace TeacherApp.Common
{
    public static class NavigationHelper
    {

        public static TestInstance CurrrentTest { get; set; }

        public static void NavigateTo(UIElement element)
        {
            var vm = App.Current.MainWindow.DataContext as MainWindowViewModel;
            if (vm != null)
            {
                vm.CurrentView = element;
            }
        }

        public static bool IsBusy
        {
            set
            {
                var vm = App.Current.MainWindow.DataContext as MainWindowViewModel;
                if (vm != null)
                {
                    vm.IsBusy = value;
                }
            }
        }

        public static bool TestServiceOnline
        {
            set
            {
                var vm = App.Current.MainWindow.DataContext as MainWindowViewModel;
                if (vm != null)
                {
                    vm.TestServiceOnline = value;
                }
            }
        }

    }
}
