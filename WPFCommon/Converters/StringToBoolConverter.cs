using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace GTS.MaxSign.Controls.Converters
{

    [ValueConversion(typeof(string), typeof(bool))]
    public class StringToBoolConverter
      : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ss = parameter.ToString().Split(':');   // parameter RMC:Collapsed,RMC:Visible
            string parKey = ss.ElementAt(0);
            object parValue = bool.Parse(ss.ElementAt(1)); //Enum.Parse(typeof(Visibility), ss.ElementAt(1));
            if (value == null)
            {
                return null;
            }
            else if (value.Equals(parKey))
            {
                // 满足条件 返回 parameter 中 parValue
                return parValue;
            }
            else
            {
                return !(bool)parValue;
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }


    }
}
