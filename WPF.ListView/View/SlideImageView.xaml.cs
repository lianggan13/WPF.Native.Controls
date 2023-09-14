using System.Windows;
using System.Windows.Input;

namespace ListViewImgTurn
{
    /// <summary>
    /// SlideImageView.xaml 的交互逻辑
    /// </summary>
    public partial class SlideImageView : Window
    {
        public SlideImageView()
        {
            InitializeComponent();
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            double step = 0.02;
            double scale = st.ScaleX;

            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                if (e.Delta > 0 && scale < 10)
                {
                    // 放大
                    scale += step;
                }
                else
                {
                    // 缩小
                    if (scale > 0.2)
                        scale -= step;
                }

                st.ScaleX = st.ScaleY = scale;
                //vb.RenderTransform = new ScaleTransform(scale, scale);
            }
        }
    }
}
