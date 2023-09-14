using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF.ProgressBar.View
{
    /// <summary>
    /// AnimationProgressBar.xaml 的交互逻辑
    /// </summary>
    public partial class AnimationProgressBar : Page
    {
        public AnimationProgressBar()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            progBar.Value = progBar.Maximum / 2;
            await Task.Delay(1500);
            progBar.Value = progBar.Maximum;
        }
    }
}
