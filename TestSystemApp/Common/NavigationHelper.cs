using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestSystemApp.TestServiceReference;
using TestSystemApp.ViewModel;

namespace TestSystemApp.Common
{
    public static class NavigationHelper
    {
        public static int CurrentTestId { get { return CurrrentTest.Id; } }
        public static Guid TestSessionId { get; set; }

        public static TestInstance CurrrentTest { get; set;}

        public static void NavigateTo(UIElement element)
        {
            var vm = App.Current.MainWindow.DataContext as MainWindowViewModel;
            if (vm != null)
            {
                vm.CurrentView = element;
            }
        }
    }
}
