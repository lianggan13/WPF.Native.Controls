using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF.Graphics.Shapes
{
    /// <summary>
    /// Polyxxx.xaml 的交互逻辑
    /// </summary>
    public partial class Polyxxxs : UserControl
    {
        const int numpts = 2000;
        public static PointCollection SinePoints { get; set; }

        static Polyxxxs()
        {
            Point[] pts = new Point[numpts];
            for (int i = 0; i < 2000; i++)
            {
                var p = new Point(i, 96 * (1 - Math.Sin(i * Math.PI / 180)));
                // 随着越多的点加入，Pointcollection 需要分配空间的次数也越多。是在循环 Add 之前，先指定点的个数。
                //SinePoints.Add(p);
                pts[i] = p;
            }

            SinePoints = new PointCollection(pts);
        }

        public Polyxxxs()
        {
            InitializeComponent();
        }
    }
}
