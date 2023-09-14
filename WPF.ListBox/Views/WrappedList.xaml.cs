using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPF.Common.Database;

namespace WPF.ListBox.View
{
    /// <summary>
    /// Interaction logic for WrappedList.xaml
    /// </summary>

    public partial class WrappedList : UserControl
    {

        public WrappedList()
        {
            InitializeComponent();
        }
        private ICollection<Product> products;

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDb.GetProducts();
            lstProducts.ItemsSource = products;

        }
    }
}