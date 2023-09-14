using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF.Common.Database;

namespace WPF.ViewCollection.View
{
    /// <summary>
    /// GroupViewCollection.xaml 的交互逻辑
    /// </summary>
    public partial class GroupViewCollection : UserControl
    {
        public GroupViewCollection()
        {
            InitializeComponent();
        }

        private ICollection<Product> products;

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDb.GetProducts();
            lstProducts.ItemsSource = products;

            ICollectionView view = CollectionViewSource.GetDefaultView(lstProducts.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("CategoryName", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("ModelName", ListSortDirection.Ascending));

            view.GroupDescriptions.Add(new PropertyGroupDescription("CategoryName"));
            var groups = view.Groups.ToList();
            var g = groups[0];
        }
    }
}
