﻿using System;
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

namespace TestSystemApp.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationFormView.xaml
    /// </summary>
    public partial class RegistrationFormView : UserControl
    {
        public RegistrationFormView()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
