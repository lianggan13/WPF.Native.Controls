using System;
using System.Windows;
using System.Windows.Controls;

namespace WPF.DatePicker.Views
{
    /// <summary>
    /// MonthPickerView.xaml 的交互逻辑
    /// </summary>
    public partial class MonthPickerView : UserControl
    {
        public MonthPickerView()
        {
            InitializeComponent();
        }

        // Handler for DatePicker DateChanged event.
        void DatePickerOnDateChanged(object sender,
                        RoutedPropertyChangedEventArgs<DateTime?> args)
        {
            if (args.NewValue != null)
            {
                DateTime dt = (DateTime)args.NewValue;
                txtblkDate.Text = dt.ToString("d");
            }
            else
                txtblkDate.Text = "";
        }
    }
}
