using System.Windows;
using System.Windows.Controls.Primitives;

namespace WPF.Button.Controls
{
    /// <summary>
    /// SwitchButtonV2.xaml 的交互逻辑
    /// </summary>
    public partial class SwitchButtonV2 : ToggleButton
    {
        public SwitchButtonV2()
        {
            InitializeComponent();
            SetCurrentValue(TextProperty, "sss");
        }


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SwitchButtonV2), new PropertyMetadata(""));


    }
}
