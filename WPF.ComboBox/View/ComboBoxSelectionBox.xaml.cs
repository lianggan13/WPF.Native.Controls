using System.Windows;
using System.Windows.Controls;

namespace WPF.ComboBox.View
{
    /// <summary>
    /// Interaction logic for ComboBoxSelectionBox.xaml
    /// </summary>

    public partial class ComboBoxSelectionBox : UserControl
    {

        public ComboBoxSelectionBox()
        {
            InitializeComponent();

            lstProducts.ItemsSource = App.StoreDb.GetProducts();
            // Select the first item.
            lstProducts.SelectedIndex = 0;
        }

        private void txtTextSearchPath_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void txtTextSearchPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Re-select the so the new TextSearch.TextPath is evaluated.
            int currentIndex = lstProducts.SelectedIndex;
            lstProducts.SelectedIndex = -1;
            lstProducts.SelectedIndex = currentIndex;
        }
    }
}