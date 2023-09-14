using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF.Mouse.Views
{
    /// <summary>
    /// DragAndDrop.xaml 的交互逻辑
    /// </summary>
    public partial class DragAndDrop : UserControl
    {
        public DragAndDrop()
        {
            InitializeComponent();
        }

        private void lblSource_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label lbl = (Label)sender;
            DragDrop.DoDragDrop(lbl, lbl.Content, DragDropEffects.Copy);
        }

        private void lblTarget_Drop(object sender, DragEventArgs e)
        {
            ((Label)sender).Content = e.Data.GetData(DataFormats.Text);
        }

        private void lblTarget_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }
    }
}
