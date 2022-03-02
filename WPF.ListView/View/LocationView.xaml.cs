using ListViewLocation.ViewModel;
using System.Windows;

namespace ListViewLocation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new LocationViewModel();
        }


    }
}
