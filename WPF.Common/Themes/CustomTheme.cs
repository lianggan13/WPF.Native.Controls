using System.Windows;

namespace WPF.Common.Themes
{
    public class CustomTheme
    {
        public static ComponentResourceKey DogBrushKey
        {
            get { return new ComponentResourceKey(typeof(CustomTheme), nameof(DogBrushKey)); }
        }
    }
}
