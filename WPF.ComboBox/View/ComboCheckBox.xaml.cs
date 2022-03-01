using GTS.MaxSign.Controls.TemplateParam.Model;
using System;
using System.Linq;
using System.Windows.Controls;

namespace GTS.MaxSign.Controls.TemplateParam.View
{
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
            this.Text = string.Join(",", ItemsSource.OfType<ComboCheckBoxItemModel>().Where(f => f.IsChecked).Select(f => f.Content));
            base.OnDropDownClosed(e);
        }

    }
}
