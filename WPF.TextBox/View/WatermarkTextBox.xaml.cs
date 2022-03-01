using System.Windows;
using System.Windows.Controls;

namespace WPFTextBox.View
{
    /// <summary>
    /// WatermarkTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class WatermarkTextBox : TextBox
    {
        public WatermarkTextBox()
        {
            InitializeComponent();
        }

        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register(nameof(Watermark), typeof(string), typeof(WatermarkTextBox), new PropertyMetadata(string.Empty));
    }
}
