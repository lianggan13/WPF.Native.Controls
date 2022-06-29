using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF.Graphics.VisualRender
{
    /// <summary>
    /// VisualLayer.xaml 的交互逻辑
    /// </summary>
    public partial class VisualLayer : Page
    {
        public VisualLayer()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            custTxtRender.RenderTransform = new ScaleTransform(6, 6);
        }
    }
}
