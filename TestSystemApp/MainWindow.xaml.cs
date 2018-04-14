using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestSystemApp.View;
using TestSystemApp.ViewModel;

namespace TestSystemApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var mainVM = new MainWindowViewModel();
            DataContext = mainVM;

            var startView = new StartView();
            var startViewModel = new StartViewModel();
            startViewModel.LoadData();
            startView.DataContext = startViewModel;

            mainVM.CurrentView = startView;
        }
    }
}
