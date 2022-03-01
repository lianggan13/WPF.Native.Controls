using System.Windows;
using System.Windows.Controls;

namespace WPFAnimation.View
{
    /// <summary>
    /// KeyFrameAnimationView.xaml 的交互逻辑
    /// </summary>
    public partial class KeyFrameAnimationView : UserControl
    {
        public KeyFrameAnimationView()
        {
            InitializeComponent();
        }

        private void txtWaiting_Unloaded(object sender, RoutedEventArgs e)
        {
            //st.Storyboard.sto
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            st.Storyboard.Stop(txtWaiting);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            st.Storyboard.Begin(this, true);
        }
    }
}
