using System.Windows;
using System.Windows.Controls;

namespace WPF.Brush.Brushes
{
    /// <summary>
    /// 灯带状态
    /// </summary>
    public enum LightBeltState
    {
        Gray,
        Green,
        Red,
        Blue
    }

    public partial class LightBand : Control
    {
        public LightBand()
        {
            InitializeComponent();
        }
        public LightBeltState State
        {
            get { return (LightBeltState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register(nameof(State), typeof(LightBeltState), typeof(LightBand), new PropertyMetadata(LightBeltState.Gray));
    }
}
