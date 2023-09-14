using System.Windows.Controls;

namespace WPF.DataGrid.View
{
    /// <summary>
    /// Interaction logic for DataGridRowDetails.xaml
    /// </summary>
    public partial class DataGridRowDetails : UserControl
    {
        public DataGridRowDetails()
        {
            InitializeComponent();

            gridProducts.ItemsSource = App.StoreDb.GetProducts();
        }
    }
}
