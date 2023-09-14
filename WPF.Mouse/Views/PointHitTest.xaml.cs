using System;
using System.Windows;
using System.Windows.Media;

namespace WPF.Mouse.Views
{
    using System.Windows.Input;
    public partial class PointHitTest
    {
        private string hitStatus;

        public PointHitTest()
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
            Point position = Mouse.GetPosition(RectangleArea);

            hitStatus = "no hit";

            // Set up a callback to receive the hit test result enumeration.
            VisualTreeHelper.HitTest(
                RectangleArea,
                null,
                    new HitTestResultCallback(HitTestResultHandler),
                    new PointHitTestParameters(position)
            );

            // Alert the status of the hit test
            this.HitLabel.Content = String.Format("Result of the hit test: {0}", hitStatus);
        }

        public HitTestResultBehavior HitTestResultHandler(HitTestResult result)
        {
            PointHitTestResult hitResult = (PointHitTestResult)result;

            hitStatus = String.Format(
                "{0} was hit at this point: {1}",
                ((FrameworkElement)hitResult.VisualHit).Name,
                hitResult.PointHit.ToString()
            );

            // Set the behavior to return visuals at all z-order levels.
            return HitTestResultBehavior.Continue;
        }
    }
}
