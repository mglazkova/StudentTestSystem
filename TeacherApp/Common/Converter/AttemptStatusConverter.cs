using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using WCFServiceLibrary.Enum;

namespace TeacherApp.Common.Converter
{
    public class AttemptStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (TestAttemptStatus)value;
            switch (status)
            {
                case TestAttemptStatus.Aborted:
                    return "Попытка прервана некорректно";
                case TestAttemptStatus.Finished:
                    return "Тест завершен";
                case TestAttemptStatus.InProgress:
                    return "Тест в процессе";
            }
            return "Неизвестно";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
