using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WPFDataGrid.Common
{
    public class DataGridHelper
    {
        public static ObservableCollection<DataGridColumn> GetColumns(DependencyObject obj)
        {
            return (ObservableCollection<DataGridColumn>)obj.GetValue(ColumnsProperty);
        }

        public static void SetColumns(DependencyObject obj, ObservableCollection<DataGridColumn> value)
        {
            obj.SetValue(ColumnsProperty, value);
        }
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.RegisterAttached("Columns", typeof(ObservableCollection<DataGridColumn>), typeof(DataGridHelper), new PropertyMetadata(null, new PropertyChangedCallback(OnColumnsChanged)));


        private static void OnColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as DataGrid;
            var collection = e.NewValue as ObservableCollection<DataGridColumn>;

            collection.CollectionChanged += (se, ev) =>
            {
                // 当集合变化的时候需要重置列
                dataGrid.Columns.Clear();
                foreach (var item in collection)
                {
                    dataGrid.Columns.Add(item);
                }
            };
            // 当对象实例发生变化的时候需要重置列
            dataGrid.Columns.Clear();
            foreach (var item in collection)
            {
                dataGrid.Columns.Add(item);
            }
        }
    }
}
