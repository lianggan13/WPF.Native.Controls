using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF.Mouse.Views
{
    /// <summary>
    /// MouseFollow.xaml 的交互逻辑
    /// </summary>
    public partial class MouseFollow : UserControl
    {
        private Vector _rectangleVelocity = new Vector(0, 0);
        private Point _lastMousePosition = new Point(0, 0);
        public MouseFollow()
        {
            this.InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            CompositionTarget.Rendering += UpdateRectangle;
            this.PreviewMouseMove += UpdateLastMousePosition;
        }

        protected void UpdateRectangle(object sender, EventArgs e)
        {
#if !true
            {
                Point location = new Point(Canvas.GetLeft(followRectangle), Canvas.GetTop(followRectangle));
                var mousePosition = System.Windows.Input.Mouse.GetPosition(containerCanvas);

                Vector toMouse = mousePosition - location;

                location += toMouse;

                Canvas.SetLeft(followRectangle, location.X);
                Canvas.SetTop(followRectangle, location.Y);
            }
#else

            {
                Point location = new Point(Canvas.GetLeft(followRectangle), Canvas.GetTop(followRectangle));
                //find vector toward mouse location
                Vector toMouse = _lastMousePosition - location;

                //add a force toward the mouse to the rectangles velocity
                double followForce = 0.01;
                _rectangleVelocity += toMouse * followForce;

                //dampen the velocity to add stability
                double drag = 0.8;
                _rectangleVelocity *= drag;

                //update the new location from the velocity
                location += _rectangleVelocity;

                //set new position
                Canvas.SetLeft(followRectangle, location.X);
                Canvas.SetTop(followRectangle, location.Y);
            }
#endif
        }

        void UpdateLastMousePosition(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _lastMousePosition = e.GetPosition(containerCanvas);
        }
    }
}
