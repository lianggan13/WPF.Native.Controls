using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace WPF.DataGrid.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataGridSorting : UserControl
    {
        public DataGridSorting()
        {
            InitializeComponent();
        }

        public static List<object> List { get; set; } = new List<object>()
        {
            new { Name = "张三", Age = 20, date = DateTime.Now, Sex = "男" },
            new { Name = "李四", Age = 10, date = DateTime.Now, Sex = "男" },
            new { Name = "王五", Age = 30, date = DateTime.Now, Sex = "男" },
            new { Name = "赵六", Age = 50, date = DateTime.Now, Sex = "男" },
            new { Name = "小明", Age = 70, date = DateTime.Now, Sex = "男" },
            new { Name = "小红", Age = 12, date = DateTime.Now, Sex = "女" },
            new { Name = "小绿", Age = 40, date = DateTime.Now, Sex = "男" },
            new { Name = "老王", Age = 33, date = DateTime.Now, Sex = "男" },
        };

        private void ContextMenu_Opened(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ContextMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

        private void DataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            e.Handled = true;
        }
    }
}
