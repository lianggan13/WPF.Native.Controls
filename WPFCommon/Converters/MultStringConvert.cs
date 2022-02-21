using System;
using System.Globalization;
using System.Windows.Data;

namespace GTS.MaxSign.Controls.Converters
{
    public class MultStringConvert : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string name = values[0].ToString() + values[1].ToString();
            return name;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
