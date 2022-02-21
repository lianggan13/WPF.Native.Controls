using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Mercku.UpperMonitor.Controls.Converters
{
    public class IndexToVisibilityConverter: IMultiValueConverter
    {

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int sort = 0;
            int total = 0;
            if (values[0] is int)
            {
                sort = (int)values[0];
            }
            if (values[1] is int)
            {
                total = (int)values[1];
            }
            if(sort == 0)
            {
                return Visibility.Collapsed;
            }
            else
                return Visibility.Visible;
            //else if (value is bool?)
            //{
            //    bool? nullable = (bool?)value;
            //    flag = nullable.HasValue ? nullable.Value : false;
            //}

            //bool inverse = (parameter as string) == "inverse";

            //if (inverse)
            //{
            //    return (flag ? Visibility.Collapsed : Visibility.Visible);
            //}
            //else
            //{
            //    return (flag ? Visibility.Visible : Visibility.Collapsed);
            //}
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
