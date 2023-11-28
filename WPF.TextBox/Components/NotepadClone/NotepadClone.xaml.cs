using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF.TextBox.Components.NotepadClone
{
    /// <summary>
    /// NotepadClone.xaml 的交互逻辑
    /// </summary>
    public partial class NotepadClone : Window
    {
        protected string strAppTitle;       // Name of program for title bar.
        protected string strAppData;        // Full file name of settings file.
        protected NotepadCloneSettings settings;    // Settings.
        protected bool isFileDirty = false; // Flag for file save prompt.


        string strLoadedFile;               // Fully qualified loaded file name.

        public NotepadClone()
        {
            InitializeComponent();

            Assembly asmbly = Assembly.GetExecutingAssembly();

            // Get the AssemblyTitle attribute for the strAppTitle field.
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)asmbly.
                GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];
            strAppTitle = title.Title;

            // Get the AssemblyProduct attribute for the strAppData file name.
            AssemblyProductAttribute product = (AssemblyProductAttribute)asmbly.
                GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0];
            strAppData = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                        "Petzold\\" + product.Product + "\\" +
                        product.Product + ".Settings.xml");

            // Create all the top-level menu items.
            AddFileMenu(menu);          // in NotepadClone.File.cs
            AddEditMenu(menu);          // in NotepadClone.Edit.cs
            AddFormatMenu(menu);        // in NotepadClone.Format.cs
            AddViewMenu(menu);          // in NotepadClone.View.cs
            AddHelpMenu(menu);          // in NotepadClone.Help.cs

            // Load settings saved from previous run.
            settings = (NotepadCloneSettings)LoadSettings();

            // Apply saved settings.
            WindowState = settings.WindowState;

            if (settings.RestoreBounds != Rect.Empty)
            {
                Left = settings.RestoreBounds.Left;
                Top = settings.RestoreBounds.Top;
                Width = settings.RestoreBounds.Width;
                Height = settings.RestoreBounds.Height;
            }

            txtbox.TextWrapping = settings.TextWrapping;
            txtbox.FontFamily = new FontFamily(settings.FontFamily);
            txtbox.FontStyle = (FontStyle)new FontStyleConverter().
                                ConvertFromString(settings.FontStyle);
            txtbox.FontWeight = (FontWeight)new FontWeightConverter().
                                ConvertFromString(settings.FontWeight);
            txtbox.FontStretch = (FontStretch)new FontStretchConverter().
                                ConvertFromString(settings.FontStretch);
            txtbox.FontSize = settings.FontSize;

            // Install handler for Loaded event.

            // Set focus to TextBox.
            txtbox.Focus();
        }
        // Overridable method to load settings, called from constructor.
        protected virtual object LoadSettings()
        {
            return NotepadCloneSettings.Load(typeof(NotepadCloneSettings),
                                             strAppData);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ApplicationCommands.New.Execute(null, this);

            // Get command-line arguments.
            string[] strArgs = Environment.GetCommandLineArgs();

            if (strArgs.Length > 1)         // First argument is program name!
            {
                if (File.Exists(strArgs[1]))
                {
                    LoadFile(strArgs[1]);
                }
                else
                {
                    MessageBoxResult result =
                        MessageBox.Show("Cannot find the " +
                            Path.GetFileName(strArgs[1]) + " file.\r\n\r\n" +
                            "Do you want to create a new file?",
                            strAppTitle, MessageBoxButton.YesNoCancel,
                            MessageBoxImage.Question);

                    // Close the window if the user clicks "Cancel".
                    if (result == MessageBoxResult.Cancel)
                        Close();

                    // Create and close file for "Yes".
                    else if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            File.Create(strLoadedFile = strArgs[1]).Close();
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show("Error on File Creation: " +
                                            exc.Message, strAppTitle,
                                MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            return;
                        }
                        UpdateTitle();
                    }
                    // No action for "No".
                }
            }
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            base.OnClosing(args);
            args.Cancel = !OkToTrash();
            settings.RestoreBounds = RestoreBounds;
        }
        // OnClosed event: Set fields of 'settings' and call SaveSettings.
        protected override void OnClosed(EventArgs args)
        {
            base.OnClosed(args);
            settings.WindowState = WindowState;
            settings.TextWrapping = txtbox.TextWrapping;

            settings.FontFamily = txtbox.FontFamily.ToString();
            settings.FontStyle =
                new FontStyleConverter().ConvertToString(txtbox.FontStyle);
            settings.FontWeight =
                new FontWeightConverter().ConvertToString(txtbox.FontWeight);
            settings.FontStretch =
                new FontStretchConverter().ConvertToString(txtbox.FontStretch);
            settings.FontSize = txtbox.FontSize;

            SaveSettings();
        }
        // Overridable method to call Save in the 'settings' object.
        protected virtual void SaveSettings()
        {
            settings.Save(strAppData);
        }
        // UpdateTitle displays file name or "Untitled".
        protected void UpdateTitle()
        {
            if (strLoadedFile == null)
                Title = "Untitled - " + strAppTitle;
            else
                Title = Path.GetFileName(strLoadedFile) + " - " + strAppTitle;
        }

        // When the TextBox text changes, just set isFileDirty.
        void TextBoxOnTextChanged(object sender, RoutedEventArgs args)
        {
            isFileDirty = true;
        }
        // When the selection changes, update the status bar.
        void TextBoxOnSelectionChanged(object sender, RoutedEventArgs args)
        {
            int iChar = txtbox.SelectionStart;
            int iLine = txtbox.GetLineIndexFromCharacterIndex(iChar);

            // Check for error that may be a bug.
            if (iLine == -1)
            {
                statLineCol.Content = "";
                return;
            }

            int iCol = iChar - txtbox.GetCharacterIndexFromLineIndex(iLine);
            string str = String.Format("Line {0} Col {1}", iLine + 1, iCol + 1);

            if (txtbox.SelectionLength > 0)
            {
                iChar += txtbox.SelectionLength;
                iLine = txtbox.GetLineIndexFromCharacterIndex(iChar);
                iCol = iChar - txtbox.GetCharacterIndexFromLineIndex(iLine);
                str += String.Format(" - Line {0} Col {1}", iLine + 1, iCol + 1);
            }
            statLineCol.Content = str;
        }


    }
}
