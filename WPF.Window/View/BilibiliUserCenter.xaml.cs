
namespace WPF.Window.View
{

    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BilibiliUserCenter : Window
    {
        public BilibiliUserCenter()
        {
            InitializeComponent();
        }
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            //pageFrame.Source = new Uri(radioButton.CommandParameter.ToString(), UriKind.Relative);
        }
    }
}
