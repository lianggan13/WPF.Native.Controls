using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF.Brush.Brushes
{
    /// <summary>
    /// ClickTheGradientCenter.xaml 的交互逻辑
    /// </summary>
    public partial class ClickTheGradientCenter : UserControl
    {
        public ClickTheGradientCenter()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseButtonEventArgs args)
        {
            double width = ActualWidth;
            double height = ActualHeight;

            Point ptMouse = args.GetPosition(this);
            ptMouse.X /= width;
            ptMouse.Y /= height;

            if (args.ChangedButton == MouseButton.Left)
            {
                brush.Center = ptMouse;
                brush.GradientOrigin = ptMouse;
            }
            else if (args.ChangedButton == MouseButton.Right)
                brush.GradientOrigin = ptMouse;
        }
    }
}
