using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WPF.Virtualization.UIVirtualization
{
    /// <summary>
    /// PendingUI.xaml 的交互逻辑
    /// </summary>
    public partial class PendingUI : UserControl
    {
        public PendingUI()
        {
            InitializeComponent();
        }

        private void itemsControl_Loaded(object sender, RoutedEventArgs e)
        {
            //ItemsControl 中的 BeginInit 和 EndInit 主要是为了提高性能和减少布局更新。

            //它们的作用是:

            //  1.通过BeginInit()标记,ItemsControl 将停止响应 CollectionChanged 事件。

            //  2.在 BeginInit 和EndInit 之间执行的所有ItemsSource或项的添加 / 更新操作,ItemsControl 都不会立即重新布局。

            //  3.调用 EndInit 后, ItemsControl 一次性重新计算布局,更新界面。

            //这可减少重复的布局计算,提高效率。

            itemsControl.BeginInit();

            var sources = new List<int>();

            for (int i = 0; i < 300; i++)
                sources.Add(i);

            itemsControl.ItemsSource = sources;

            itemsControl.EndInit();
        }

        private void itemsControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
