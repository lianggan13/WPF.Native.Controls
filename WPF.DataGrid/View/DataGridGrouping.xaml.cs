using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using WPF.Common.Database;

namespace WPF.DataGrid.View
{
    /// <summary>
    /// Interaction logic for DataGridGrouping.xaml
    /// </summary>
    public partial class DataGridGrouping : UserControl
    {
        public DataGridGrouping()
        {
            InitializeComponent();

            gridProducts.ItemsSource = App.StoreDb.GetProducts();
            ICollectionView view = CollectionViewSource.GetDefaultView(gridProducts.ItemsSource);

            view.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Product.CategoryName)));
        }
    }
}
