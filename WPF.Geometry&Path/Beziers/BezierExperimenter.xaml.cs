using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF.Geometry_Path.Beziers
{
    /// <summary>
    /// BezierExperimenter.xaml 的交互逻辑
    /// </summary>
    public partial class BezierExperimenter : UserControl
    {
        public BezierExperimenter()
        {
            InitializeComponent();
        }

        // When the Canvas size changes, reset the four points.
        protected virtual void CanvasOnSizeChanged(object sender,
                                        SizeChangedEventArgs args)
        {
            ptStart.Center = new Point(args.NewSize.Width / 4,
                                       args.NewSize.Height / 2);
            ptCtrl1.Center = new Point(args.NewSize.Width / 2,
                                       args.NewSize.Height / 4);
            ptCtrl2.Center = new Point(args.NewSize.Width / 2,
                                       3 * args.NewSize.Height / 4);
            ptEnd.Center = new Point(3 * args.NewSize.Width / 4,
                                     args.NewSize.Height / 2);


            DrawBezierManually();
        }
        // Change the control points based on mouse clicks and moves.
        protected override void OnMouseDown(MouseButtonEventArgs args)
        {
            base.OnMouseDown(args);
            Point pt = args.GetPosition(canvas);

            if (args.ChangedButton == MouseButton.Left)
                ptCtrl1.Center = pt;

            if (args.ChangedButton == MouseButton.Right)
                ptCtrl2.Center = pt;

            DrawBezierManually();
        }
        protected override void OnMouseMove(MouseEventArgs args)
        {
            base.OnMouseMove(args);
            Point pt = args.GetPosition(canvas);

            if (args.LeftButton == MouseButtonState.Pressed)
                ptCtrl1.Center = pt;

            if (args.RightButton == MouseButtonState.Pressed)
                ptCtrl2.Center = pt;


            DrawBezierManually();
        }

        void DrawBezierManually()
        {
            Point[] pts = new Point[10];

            for (int i = 0; i < pts.Length; i++)
            {
                double t = (double)i / (pts.Length - 1);

                double x = (1 - t) * (1 - t) * (1 - t) * ptStart.Center.X +
                           3 * t * (1 - t) * (1 - t) * ptCtrl1.Center.X +
                           3 * t * t * (1 - t) * ptCtrl2.Center.X +
                           t * t * t * ptEnd.Center.X;

                double y = (1 - t) * (1 - t) * (1 - t) * ptStart.Center.Y +
                           3 * t * (1 - t) * (1 - t) * ptCtrl1.Center.Y +
                           3 * t * t * (1 - t) * ptCtrl2.Center.Y +
                           t * t * t * ptEnd.Center.Y;

                pts[i] = new Point(x, y);
            }
            fittingBezier.Points = new PointCollection(pts);
        }
    }
}
