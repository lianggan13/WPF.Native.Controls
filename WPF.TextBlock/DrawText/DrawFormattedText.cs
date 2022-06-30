using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace WPF.TextBlock.DrawText
{
    public class DrawFormattedText : FrameworkElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            // Example 14-22. Typeface

            FontFamily preferredFont = new FontFamily("Candara");
            FontFamily fallbackFont = new FontFamily("Verdana");

            Typeface tf = new Typeface(
                preferredFont,
                FontStyles.Italic,
                FontWeights.Bold,
                FontStretches.Normal,
                fallbackFont);

            // End of Example 14-22.

            FormattedText text = new FormattedText(
                "I抦 not a pheasant plucker, I抦 the pheasant plucker抯 son, " +
                    "and I抦 only plucking pheasants 憈il the pheasant plucker comes.",
                Thread.CurrentThread.CurrentUICulture,
                FlowDirection.LeftToRight,
                tf,
                24,    // Font size in pixels
                Brushes.Black);

            text.MaxTextWidth = this.ActualWidth;
            text.MaxTextHeight = this.ActualHeight;


            // Example 14-23. Simple underline decoration

            text.SetTextDecorations(TextDecorations.Underline);

            // End of Example 14-23.

            drawingContext.DrawText(text, new Point(0, 0));
        }
    }
}
