using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GTS.MaxSign.Controls.Converters
{
    [ValueConversion(typeof(string), typeof(SolidColorBrush))]
    public class StringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string sValue = value?.ToString();
            if (string.IsNullOrEmpty(sValue))
            {
                return new SolidColorBrush(Color.FromArgb(0xFF, 0x37, 0x6B, 0xFA));
            }
            else
            {
                return new SolidColorBrush(Color.FromArgb(0xFF, 0x21, 0x21, 0x21));
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
