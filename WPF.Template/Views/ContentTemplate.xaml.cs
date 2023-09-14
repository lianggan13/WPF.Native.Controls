using System.Windows;
using System.Windows.Controls;
using WPF.Template.Models;

namespace WPF.Template.Views
{
    /// <summary>
    /// ContentTemplate.xaml 的交互逻辑
    /// </summary>
    public partial class ContentTemplate : UserControl
    {
        public ContentTemplate()
        {
            InitializeComponent();
        }

        private void EmployeeButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            Employee emp = btn.Content as Employee;
            MessageBox.Show(emp.Name + " button clicked!");
        }
    }
}
