using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF.Animation.View
{
    /// <summary>
    /// BouncingBall.xaml 的交互逻辑
    /// </summary>
    public partial class BouncingBall : Page
    {

        private bool _dragging = false;

        public BouncingBall()
        {
            InitializeComponent();
        }

        private void BouncingBall_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
        }

        private void BouncingBall_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void BouncingBall_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragging)
                return;

            double x = Mouse.GetPosition(this.BouncingBallContainer).X;

            Vector vector = VisualTreeHelper.GetOffset(this.BouncingBallCanvas);
            double pX = vector.X;

            TranslateTransform t = (TranslateTransform)this.bounce;
            t.X = x - pX;
        }

    }

    public class FlipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            return (((double)value) * -1);
        }
        public object ConvertBack(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            return (((double)value) * -1);
        }
    }
}
