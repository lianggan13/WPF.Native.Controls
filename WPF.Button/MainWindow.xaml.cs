using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF.Button.Controls;

namespace WPF.Button
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnBct_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("【开启广播】成功！");
        }

        private void btnBct_Unchecked(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("【停止广播】成功！");
        }

        private void DrawnButton_Knock(object sender, RoutedEventArgs e)
        {
            DrawnButton btn = e.Source as DrawnButton;
            MessageBox.Show("The button labeled \"" + btn.Text +
                            "\" has been knocked.", Title);
        }

        private void ColorGrid_SelectedColorChanged(object sender, System.EventArgs e)
        {
            ColorGrid clrgrid = sender as ColorGrid;
            var grid = (Grid)clrgrid.Parent;
            grid.Background = new SolidColorBrush(clrgrid.SelectedColor);
        }
    }
}
