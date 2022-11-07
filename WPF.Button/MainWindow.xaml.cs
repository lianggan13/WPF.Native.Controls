using System.Windows;
using System.Windows.Media;

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
    }
    public class Foo
    {
        public static Brush GetBar(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BarProperty);
        }

        public static void SetBar(DependencyObject obj, Brush value)
        {
            obj.SetValue(BarProperty, value);
        }

        // Using a DependencyProperty as the backing store for Bar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BarProperty =
            //DependencyProperty.RegisterAttached("Bar", typeof(Brush), typeof(System.Windows.Controls.Button), new PropertyMetadata(null));
            DependencyProperty.RegisterAttached("Bar", typeof(Brush), typeof(Foo), new PropertyMetadata(null));




    }
}
