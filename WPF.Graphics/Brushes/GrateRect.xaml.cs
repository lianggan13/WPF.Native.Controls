using System.Windows;
using System.Windows.Controls;


namespace WPF.Graphics.Brushes
{
    /// <summary>
    /// GrateRect.xaml 的交互逻辑
    /// </summary>
    public partial class GrateRect : Control
    {
        public GrateRect()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty DefenseProperty =
            DependencyProperty.Register(nameof(Defense), typeof(bool), typeof(GrateRect), new PropertyMetadata(false));

        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register(nameof(Left), typeof(GratingState), typeof(GrateRect), new PropertyMetadata(GratingState.Unknown));

        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register(nameof(Top), typeof(GratingState), typeof(GrateRect), new PropertyMetadata(GratingState.Unknown));

        public static readonly DependencyProperty RightProperty =
            DependencyProperty.Register(nameof(Right), typeof(GratingState), typeof(GrateRect), new PropertyMetadata(GratingState.Unknown));

        public static readonly DependencyProperty BottomProperty =
            DependencyProperty.Register(nameof(Bottom), typeof(GratingState), typeof(GrateRect), new PropertyMetadata(GratingState.Unknown));

        public bool Defense
        {
            get { return (bool)GetValue(DefenseProperty); }
            set { SetValue(DefenseProperty, value); }
        }

        public GratingState Left
        {
            get { return (GratingState)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        public GratingState Top
        {
            get { return (GratingState)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        public GratingState Right
        {
            get { return (GratingState)GetValue(RightProperty); }
            set { SetValue(RightProperty, value); }
        }

        public GratingState Bottom
        {
            get { return (GratingState)GetValue(BottomProperty); }
            set { SetValue(BottomProperty, value); }
        }
    }
}
