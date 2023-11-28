using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF.TextBox.View
{
    /// <summary>
    /// TextBoxListerView.xaml 的交互逻辑
    /// </summary>
    public partial class TextBoxListerView : UserControl
    {
        public TextBoxListerView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize FontFamily box with system font families.
            foreach (FontFamily fam in Fonts.SystemFontFamilies.OrderBy(f => f.ToString()))
                boxFamily.Add(fam);

            boxFamily.Focus();
        }
    }
}
