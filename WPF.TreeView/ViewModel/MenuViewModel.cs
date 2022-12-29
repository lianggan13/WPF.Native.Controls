using System.Collections.Generic;
using System.Windows;
using WPF.TreeView.Model;
using WPFCommon.MVVMFoundation;

namespace WPF.TreeView.ViewModel
{
    public class MenuViewModel : NotifyPropertyChanged
    {
        public List<MenuItemModel> TreeList { get; set; }

        public RelayCommand<MenuItemModel> OpenViewCommand => new RelayCommand<MenuItemModel>(OpenView, (item) => item.Children.Count == 0);


        public MenuViewModel()
        {
            TreeList = new List<MenuItemModel>();
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
        }

        private void OpenView(MenuItemModel item)
        {
            MessageBox.Show(item.Header);
        }


    }
}
