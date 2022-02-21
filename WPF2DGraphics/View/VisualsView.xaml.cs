using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace WPF2DGraphics.View
{
    /// <summary>
    /// VisualsView.xaml 的交互逻辑
    /// </summary>
    public partial class VisualsView : Window
    {
        List<Visual> visuals = new List<Visual>();
        public VisualsView()
        {
            // InitializeComponent();

            Title = "Hosting DrawingVisuals";
            Width = 300;
            Height = 350;

            DrawingVisual bodyVisual = new DrawingVisual();
            DrawingVisual eyesVisual = new DrawingVisual();
            DrawingVisual mouthVisual = new DrawingVisual();

            using (DrawingContext dc = bodyVisual.RenderOpen())
            {
                // The body
                dc.DrawGeometry(Brushes.Blue, null, Geometry.Parse(
                @"M 240,250
                  C 200,375 200,250 175,200
                  C 100,400 100,250 100,200
                  C 0,350 0,250 30,130
                  C 75,0 100,0 150,0
                  C 200,0 250,0 250,150 Z"));
            }
            using (DrawingContext dc = eyesVisual.RenderOpen())
            {
                // Left eye
                dc.DrawEllipse(Brushes.Black, new Pen(Brushes.White, 10),
                new Point(95, 95), 15, 15);
                // Right eye
                dc.DrawEllipse(Brushes.Black, new Pen(Brushes.White, 10),
                new Point(170, 105), 15, 15);
            }
            using (DrawingContext dc = mouthVisual.RenderOpen())
            {
                // The mouth
                Pen p = new Pen(Brushes.Black, 10);
                p.StartLineCap = PenLineCap.Round;
                p.EndLineCap = PenLineCap.Round;
                dc.DrawLine(p, new Point(75, 160), new Point(175, 150));
            }


            visuals.Add(bodyVisual);
            visuals.Add(eyesVisual);
            visuals.Add(mouthVisual);

            // Add to visual and logical tree
            foreach (Visual v in visuals)
            {
                AddVisualChild(v);
                AddLogicalChild(v);
            }
        }

        // The two necessary overrides, implemented for the single Visual:
        protected override int VisualChildrenCount
        {
            get { return visuals.Count; }
        }
        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }

        protected override void AddChild(object value)
        {
            base.AddChild(value);
        }


        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            // Retrieve the mouse pointer location relative to the Window
            Point location = e.GetPosition(this);
            //// Perform visual hit testing
            //HitTestResult result = VisualTreeHelper.HitTest(this, location);
            //// If we hit any DrawingVisual, rotate it
            //if (result.VisualHit.GetType() == typeof(DrawingVisual))
            //{
            //    DrawingVisual dv = result.VisualHit as DrawingVisual;
            //    if (dv.Transform == null)
            //        dv.Transform = new RotateTransform();
            //    (dv.Transform as RotateTransform).Angle++;
            //}

            // Perform visual hit testing
            VisualTreeHelper.HitTest(this, null,
            new HitTestResultCallback(HitTestCallback),
            new PointHitTestParameters(location));


        }

        public HitTestResultBehavior HitTestCallback(HitTestResult result)
        {
            // If we hit any DrawingVisual, rotate it
            if (result.VisualHit.GetType() == typeof(DrawingVisual))
            {
                DrawingVisual dv = result.VisualHit as DrawingVisual;
                if (dv.Transform == null)
                    dv.Transform = new RotateTransform();
                (dv.Transform as RotateTransform).Angle++;
            }
            // Keep looking for hits
            return HitTestResultBehavior.Continue;
        }
    }
}
