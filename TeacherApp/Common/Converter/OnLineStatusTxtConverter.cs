using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace TeacherApp.Common.Converter
{
    public class OnLineStatusTxtConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;
            return (boolValue) ? "Сервис для тестирования доступен!" : "Сервис для тестирования НЕ доступен!";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
