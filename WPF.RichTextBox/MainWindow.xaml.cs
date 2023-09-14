using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace WPF.RichTextBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtbox.Focus();
        }
        string strFilter =
            "Document Files(*.txt)|*.txt|All files (*.*)|*.*";

        protected override void OnPreviewTextInput(TextCompositionEventArgs args)
        {
            if (args.ControlText.Length > 0 && args.ControlText[0] == '\x0F') // Ctrl + O
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.CheckFileExists = true;
                dlg.Filter = strFilter;

                if ((bool)dlg.ShowDialog(this))
                {
                    FlowDocument flow = txtbox.Document;
                    TextRange range = new TextRange(flow.ContentStart,
                                                    flow.ContentEnd);
                    Stream strm = null;

                    try
                    {
                        strm = new FileStream(dlg.FileName, FileMode.Open);
                        //range.Load(strm, DataFormats.Xaml);
                        range.Load(strm, DataFormats.Text);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, Title);
                    }
                    finally
                    {
                        if (strm != null)
                            strm.Close();
                    }
                }

                args.Handled = true;
            }
            if (args.ControlText.Length > 0 && args.ControlText[0] == '\x13') // Ctrl + S
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = strFilter;

                if ((bool)dlg.ShowDialog(this))
                {
                    FlowDocument flow = txtbox.Document;
                    TextRange range = new TextRange(flow.ContentStart,
                                                    flow.ContentEnd);
                    Stream strm = null;

                    try
                    {
                        strm = new FileStream(dlg.FileName, FileMode.Create);
                        //range.Save(strm, DataFormats.Xaml);
                        range.Save(strm, DataFormats.Text);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, Title);
                    }
                    finally
                    {
                        if (strm != null)
                            strm.Close();
                    }
                }
                args.Handled = true;
            }
            base.OnPreviewTextInput(args);
        }
    }
}
