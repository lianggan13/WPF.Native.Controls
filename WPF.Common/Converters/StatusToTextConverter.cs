using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GTS.MaxSign.Controls.Converters
{
    /// <summary>
    /// Converts boolean to visibility values.
    /// </summary>
    public class StatusToTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string text = string.Empty;
            bool is_checked = (bool)values[0];
            bool is_pass = (bool)values[1];
            bool is_checking = (bool)values[2];
            if (is_checking)
                text = Application.Current.Resources["trans0021"] as string;  //正在
            else if (is_checked && is_pass)
                text = Application.Current.Resources["trans0020"] as string;  //正常
            else if (is_checked && !is_pass)
                text = Application.Current.Resources["trans0022"] as string;  //异常
            else if (!is_checked)
                text = Application.Current.Resources["trans0030"] as string;  //未检测
            return text;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
