using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WPF.Common.Converters
{
    public class ImagePathConverter : IValueConverter
    {
        private string imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Images");
        public string ImageDirectory
        {
            get { return imageDirectory; }
            set { imageDirectory = value; }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string imagePath = Path.Combine(ImageDirectory, (string)value);
            return new BitmapImage(new Uri(imagePath));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("The method or operation is not implemented.");
        }
    }
}
