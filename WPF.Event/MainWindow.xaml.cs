using System.Windows;

namespace WPF.Event
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TapHandler(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tapped!");
        }
    }
}
