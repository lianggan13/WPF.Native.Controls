using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFCommon.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value == false ? new SolidColorBrush(Color.FromArgb(0xFF, 0x37, 0x6B, 0xFA)) : new SolidColorBrush(Color.FromArgb(0xFF, 0x21, 0x21, 0x21));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
