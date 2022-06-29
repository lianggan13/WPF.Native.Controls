using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPF.TextBlock.FlowDocuments
{
    /// <summary>
    /// ControlViewer.xaml 的交互逻辑
    /// </summary>
    public partial class ControlViewer : Page
    {
        public ControlViewer()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            scViewer.Document = (FlowDocument)Application.LoadComponent(
                new Uri("./FlowDocuments/MyFlowDocument.xaml", UriKind.Relative));

            pgViewer.Document = (FlowDocument)Application.LoadComponent(
               new Uri("./FlowDocuments/MyFlowDocument.xaml", UriKind.Relative));

            reader.Document = (FlowDocument)Application.LoadComponent(
               new Uri("./FlowDocuments/MyFlowDocument.xaml", UriKind.Relative));
        }
    }
}
