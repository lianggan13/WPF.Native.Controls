using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF.Brush.Views
{
    using System.Windows.Media;
    /// <summary>
    /// VaryBrush.xaml 的交互逻辑
    /// </summary>
    public partial class VaryBrush : UserControl
    {
        //SolidColorBrush brush = new SolidColorBrush(Colors.Black);
        //SolidColorBrush brush = Brushes.Black; // Invalid Opertaion Exception 利用 Brushes 所得到的 SolidColorBrush 对象处于 冻结 (frozen) 状态，不能再改变。将对象冻结，可以提高效率，不需要监控，且可以在不同线程之间共享
        SolidColorBrush brush = Brushes.Black.Clone();
        public VaryBrush()
        {
            InitializeComponent();
            Background = brush;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            double width = ActualWidth;
            double height = ActualHeight;
            Point ptCenter = new Point(width / 2, height / 2);

            Point ptMouse = e.GetPosition(this);
            Vector vectMouse = ptMouse - ptCenter;

            double angle = Math.Atan2(vectMouse.Y, vectMouse.X) * 180; // 此角度是从 水平轴开始顺时针旋转，所得到的夹角

            //Vector vectHorin = new Point(ptMouse.X, ptCenter.Y) - ptCenter;
            //double angle = Vector.AngleBetween(vectHorin, vectMouse);

            Vector vectEllipse = new Vector(width / 2 * Math.Cos(angle),
                                            height / 2 * Math.Sin(angle));
            Byte byLevel = (byte)(255 * (1 - Math.Min(1, vectMouse.Length /
                                                          vectEllipse.Length)));
            Color clr = brush.Color;
            clr.R = clr.G = clr.B = byLevel;
            brush.Color = clr;


            line.X1 = ptCenter.X;
            line.Y1 = ptCenter.Y;

            line.X2 = ptMouse.X;
            line.Y2 = ptMouse.Y;
        }
    }
}
