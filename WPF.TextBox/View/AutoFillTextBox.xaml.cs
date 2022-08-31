using System.Windows;

using System.Windows.Controls.Primitives;

namespace WPF.TextBox.View
{
    using System.Windows.Controls;


    /// <summary>
    /// SearchTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class AutoFillTextBox : Page
    {
        public AutoFillTextBox()
        {
            InitializeComponent();
        }

    }

    public class ContentTextBox : TextBox
    {
        static ContentTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentTextBox), new FrameworkPropertyMetadata(typeof(ContentTextBox)));
        }

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(ContentTextBox));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(ContentTextBox), new PropertyMetadata(false));

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            if (string.IsNullOrEmpty(Text)) IsOpen = false;
            else IsOpen = true;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Loaded -= MyTextBox_Loaded;
            Loaded += MyTextBox_Loaded;
        }

        private void MyTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (Content is Selector selector)
            {
                selector.SelectionChanged -= Selector_SelectionChanged;
                selector.SelectionChanged += Selector_SelectionChanged;
            }
        }

        private void Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsOpen = false;
        }
    }


}
