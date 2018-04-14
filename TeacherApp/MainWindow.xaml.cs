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
using TeacherApp.Common;
using TeacherApp.Host;
using TeacherApp.View;
using TeacherApp.ViewModel;

namespace TeacherApp
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

            var startView = new TeacherStartView();
            var startViewModel = new TeacherStartViewModel();
            startViewModel.LoadData();
            startView.DataContext = startViewModel;

            mainVM.CurrentView = startView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Поднимаем сервис для работы модулей тестирования
                Hoster.StartHost();
                NavigationHelper.TestServiceOnline = true;
            }
            catch (Exception ex)
            {
                NavigationHelper.TestServiceOnline = false;
                MessageBox.Show("Ошибка при попытке инициализации сервиса для тестирования. Прохождение тестов недоступно!", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Автор программы: Глазкова Мария, ФРТ каф. РЭС, 2016 г.", "Об авторе", MessageBoxButton.OK);
        }
    }
}
