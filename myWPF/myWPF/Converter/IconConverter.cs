using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace myWPF.Converter
{
    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                switch (value.ToString())
                {
                    case "1": return "M631.200846 64.98813 64.989153 631.202892l298.006208 327.807954 596.015486-566.214762L959.010847 64.98813 631.200846 64.98813zM765.305123 362.996384c-57.604976 0-104.302531-46.697555-104.302531-104.302531 0-57.604976 46.697555-104.302531 104.302531-104.302531s104.302531 46.697555 104.302531 104.302531C869.607654 316.298829 822.910099 362.996384 765.305123 362.996384z";
                    case "2": return "M 897.536 132.096 L 417.28 628.224 L 128 322.56 L 0.512 454.144 L 417.28 891.904 L 1024.51 263.68 Z";
                    default: return value;
                }
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
