using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF.Control.Components
{
    public class CustomDrawnDecorator : Decorator
    {
        public static DependencyProperty BackgroundColorProperty;
        static CustomDrawnDecorator()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata(Colors.Yellow);
            metadata.AffectsRender = true;
            BackgroundColorProperty = DependencyProperty.Register("BackgroundColor",
                typeof(Color), typeof(CustomDrawnDecorator), metadata);
        }


        public Color BackgroundColor
        {
            get
            {
                return (Color)GetValue(CustomDrawnDecorator.BackgroundColorProperty);
            }
            set
            {
                SetValue(CustomDrawnDecorator.BackgroundColorProperty, value);
            }
        }

        private Brush GetForegroundBrush()
        {
            if (!IsMouseOver)
            {
                return new SolidColorBrush(BackgroundColor);
            }
            else
            {
                RadialGradientBrush brush = new RadialGradientBrush(Colors.White, BackgroundColor);
                Point absoluteGradientOrigin = Mouse.GetPosition(this);
                Point relativeGradientOrigin = new Point(
                    absoluteGradientOrigin.X / base.ActualWidth, absoluteGradientOrigin.Y / base.ActualHeight);

                brush.GradientOrigin = relativeGradientOrigin;
                brush.RadiusX = 0.2;
                brush.Center = relativeGradientOrigin;
                brush.Freeze();
                return brush;
            }
        }
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            Rect bounds = new Rect(0, 0, base.ActualWidth, base.ActualHeight);
            dc.DrawRectangle(GetForegroundBrush(), null, bounds);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.InvalidateVisual();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.InvalidateVisual();
        }

        protected override Size MeasureOverride(Size constraint)
        {
            UIElement child = this.Child;
            if (child != null)
            {
                child.Measure(constraint);
                return child.DesiredSize;
            }
            else
            {
                return new Size();
            }

        }
    }
}
