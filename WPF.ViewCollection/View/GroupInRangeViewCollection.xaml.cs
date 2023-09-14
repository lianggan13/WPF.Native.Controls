using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF.Common.Database;

namespace WPF.ViewCollection.View
{
    /// <summary>
    /// GroupInRangeViewCollection.xaml 的交互逻辑
    /// </summary>
    public partial class GroupInRangeViewCollection : UserControl
    {
        public GroupInRangeViewCollection()
        {
            InitializeComponent();
        }

        private ICollection<Product> products;

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDb.GetProducts();
            CollectionViewSource viewSource = (CollectionViewSource)this.FindResource("GroupByRangeView");
            viewSource.Source = products;
            //lstProducts.ItemsSource = products;

            //ICollectionView view = CollectionViewSource.GetDefaultView(lstProducts.ItemsSource);
            //view.SortDescriptions.Add(new SortDescription("UnitCost", ListSortDirection.Ascending));
            //PriceRangeProductGrouper grouper = new PriceRangeProductGrouper();
            //grouper.GroupInterval = 50;
            //view.GroupDescriptions.Add(new PropertyGroupDescription("UnitCost", grouper));
        }
    }

    public class PriceRangeProductGrouper : IValueConverter
    {
        public int GroupInterval
        {
            get;
            set;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal price = (decimal)value;
            if (price < GroupInterval)
            {
                return String.Format("Less than {0:C}", GroupInterval);
            }
            else
            {
                int interval = (int)price / GroupInterval;
                int lowerLimit = interval * GroupInterval;
                int upperLimit = (interval + 1) * GroupInterval;
                return String.Format("{0:C} to {1:C}", lowerLimit, upperLimit);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("This converter is for grouping only.");
        }
    }
}
