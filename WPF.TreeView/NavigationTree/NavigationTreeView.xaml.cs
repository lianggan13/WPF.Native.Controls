
namespace WPF.TreeView.NavigationTree
{

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using WPFCommon.Assets.Compent;
    using WPFCommon.MVVMFoundation;

    /// <summary>
    /// NavigationTreeView.xaml 的交互逻辑
    /// </summary>
    public partial class NavigationTreeView : UserControl
    {
        public static List<MenuItemModel> TreeList { get; private set; } = new List<MenuItemModel>();

        public RelayCommand<MenuItemModel> OpenViewCommand => new RelayCommand<MenuItemModel>(OpenView, (item) => item.Children.Count == 0);


        static NavigationTreeView()
        {
            MenuItemModel tim = new MenuItemModel();
            tim.Header = "工艺设计";
            //&#xe740;  XAML里使用
            tim.IconCode = "\ue610"; // 字体图标编码，阿里的Iconfont平台打包的图标库
            TreeList.Add(tim);
            tim.Children.Add(new MenuItemModel
            {
                Header = "加工工艺",
                TargetView = "BlankPage",
            });
            tim.Children.Add(new MenuItemModel
            {
                Header = "EBOM",
                TargetView = "BlankPage",
            });
            tim.Children.Add(new MenuItemModel
            {
                Header = "设备看板",
                TargetView = "DevicePage",
            });

            tim.Children.Add(new MenuItemModel
            {
                Header = "PBOM",
                TargetView = "PBomPage",
            });
            MenuItemModel subMenu = new MenuItemModel();
            subMenu.Header = "二级菜单";
            subMenu.Children.Add(
                new MenuItemModel
                {
                    Header = "三级菜单"
                }
               );
            tim.Children.Add(subMenu);
        }

        public NavigationTreeView()
        {
            InitializeComponent();

        }

        private void OpenView(MenuItemModel item)
        {
            MessageBox.Show(item.Header);
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
            TreeList.ForEach(t =>
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


    public class MenuItemModel : NotifyPropertyChanged
    {
        public string IconCode { get; set; }

        public string Header { get; set; }

        public string TargetView { get; set; }

        public ObservableCollection<MenuItemModel> Children { get; set; } = new ObservableCollection<MenuItemModel>();

        private bool isDragOver = false;
        public bool IsDragOver
        {
            get { return isDragOver; }
            set { SetProperty(ref isDragOver, value); }
        }
    }
}
