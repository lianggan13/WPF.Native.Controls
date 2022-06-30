using System;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;


namespace WPF.TextBlock.DrawText
{
    /// <summary>
    /// DrawTextViewer.xaml 的交互逻辑
    /// </summary>
    public partial class DrawTextViewer : Page
    {
        public DrawTextViewer()
        {
            InitializeComponent();
        }
    }

    public class FontUriExtension : MarkupExtension
    {
        string fontFamilyName;

        public FontUriExtension(string fontFamilyName)
        {
            this.fontFamilyName = fontFamilyName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Typeface tf = new Typeface(this.fontFamilyName);
            GlyphTypeface gtf = null;
            if (!tf.TryGetGlyphTypeface(out gtf))
            {
                throw new ArgumentException("Font family not found");
            }
            return gtf.FontUri;
        }
    }
}
