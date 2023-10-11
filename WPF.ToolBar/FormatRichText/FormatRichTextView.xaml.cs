using System.Windows.Controls;

namespace WPF.ToolBar.FormatRichText
{
    /// <summary>
    /// FormatRichTextView.xaml 的交互逻辑
    /// </summary>
    public partial class FormatRichTextView : UserControl
    {
        public FormatRichTextView()
        {
            InitializeComponent();


        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            AddFileToolBar(tray, 0, 0);
            AddEditToolBar(tray, 1, 0);
            AddCharToolBar(tray, 2, 0);
            AddParaToolBar(tray, 2, 1);
            AddStatusBar(dock);
        }
    }
}
