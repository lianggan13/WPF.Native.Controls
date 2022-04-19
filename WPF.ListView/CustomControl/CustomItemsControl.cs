using System.Windows;
using System.Windows.Controls;

namespace WPF.ListView.CustomControl
{
    public class CustomItemsControl
     : ItemsControl
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ContentControl();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            // Even wrap other ContentControls
            return false;
        }
    }
}
