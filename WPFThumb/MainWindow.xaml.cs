using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WPFThumb
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            double nTop = Canvas.GetTop(thumb) + e.VerticalChange;
            double nLeft = Canvas.GetLeft(thumb) + e.HorizontalChange;
            if (nTop < 0)
            {
                nTop = 0;
            }
            else if (nTop > canvas.ActualHeight - thumb.ActualHeight)
            {
                nTop = canvas.ActualHeight - thumb.ActualHeight;
            }

            if (nLeft < 0)
            {
                nLeft = 0;
            }
            else if (nLeft > canvas.ActualWidth - thumb.ActualWidth)
            {
                nLeft = canvas.ActualWidth - thumb.ActualWidth;
            }

            Canvas.SetTop(thumb, nTop);
            Canvas.SetLeft(thumb, nLeft);

            tt.Text = $"Top:{nTop} \nLeft:{nLeft}";
        }
    }
}
