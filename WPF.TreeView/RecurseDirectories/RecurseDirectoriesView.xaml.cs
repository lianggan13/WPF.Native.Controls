using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WPF.TreeView.RecurseDirectories
{
    /// <summary>
    /// RecurseDirectoriesView.xaml 的交互逻辑
    /// </summary>
    public partial class RecurseDirectoriesView : UserControl
    {
        public RecurseDirectoriesView()
        {
            InitializeComponent();
        }

        private void DirectoryTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Get selected item.
            DirectoryTreeViewItem item = e.NewValue as DirectoryTreeViewItem;

            // Clear out the DockPanel.
            stack.Children.Clear();

            // Fill it up again.
            FileInfo[] infos;

            try
            {
                infos = item.DirectoryInfo.GetFiles();
            }
            catch
            {
                return;
            }

            foreach (FileInfo info in infos)
            {
                TextBlock text = new TextBlock();
                text.Text = info.Name;
                stack.Children.Add(text);
            }
        }
    }
}
