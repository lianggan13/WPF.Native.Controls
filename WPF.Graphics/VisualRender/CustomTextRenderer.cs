
using System.Globalization;
using System.Windows;


namespace WPF.Graphics.VisualRender
{
    using System.Windows.Media;
    public class CustomTextRenderer : FrameworkElement
    {
        FormattedText ftext = null;


        public CustomTextRenderer()
        {
            var dpiInfo = VisualTreeHelper.GetDpi(Application.Current.MainWindow);
            var d = dpiInfo.PixelsPerDip;

            ftext = new FormattedText("Hello, world",
               CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
               new Typeface("Verdana"), 24, Brushes.Black, d);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            double angle = 30;
            double x = 0, y = 0;
            RotateTransform form = new RotateTransform(angle, x, y);
            //drawingContext.Pop();
            drawingContext.PushTransform(form);
            //drawingContext.Pop();

            drawingContext.DrawRectangle(Brushes.Chocolate, null, new Rect(0, 0, 100, 50));

            drawingContext.DrawText(ftext, new Point(0, 0));
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            ftext.MaxTextWidth = availableSize.Width;
            ftext.MaxTextHeight = availableSize.Height;
            return new Size(ftext.Width, ftext.Height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            ftext.MaxTextWidth = finalSize.Width;
            ftext.MaxTextHeight = finalSize.Height;
            return finalSize;
        }
    }
}
