using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace WPF.Control.Components
{
    public class EllipseWithChild : FrameworkElement
    {
        // Dependency properties.
        public static readonly DependencyProperty FillProperty;
        public static readonly DependencyProperty StrokeProperty;

        // Public interfaces to dependency properties.
        public Brush Fill
        {
            set { SetValue(FillProperty, value); }
            get { return (Brush)GetValue(FillProperty); }
        }
        public Pen Stroke
        {
            set { SetValue(StrokeProperty, value); }
            get { return (Pen)GetValue(StrokeProperty); }
        }
        // Static constructor.
        static EllipseWithChild()
        {
            FillProperty =
                DependencyProperty.Register("Fill", typeof(Brush),
                        typeof(EllipseWithChild), new FrameworkPropertyMetadata(null,
                                FrameworkPropertyMetadataOptions.AffectsRender));
            StrokeProperty =
                DependencyProperty.Register("Stroke", typeof(Pen),
                        typeof(EllipseWithChild), new FrameworkPropertyMetadata(null,
                                FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        UIElement child;

        // Public Child property.
        public UIElement Child
        {
            get => child;
            set
            {
                if (child != null)
                {
                    RemoveVisualChild(child);
                    RemoveLogicalChild(child);
                }
                if ((child = value) != null)
                {
                    AddVisualChild(child);
                    AddLogicalChild(child);
                }
            }

        }
        // Override of VisualChildrenCount returns 1 if Child is non-null.
        protected override int VisualChildrenCount
        {
            get
            {
                return Child != null ? 1 : 0;
            }
        }
        // Override of GetVisualChildren returns Child.
        protected override Visual GetVisualChild(int index)
        {
            if (index > 0 || Child == null)
                throw new ArgumentOutOfRangeException("index");

            return Child;
        }
        // Override of MeasureOverride calls child's Measure method.
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            Size sizeDesired = new Size(0, 0);

            if (Stroke != null)
            {
                sizeDesired.Width += 2 * Stroke.Thickness;
                sizeDesired.Height += 2 * Stroke.Thickness;

                sizeAvailable.Width =
                    Math.Max(0, sizeAvailable.Width - 2 * Stroke.Thickness);
                sizeAvailable.Height =
                    Math.Max(0, sizeAvailable.Height - 2 * Stroke.Thickness);
            }
            if (Child != null)
            {
                Child.Measure(sizeAvailable); // Measure: update child's DesiredSize.

                sizeDesired.Width += Child.DesiredSize.Width;
                sizeDesired.Height += Child.DesiredSize.Height; // Child's DesiredSize include Margin
            }
            return sizeDesired;
        }
        // Override of ArrangeOverride calls child's Arrange method.
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            // sizeFinal: final layout size
            if (Child != null)
            {
                Rect rect = new Rect(
                    new Point((sizeFinal.Width - Child.DesiredSize.Width) / 2,
                              (sizeFinal.Height - Child.DesiredSize.Height) / 2),
                              Child.DesiredSize);
                Child.Arrange(rect);
            }
            return sizeFinal;
        }


        // Override of OnRender.
        protected override void OnRender(DrawingContext dc)
        {
            Point center = new Point(RenderSize.Width / 2, RenderSize.Height / 2);

            double radiusX = RenderSize.Width / 2 - (Stroke?.Thickness ?? 0) / 2;
            double radiusY = RenderSize.Height / 2 - (Stroke?.Thickness ?? 0) / 2;

            radiusX = Math.Max(0, radiusX);
            radiusY = Math.Max(0, radiusY);

            dc.DrawEllipse(Fill, pen: Stroke, center, radiusX, radiusY);

            return;
            FormattedText formtxt = new FormattedText("Desk Text", CultureInfo.CurrentCulture, FlowDirection, new Typeface("Times New Roman Italic"), 24, Brushes.DarkBlue, 1);
            Point ptText = new Point((RenderSize.Width - formtxt.Width) / 2, (RenderSize.Height - formtxt.Height) / 2);
            dc.DrawText(formtxt, ptText);
        }
    }
}
