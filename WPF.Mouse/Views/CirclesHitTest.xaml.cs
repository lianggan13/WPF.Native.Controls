using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF.Mouse.Views
{
    using System.Windows.Input;
    public partial class CirclesHitTest
    {
        private string hitStatus = "no hit";
        private Path ellipseTrace;

        public CirclesHitTest()
        {
            this.InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            CompositionTarget.Rendering += this.CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            // Retrieve the coordinate of the ellipse position.
            Point position = Mouse.GetPosition(SceneCanvas);

            // Get the Geometry of the colliding object
            EllipseGeometry hitTestArea = new EllipseGeometry(
                position,
                DragEllipse.Width / 2,
                DragEllipse.Height / 2
            );

            hitStatus = "no hit";

            this.SceneCanvas.Children.Remove(ellipseTrace);

            ellipseTrace = new Path();
            ellipseTrace.Data = hitTestArea;
            ellipseTrace.Stroke = Brushes.Black;

            this.SceneCanvas.Children.Add(ellipseTrace);
            // Set up a callback to receive the hit test result enumeration.
            VisualTreeHelper.HitTest(
                this.SceneCanvas,
                MyHitTestFilter,
                    new HitTestResultCallback(HitTestResultHandler),
                    new GeometryHitTestParameters(hitTestArea)
            );

            // Alert the status of the hit test
            this.HitLabel.Content = String.Format("Result of the hit test: {0}", hitStatus);

            Canvas.SetLeft(DragEllipse, position.X - (DragEllipse.Width / 2));
            Canvas.SetTop(DragEllipse, position.Y - (DragEllipse.Height / 2));
        }


        public HitTestResultBehavior HitTestResultHandler(HitTestResult result)
        {
            GeometryHitTestResult hitResult = (GeometryHitTestResult)result;

            // Retrieve the results of the hit test.
            IntersectionDetail intersectionDetail = hitResult.IntersectionDetail;

            switch (intersectionDetail)
            {
                case IntersectionDetail.FullyContains:
                    hitStatus = "hit - FullyContains";
                    break;
                case IntersectionDetail.Intersects:
                    hitStatus = "hit - Intersects";
                    break;
                case IntersectionDetail.FullyInside:
                    hitStatus = "hit - FullyInside";
                    break;
                case IntersectionDetail.Empty:
                default:
                    hitStatus = "no hit";
                    break;
            }

            // Set the behavior to return visuals at all z-order levels.
            return HitTestResultBehavior.Stop;
        }

        public HitTestFilterBehavior MyHitTestFilter(DependencyObject o)
        {
            // Test for the object value you want to filter.
            if (o.GetType() == typeof(Canvas) || o.GetType() == typeof(Ellipse))
            {
                return HitTestFilterBehavior.ContinueSkipSelf;
            }
            else
            {
                return HitTestFilterBehavior.Continue;
            }
        }
    }
}
