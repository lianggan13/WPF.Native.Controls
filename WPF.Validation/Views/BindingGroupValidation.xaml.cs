using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF.Common.Database;

namespace WPF.Validation.Views
{
    /// <summary>
    /// Interaction logic for BindingGroupValidation.xaml
    /// </summary>
    public partial class BindingGroupValidation : UserControl
    {
        public BindingGroupValidation()
        {
            InitializeComponent();
        }

        private ICollection<Product> products;

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDb.GetProducts();
            lstProducts.ItemsSource = products;
        }

        private void cmdUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            // Make sure update has taken place.
            FocusManager.SetFocusedElement(this, (Button)sender);
        }

        private void txt_LostFocus(object sender, RoutedEventArgs e)
        {
            productBindingGroup.CommitEdit();
        }

        private void lstProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productBindingGroup.CommitEdit();
        }
    }
}
