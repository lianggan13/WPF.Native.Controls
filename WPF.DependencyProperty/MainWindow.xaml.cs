using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFDependencyProperty
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SetTextBinding();
        }

        private void SetTextBinding()
        {
            var binding = new Binding
            {
                Source = this,
                Path = new PropertyPath("MyAge"),
                UpdateSourceTrigger = UpdateSourceTrigger.LostFocus
            };
            TestTextBox.SetBinding(TextBox.TextProperty, binding);
        }

        public int MyAge
        {
            get { return (int)GetValue(MyAgeProperty); }
            set { SetValue(MyAgeProperty, value); }
        }

        public static int DefaultAge = 18;

        public static readonly DependencyProperty MyAgeProperty =
            DependencyProperty.Register("MyAge", typeof(int), typeof(Window), new PropertyMetadata(18, PropertyChangedCallbackHandler, CoerceValueCallbackHandler), ValidateValueCallbackHandler);

        // 验证
        private static bool ValidateValueCallbackHandler(object value)
        {
            if (value is int iValue)
            {
                if (iValue > 100 || iValue < DefaultAge)
                {
                    MessageBox.Show($"年龄 {iValue} 不在允许范围中！");
                }
            }
            return true;
        }

        // 强制转换
        private static object CoerceValueCallbackHandler(DependencyObject d, object baseValue)
        {
            if (baseValue is int iValue)
            {
                return iValue > 100 || iValue < DefaultAge ? DefaultAge : iValue;
            }
            else
            {
                return false;
            }
        }

        // 属性变更
        private static void PropertyChangedCallbackHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window win)
            {
                var txt = win?.FindName("TestTextBox") as TextBox;
                txt.BorderBrush = Brushes.GreenYellow;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TestTextBox.Text = DefaultAge.ToString();
        }
    }
}
