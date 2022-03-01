using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WPFVisualState.View
{
    /// <summary>
    /// LoadingWait.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingWait : UserControl
    {
        const double Virtual_Radius = 50;
        public LoadingWait()
        {
            InitializeComponent();
        }
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(nameof(Radius), typeof(double), typeof(LoadingWait), new PropertyMetadata(12.0, new PropertyChangedCallback(Radius_PropertyChangedCallback)));


        private static void Radius_PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LoadingWait).Refresh((double)e.NewValue);
        }
        private void Refresh(double radius)
        {
            viewbox.Width = radius * 2;
            viewbox.Height = radius * 2;

            var ellipses = canvas.Children.OfType<UIElement>().Where(u => u is Ellipse);
            for (int i = 0; i < ellipses.Count(); i++)
            {
                var ellipse = ellipses.ElementAt(i);
                MeasureXY(i, out double x, out double y);

                Canvas.SetLeft(ellipse, x);
                Canvas.SetTop(ellipse, y);
            }
        }

        private void MeasureXY(int i, out double x, out double y)
        {
            x = Virtual_Radius + Virtual_Radius * Math.Sin(i * Math.PI * 2 / 10.0);
            y = Virtual_Radius + Virtual_Radius * Math.Cos(i * Math.PI * 2 / 10.0);
        }

        public bool Run
        {
            get { return (bool)GetValue(RunProperty); }
            set { SetValue(RunProperty, value); }
        }

        public static readonly DependencyProperty RunProperty =
            DependencyProperty.Register(nameof(Run), typeof(bool), typeof(LoadingWait), new PropertyMetadata(default(bool), new PropertyChangedCallback(Run_PropertyChangedCallback)));

        private static void Run_PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as LoadingWait;
            VisualStateManager.GoToState(obj, (bool)e.NewValue ? obj.RunState.Name : obj.StopState.Name, false);
        }
    }
}
