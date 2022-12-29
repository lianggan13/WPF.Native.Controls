using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WPFCommon.Assets.Compent;

namespace WPF.TreeView.View
{
    using System.Diagnostics;
    using System.Windows.Controls;
    using WPF.TreeView.Model;

    /// <summary>
    /// MenuTree.xaml 的交互逻辑
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
        }


        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);

            return source;
        }

        private void TaskTree_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tree = sender as TreeView;
            if (ControlHelper.GetHitUIElement<TreeViewItem>(tree, e.GetPosition(tree)) is TreeViewItem tvItem)
            {
                tvItem.Focus();
                DragDrop.DoDragDrop(tree, new DataObject(tvItem), DragDropEffects.Move);

                //if (tvItem.DataContext is TestTaskBase ttb)
                //{
                //    if (ttb.IsRenaming) return;
                //    tvItem.Focus();
                //}
            }

        }

        private void TaskTree_DragEnter(object sender, DragEventArgs e)
        {
            ClearDragOver();

            (TreeViewItem source, TreeViewItem target) = GetDragObjects(sender, e);
            MenuItemModel t1 = source.DataContext as MenuItemModel;
            MenuItemModel t2 = target.DataContext as MenuItemModel;
            t2.IsDragOver = true;
            //Debug.WriteLine($"{t1?.Header} --> {t2?.Header}");
        }

        private void ClearDragOver()
        {
            vm.TreeList.ForEach(t =>
            {
                t.IsDragOver = false;
                foreach (var c in t?.Children)
                {
                    c.IsDragOver = false;
                }
            });

        }

        private void TaskTree_DragOver(object sender, DragEventArgs e)
        {

        }


        private void TaskTree_Drop(object sender, DragEventArgs e)
        {
            (TreeViewItem source, TreeViewItem target) = GetDragObjects(sender, e);
            MenuItemModel t1 = source.DataContext as MenuItemModel;
            MenuItemModel t2 = target.DataContext as MenuItemModel;
            ClearDragOver();

            // TODO: move colletion...
            Debug.WriteLine($"{t1?.Header} --> {t2?.Header}");
        }

        private void TaskTree_DragLeave(object sender, DragEventArgs e)
        {
            ClearDragOver();
        }


        private (TreeViewItem source, TreeViewItem target) GetDragObjects(object sender, DragEventArgs e)
        {
            var tree = sender as TreeView;

            TreeViewItem source = e.Data.GetData(typeof(TreeViewItem)) as TreeViewItem;
            TreeViewItem target = ControlHelper.GetHitUIElement<TreeViewItem>(tree, e.GetPosition(tree));

            //return !(source == null || target == null || ReferenceEquals(source, target));

            return (source, target);

        }


    }
}
