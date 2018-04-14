using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TestSystemApp.TestServiceReference;

namespace TestSystemApp.Common.Converter
{
    public class Grade2TextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch((Grade)value)
            {
                case Grade.None:
                    return "Без оценки";                   
                case Grade.Poor:
                    return "Неудовлетворительно";                     
                case Grade.Satisfactory:
                    return "Удовлетворительно"; 
                case Grade.Good:
                    return "Хорошо"; 
                case Grade.Excellent:
                    return "Отлично"; 
            }
            return "Без оценки"; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
