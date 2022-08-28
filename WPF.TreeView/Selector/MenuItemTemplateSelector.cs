using System.Windows;
using System.Windows.Controls;
using WPF.TreeView.Model;

namespace WPF.TreeView.Selector
{
    public class MenuItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is MenuItemModel menu && container is FrameworkElement element)
            {
                if (menu.SubItems != null && menu.SubItems.Count > 0)
                {
                    return element.FindResource("MenuItemsTemplate") as DataTemplate;
                }
                else
                {
                    return element.FindResource("MenuItemNodeTemplate") as DataTemplate;
                }
            }

            return null;

        }

    }
}
