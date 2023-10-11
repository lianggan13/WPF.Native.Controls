//----------------------------------------------------
// FormatRichText.Edit.cs (c) 2006 by Charles Petzold
//----------------------------------------------------


namespace WPF.ToolBar.FormatRichText
{

    using System;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public partial class FormatRichTextView : UserControl
    {
        void AddEditToolBar(ToolBarTray tray, int band, int index)
        {
            // Create Toolbar.
            ToolBar toolbar = new ToolBar();
            toolbar.Band = band;
            toolbar.BandIndex = index;
            tray.ToolBars.Add(toolbar);

            RoutedUICommand[] comm =
                {
                    ApplicationCommands.Cut, ApplicationCommands.Copy,
                    ApplicationCommands.Paste, ApplicationCommands.Delete,
                    ApplicationCommands.Undo, ApplicationCommands.Redo
                };

            string[] strImages =
                {
                    "CutHS.png", "CopyHS.png",
                    "PasteHS.png", "DeleteHS.png",
                    "Edit_UndoHS.png", "Edit_RedoHS.png"
                };


            for (int i = 0; i < 6; i++)
            {
                if (i == 4)
                    toolbar.Items.Add(new Separator());

                Button btn = new Button();
                btn.Command = comm[i];
                toolbar.Items.Add(btn);

                Image img = new Image();
                img.Source = new BitmapImage(
                        new Uri("pack://application:,,,/WPF.ToolBar;component/Assets/Images/" + strImages[i]));
                img.Stretch = Stretch.None;
                btn.Content = img;

                ToolTip tip = new ToolTip();
                tip.Content = comm[i].Text;
                btn.ToolTip = tip;
            }

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste));
            CommandBindings.Add(new CommandBinding(
                            ApplicationCommands.Delete, OnDelete, CanDelete));

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Undo));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Redo));
        }
        void CanDelete(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = !txtbox.Selection.IsEmpty;
        }
        void OnDelete(object sender, ExecutedRoutedEventArgs args)
        {
            txtbox.Selection.Text = "";
        }
    }
}
