using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WPF.Common.Database;

namespace WPF.ListBox.View
{
    /// <summary>
    /// Interaction logic for VariedStyles.xaml
    /// </summary>

    public partial class VariedStyles : UserControl
    {

        public VariedStyles()
        {
            InitializeComponent();
        }

        private ICollection<Product> products;

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDb.GetProducts();
            lstProducts.ItemsSource = products;
        }

        private void cmdApplyChange_Click(object sender, RoutedEventArgs e)
        {
            ((ObservableCollection<Product>)products)[1].CategoryName = "Travel";
            StyleSelector selector = lstProducts.ItemContainerStyleSelector;
            lstProducts.ItemContainerStyleSelector = null;
            lstProducts.ItemContainerStyleSelector = selector;
        }
    }

}