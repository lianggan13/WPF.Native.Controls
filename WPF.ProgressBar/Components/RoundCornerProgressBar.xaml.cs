using System.Windows;

namespace WPF.ProgressBar.Components
{
    /// <summary>
    /// RoundCornerProgressBar.xaml 的交互逻辑
    /// </summary>
    public partial class RoundCornerProgressBar : System.Windows.Controls.ProgressBar
    {

        static RoundCornerProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RoundCornerProgressBar),
                new FrameworkPropertyMetadata(typeof(RoundCornerProgressBar)));
        }

        public RoundCornerProgressBar()
        {
            InitializeComponent();
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(RoundCornerProgressBar),
                new PropertyMetadata(new CornerRadius(8)));
    }
}
