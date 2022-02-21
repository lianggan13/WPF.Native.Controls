using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WPFAnimation.View
{
    /// <summary>
    /// Interaction logic for LoadingWait.xaml
    /// </summary>
    public partial class LoadingWait : UserControl
    {
        public LoadingWait()
        {
            InitializeComponent();
        }

        private void HandleLoaded(object sender, RoutedEventArgs e)
        {

            return;
            //const double offset = Math.PI;
            const double offset = 0;
            const double step = Math.PI * 2 / 10.0;

            SetPosition(C0, offset, 0.0, step);
            SetPosition(C1, offset, 1.0, step);
            SetPosition(C2, offset, 2.0, step);
            SetPosition(C3, offset, 3.0, step);
            SetPosition(C4, offset, 4.0, step);
            SetPosition(C5, offset, 5.0, step);
            SetPosition(C6, offset, 6.0, step);
            SetPosition(C7, offset, 7.0, step);
            SetPosition(C8, offset, 8.0, step);
        }

        private void SetPosition(Ellipse ellipse, double offset, double posOffSet, double step)
        {
            double angle = offset + posOffSet * step;
            ellipse.SetValue(Canvas.LeftProperty, 50.0
                + Math.Sin(offset + posOffSet * step) * 50.0);

            double cos = Math.Cos(offset + posOffSet * step) * 50.0;
            ellipse.SetValue(Canvas.TopProperty, 50
                + Math.Cos(offset + posOffSet * step) * 50.0);

            //double angle = offset + posOffSet * step;
            //ellipse.SetValue(Canvas.LeftProperty, 50.0
            //    + Math.Cos(offset + posOffSet * step) * 50.0);

            //double cos = Math.Cos(offset + posOffSet * step) * 50.0;
            //ellipse.SetValue(Canvas.TopProperty, 50
            //    + Math.Sin(offset + posOffSet * step) * 50.0);
        }

        private void HandleUnloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
