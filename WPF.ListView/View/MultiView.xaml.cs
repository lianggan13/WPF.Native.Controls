using System.Windows.Controls;

namespace WPF.ListView.View
{
    /// <summary>
    /// Interaction logic for MultiView.xaml
    /// </summary>

    public partial class MultiView : UserControl
    {
        public MultiView()
        {
            InitializeComponent();

            lstProducts.ItemsSource = App.StoreDb.GetProducts();

        }
        private void lstView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)lstView.SelectedItem;
            lstProducts.View = (ViewBase)this.FindResource(selectedItem.Content);
        }
    }
}