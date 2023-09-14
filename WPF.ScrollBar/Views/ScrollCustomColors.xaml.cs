

namespace WPF.ScrollBar.Views
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    /// <summary>
    /// ScrollCustomColors.xaml 的交互逻辑
    /// </summary>
    public partial class ScrollCustomColors : UserControl
    {
        ScrollBar[] scrolls = new ScrollBar[3];
        public ScrollCustomColors()
        {
            InitializeComponent();
            //pnlColor.Background = new SolidColorBrush(SystemColors.WindowColor);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            scrolls[0] = barR;
            scrolls[1] = barG;
            scrolls[2] = barB;
            foreach (ScrollBar bar in scrolls)
            {
                bar.ValueChanged += ScrollOnValueChanged;
            }


            // Initialize scroll bars.
            Color clr = (pnlColor.Background as SolidColorBrush).Color;
            scrolls[0].Value = clr.R;
            scrolls[1].Value = clr.G;
            scrolls[2].Value = clr.B;

            // Set initial focus.
            scrolls[0].Focus();

        }

        private void ScrollOnValueChanged(object sender, RoutedEventArgs args)
        {
            ScrollBar scroll = sender as ScrollBar;
            Panel pnl = scroll.Parent as Panel;

            int index = Array.IndexOf(scrolls, scroll);
            TextBlock txt = pnl.Children[6 + index
                                    ] as TextBlock;

            txt.Text = String.Format("{0}\n0x{0:X2}", (int)scroll.Value);
            pnlColor.Background =
                new SolidColorBrush(
                    Color.FromRgb((byte)scrolls[0].Value,
                                  (byte)scrolls[1].Value, (byte)scrolls[2].Value));
        }
    }
}
