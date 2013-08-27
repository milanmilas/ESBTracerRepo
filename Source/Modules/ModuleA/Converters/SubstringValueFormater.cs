using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ModuleA.Converters
{
    public class SubstringValueFormater : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = value.ToString();
            int prefixLength;
            if (!int.TryParse(parameter.ToString(), out prefixLength))
                return s;
            int stringLength = s.Length;

            if (stringLength <= (prefixLength < 0 ? -prefixLength : prefixLength))
            {
                return s;
            }

            return s.Substring(0, prefixLength > 0 ? prefixLength : s.Length + prefixLength);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
