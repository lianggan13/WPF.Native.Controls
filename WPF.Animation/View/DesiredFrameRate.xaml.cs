using System.Windows.Controls;
using System.Windows.Media;

namespace WPF.Animation.View
{
    /// <summary>
    /// DesiredFrameRate.xaml 的交互逻辑
    /// </summary>
    public partial class DesiredFrameRate : UserControl
    {
        public DesiredFrameRate()
        {
            InitializeComponent();

            // 确定显卡支持的渲染级别
            var tier = RenderCapability.Tier;
        }
    }
}
