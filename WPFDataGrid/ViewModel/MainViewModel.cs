using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WPFCommon.MVVMFoundation;
using WPFDataGrid.Model;

namespace WPFDataGrid.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<RowDataModel> RowsData { get; set; } = new ObservableCollection<RowDataModel>();
        public ObservableCollection<DataGridColumn> Columns { get; set; } = new ObservableCollection<DataGridColumn>();

        public MainViewModel()
        {
            RowsData.Add(new RowDataModel { Value1 = "AAA", Value2 = "BBB", Value3 = "CCC", Value4 = "BBB", });
            RowsData.Add(new RowDataModel { Value1 = "AAA", Value2 = "BBB", Value3 = "CCC", Value4 = "BBB", });
            RowsData.Add(new RowDataModel { Value1 = "AAA", Value2 = "BBB", Value3 = "CCC", Value4 = "BBB", });
            RowsData.Add(new RowDataModel { Value1 = "AAA", Value2 = "BBB", Value3 = "CCC", Value4 = "BBB", });
            RowsData.Add(new RowDataModel { Value1 = "AAA", Value2 = "BBB", Value3 = "CCC", Value4 = "BBB", });


            Columns.Add(new DataGridTextColumn { Header = "Column-1", Binding = new Binding("Value1") });
            Columns.Add(new DataGridTextColumn { Header = "Column-2", Binding = new Binding("Value2") });
            Columns.Add(new DataGridTextColumn { Header = "Column-3", Binding = new Binding("Value3") });
        }


        public ICommand AddCommand
        {
            get => new RelayCommand(() =>
            {
                Columns.Add(new DataGridTextColumn { Header = $"Column-{4}", Binding = new Binding($"Value{4}") });
            });
        }
    }
}
