using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class DialogWindow : System.Windows.Window
    {
        public DialogWindow()
        {
            InitializeComponent();

            openFileDialogButton.Click += new RoutedEventHandler(openFileDialogButton_Click);
            saveFileDialogButton.Click += new RoutedEventHandler(saveFileDialogButton_Click);
            folderBrowserDialogButton.Click += new RoutedEventHandler(folderBrowserDialogButton_Click);
            fontDialogButton.Click += new RoutedEventHandler(fontDialogButton_Click);
            colorDialogButton.Click += new RoutedEventHandler(colorDialogButton_Click);
            pageSetupDialogButton.Click += new RoutedEventHandler(pageSetupDialogButton_Click);
            printDialogButton.Click += new RoutedEventHandler(printDialogButton_Click);
            //printPreviewDialogButton.Click += new RoutedEventHandler(printPreviewDialogButton_Click);
        }

        //void printPreviewDialogButton_Click(object sender, RoutedEventArgs e) {
        //  System.Windows.Forms.PrintPreviewDialog dlg = new System.Windows.Forms.PrintPreviewDialog();
        //  dlg.ShowDialog();
        //}

        void printDialogButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dlg = new PrintDialog();
            if (dlg.ShowDialog() == true)
            {
                MessageBox.Show("Now would be a good time to print something...", "Printing");
            }
        }

        void pageSetupDialogButton_Click(object sender, RoutedEventArgs e)
        {
            // Creating the PageSetupDialog in code
            //System.Windows.Forms.PageSetupDialog dlg = new System.Windows.Forms.PageSetupDialog();
            //dlg.PageSettings = new System.Drawing.Printing.PageSettings();

            // Get the PageSetupDialog created in XAML
            System.Windows.Forms.PageSetupDialog dlg = (System.Windows.Forms.PageSetupDialog)Resources["printSetupDialog"];

            string prefix = "Page Setup Dialog: ";
            dlg.PageSettings.Landscape = ((string)pageSetupDialogButton.Content).EndsWith("Landscape");
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pageSetupDialogButton.Content = prefix + "Orientation=" + (dlg.PageSettings.Landscape ? "Landscape" : "Portrait");
            }
        }

        void colorDialogButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the color from a WPF element
            Color wpfColor = ((SolidColorBrush)colorDialogButton.Background).Color;

            // Convert WPF color to GDI+ color that the Windows Forms ColorDialog uses
            System.Drawing.Color gdiColor =
              System.Drawing.Color.FromArgb(wpfColor.A, wpfColor.R, wpfColor.G, wpfColor.B);

            // Initialize and show the color dialog using a GDI+ color
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.Color = gdiColor;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                gdiColor = dlg.Color;

                // Convert GDI+ color back to WPF color
                wpfColor = Color.FromArgb(gdiColor.A, gdiColor.R, gdiColor.G, gdiColor.B);

                // Set the color on a WPF element
                colorDialogButton.Background = new SolidColorBrush(wpfColor);
            }
        }

        System.Drawing.Font GetGdiFontFromWpfControl(Control control)
        {
            // Map the normal, bold and italics styles
            System.Drawing.FontStyle style = System.Drawing.FontStyle.Regular;
            if (control.FontWeight >= FontWeights.Bold)
            {
                style |= System.Drawing.FontStyle.Bold;
            }

            if (control.FontStyle == FontStyles.Italic)
            {
                style |= System.Drawing.FontStyle.Italic;
            }

            // Map the family name and point size (converting from device-independent pixels to points)
            System.Drawing.Font
              font = new System.Drawing.Font(
                control.FontFamily.ToString(), (float)(control.FontSize * 72) / 96, style);

            return font;
        }

        void SetGdiFontOnWpfControl(Control control, System.Drawing.Font font)
        {
            control.FontFamily = new FontFamily(font.FontFamily.Name);
            control.FontSize = (font.SizeInPoints * 96) / 72; // converting from points to device-independent pixels
            control.FontStretch = FontStretches.Normal; // No mapping
            control.FontStyle = (font.Italic ? FontStyles.Italic : FontStyles.Normal);
            control.FontWeight = (font.Bold ? FontWeights.Bold : FontWeights.Normal);
        }

        void fontDialogButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the GDI+ font from a WPF control
            System.Drawing.Font gdiFont = GetGdiFontFromWpfControl(fontDialogButton);

            // Initialize and show the font dialog using the GDI+ font
            System.Windows.Forms.FontDialog dlg = new System.Windows.Forms.FontDialog();
            dlg.Font = gdiFont;

            // no ready mapping of strikeout or underline to WPF,
            // so turn it off (also turns off the font color setting)
            dlg.ShowEffects = false;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Set the GDI+ font settings on a WPF control
                SetGdiFontOnWpfControl(fontDialogButton, dlg.Font);
            }
        }

        void folderBrowserDialogButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            string prefix = "Folder Browser Dialog: ";
            if (((string)folderBrowserDialogButton.Content).Length > prefix.Length)
            {
                dlg.SelectedPath = ((string)folderBrowserDialogButton.Content).Substring(prefix.Length);
            }

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderBrowserDialogButton.Content = prefix + dlg.SelectedPath;
            }
        }

        void saveFileDialogButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            string prefix = "Save File Dialog: ";
            if (((string)saveFileDialogButton.Content).Length > prefix.Length)
            {
                dlg.FileName = ((string)saveFileDialogButton.Content).Substring(prefix.Length);
            }

            if (dlg.ShowDialog() == true)
            {
                saveFileDialogButton.Content = prefix + dlg.FileName;
            }
        }

        void openFileDialogButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            string prefix = "Open File Dialog: ";
            if (((string)openFileDialogButton.Content).Length > prefix.Length)
            {
                dlg.FileName = ((string)openFileDialogButton.Content).Substring(prefix.Length);
            }

            if (dlg.ShowDialog() == true)
            {
                openFileDialogButton.Content = prefix + dlg.FileName;
            }
        }

    }
}
