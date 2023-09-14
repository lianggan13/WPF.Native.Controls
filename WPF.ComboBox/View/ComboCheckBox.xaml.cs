using System;
using System.Linq;

namespace WPF.ComboBox.View
{
    using System.Windows.Controls;

    /// <summary>
    /// SoloTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class ComboCheckBox : ComboBox
    {
        public ComboCheckBox()
        {
            InitializeComponent();
        }


        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            e.Handled = true;
            //base.OnSelectionChanged(e);
        }
        protected override void OnDropDownClosed(EventArgs e)
        {
            this.Text = string.Join(",", ItemsSource.OfType<IComboCheckBoxItem>().Where(f => f.IsChecked).Select(f => f.Content));
            base.OnDropDownClosed(e);
        }
    }

    public interface IComboCheckBoxItem
    {
        object Content { get; }
        bool IsChecked { get; set; }
    }
}
