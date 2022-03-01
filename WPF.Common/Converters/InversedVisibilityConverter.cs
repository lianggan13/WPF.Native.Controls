using System;
using System.Windows;
using System.Windows.Data;

namespace GTS.MaxSign.Controls.Converters
{
    [ValueConversion(typeof(Visibility), typeof(Visibility))]
    public class InversedVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return (Visibility)value == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
