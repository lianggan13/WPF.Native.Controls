using System;
using System.Windows;

using System.Windows.Media;

namespace WPF.Mouse.Views
{
    using System.Windows.Controls;
    using System.Windows.Input;
    public partial class MousePosition
    {
        private TranslateTransform ellipseTransform = new TranslateTransform();

        public MousePosition()
        {
            this.InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            DragEllipse.RenderTransform = ellipseTransform;
            CompositionTarget.Rendering += this.CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            Point mouse1 = Mouse.GetPosition(firstEllipse);
            Point mouse2 = Mouse.GetPosition(secondEllipse);
            Point mouse3 = Mouse.GetPosition(thirdEllipse);
            Point mouse4 = Mouse.GetPosition(fourthEllipse);

            firstCoordinates.Content = mouse1.ToString();
            secondCoordinates.Content = mouse2.ToString();
            thirdCoordinates.Content = mouse3.ToString();
            fourthCoordinates.Content = mouse4.ToString();

            Point position = Mouse.GetPosition(DragEllipse);
            ellipseTransform.X += position.X - (DragEllipse.Width / 2);
            ellipseTransform.Y += position.Y - (DragEllipse.Height / 2);
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            System.Windows.Controls.Primitives.Thumb thumb = (System.Windows.Controls.Primitives.Thumb)sender;
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
        }
    }
}
