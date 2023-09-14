using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF.Animation.View
{
    /// <summary>
    /// RotationPath.xaml 的交互逻辑
    /// </summary>
    public partial class RotationPath : Page
    {
        private double radius = 20;
        private double maxRadius;
        private double currTheta = 0;
        private Point center;
        private TranslateTransform translation = new TranslateTransform();
        private Path outline = new Path();

        public RotationPath()
        {
            this.InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            // Set up the center of the coordinate plane
            this.center = new Point(this.Width / 2, this.Height / 2);//Vector(0, 0);

            // Set the maximum radius length
            this.maxRadius = this.Width / 3;

            // Default the radius
            this.RadiusControl.Value = 2;

            // hook up a handler to be called each time the scene is rendered.
            CompositionTarget.Rendering += this.CompositionTarget_Rendering;

            // Draw the rotation outline
            EllipseGeometry rotationOutline = new EllipseGeometry(
                new Point(this.Width / 2, this.Height / 2), this.radius, this.radius);

            outline.Data = rotationOutline;
            outline.Stroke = Brushes.Black;
            outline.StrokeThickness = .25;

            this.Grid.Children.Add(outline);

            // assign the transform
            this.MyControl.RenderTransform = this.translation;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            this.maxRadius = this.Grid.ActualWidth / 3;


            double angleInRadians = DegreeToRad(currTheta);
            double sliderRatio = this.RadiusControl.Value * .1;

            // Adjust the radius to the current slider position
            this.radius = sliderRatio * this.maxRadius;

            // Apply the coordinate transformation
            this.translation.X = this.radius * Math.Cos(angleInRadians);
            this.translation.Y = this.radius * Math.Sin(angleInRadians);

            // increment angle
            currTheta++;

            this.center = new Point(this.Grid.ActualWidth / 2, this.Grid.ActualHeight / 2);
            // Adjust the path trace
            EllipseGeometry pathOutline = (EllipseGeometry)outline.Data;
            pathOutline.Center = new Point(this.center.X, this.center.Y);
            pathOutline.RadiusX = this.radius;
            pathOutline.RadiusY = this.radius;
        }

        private double DegreeToRad(double theta)
        {
            return Math.PI / 180 * theta;
        }
    }
}
