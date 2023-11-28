using System.Windows;
using WPF.TextBox.Components.NotepadClone;

namespace WPF.TextBox
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NoeepadCloneButton_Click(object sender, RoutedEventArgs e)
        {
            new NotepadClone()
            {
                Owner = this,
            }.ShowDialog();
        }
    }
}
