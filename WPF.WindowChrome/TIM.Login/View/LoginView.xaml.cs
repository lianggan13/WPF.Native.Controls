using System.Windows;

namespace TIM.Login.View
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void CloseBtn_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
