using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GTS.MaxSign.Controls.Converters
{
    /// <summary>
    /// Converts boolean to visibility values.
    /// </summary>
    public class StatusToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Brush color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
            bool is_checked = (bool)values[0];
            bool is_pass = (bool)values[1];
            bool is_checking = (bool)values[2];
            if (is_checked && is_pass)
                color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
            else if (is_checked && !is_pass)
                color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E83B3B"));
            else if (!is_checked)
                color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#999999"));
            if (is_checking)
                color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#376BFA"));
            return color;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
