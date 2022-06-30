using System.ComponentModel;
using System.Windows;
using System.Windows.Media;


namespace WPF.TextBlock.DrawText
{
    public class DrawGlyph : FrameworkElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            GlyphRun run = BuildGlyphRun("Hello, world!");
            if (run != null)
            {
                drawingContext.DrawGlyphRun(Brushes.Black, run);
            }
        }

        public static GlyphRun BuildGlyphRun(string text)
        {
            double fontSize = 50;
            GlyphRun glyphs = null;

            Typeface font = new Typeface("Calibri");
            GlyphTypeface glyphFace;
            if (font.TryGetGlyphTypeface(out glyphFace))
            {
                glyphs = new GlyphRun();
                ISupportInitialize isi = glyphs;
                isi.BeginInit();
                glyphs.GlyphTypeface = glyphFace;
                glyphs.FontRenderingEmSize = fontSize;

                char[] textChars = text.ToCharArray();
                glyphs.Characters = textChars;
                ushort[] glyphIndices = new ushort[textChars.Length];
                double[] advanceWidths = new double[textChars.Length];

                for (int i = 0; i < textChars.Length; ++i)
                {
                    int codepoint = textChars[i];
                    ushort glyphIndex = glyphFace.CharacterToGlyphMap[codepoint];
                    double glyphWidth = glyphFace.AdvanceWidths[glyphIndex];

                    glyphIndices[i] = glyphIndex;
                    advanceWidths[i] = glyphWidth * fontSize;
                }
                glyphs.GlyphIndices = glyphIndices;
                glyphs.AdvanceWidths = advanceWidths;

                glyphs.BaselineOrigin = new Point(0, glyphFace.Baseline * fontSize);
                isi.EndInit();
            }
            return glyphs;
        }
    }
}
