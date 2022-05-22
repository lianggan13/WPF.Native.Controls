using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF.Graphics.Brushes
{
    /// <summary>
    /// ImageBrushes.xaml 的交互逻辑
    /// </summary>
    public partial class ImageBrushes : Page
    {
        public ImageBrushes()
        {
            InitializeComponent();
            EllipseGeometry ellipse = new EllipseGeometry(new Point(50, 50), 50, 20);
            RectangleGeometry rect = new RectangleGeometry(new Rect(50, 50, 50, 20), 5, 5);
        }
    }
}
