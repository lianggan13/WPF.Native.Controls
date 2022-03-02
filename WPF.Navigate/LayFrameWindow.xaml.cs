using Frame界面导航过度.ViewModel;
using System.Windows;

namespace FrameTranlation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LayFrameWindow : Window
    {
        public LayFrameWindow()
        {
            InitializeComponent();
            this.DataContext = new LayFrameViewModel();
        }
    }
}
