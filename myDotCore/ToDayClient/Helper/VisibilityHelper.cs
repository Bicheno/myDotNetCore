using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ToDayClient.Helper
{
    public class VisibilityHelper : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            if (parameter != null)
            {
                switch (parameter.ToString())
                {
                    case "Enabled":
                        return (bool)value ? Visibility.Visible : Visibility.Collapsed;
                    case "Disabled":
                        return (bool)value ? Visibility.Visible : Visibility.Collapsed;
                        //case "Invaild":
                        //    return ((int)value == 2) ? Visibility.Collapsed : Visibility.Visible;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
