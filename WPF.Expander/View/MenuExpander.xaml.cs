using System.Collections.ObjectModel;
using System.Windows.Controls;
using WPF.Expander.Model;

namespace WPF.Expander.View
{
    /// <summary>
    /// MenuExpander.xaml 的交互逻辑
    /// </summary>
    public partial class MenuExpander : Page
    {
        public MenuExpander()
        {
            InitializeComponent();
        }

        public ObservableCollection<MenuItemModel> MenuItems { get; set; } = new ObservableCollection<MenuItemModel>()
        {
            new MenuItemModel()
            {
                Title ="系统设置",
                SubItems =new ObservableCollection<MenuItemModel>()
                {
                    new MenuItemModel()
                    {
                        Title ="网络",
                    },
                    new MenuItemModel()
                    {
                        Title="音频",
                    }
                }
            },
            new MenuItemModel()
            {
                Title ="主要服务",
                SubItems =new ObservableCollection<MenuItemModel>()
                {
                    new MenuItemModel()
                    {
                        Title ="实时播放",
                    },
                    new MenuItemModel()
                    {
                        Title="回放",
                    }
                }
            },
            new MenuItemModel()
            {
                Title ="关闭",
            }
        };
    }
}
