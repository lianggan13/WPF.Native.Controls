using DevExpress.Xpf.Charts;
using System.Windows;
using System.Windows.Controls;

namespace WPFChart.View
{
    /// <summary>
    /// Testing2DVM.xaml 的交互逻辑
    /// </summary>
    public partial class Testing2DChart : UserControl
    {
        public Testing2DChart()
        {
            InitializeComponent();
            chart.Diagram.Series.CollectionChanged += VM.Series_CollectionChanged;
        }

        private void CheckBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            //chart.Diagram.Series.Count
        }

        private void CheckBox_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // e.Handled = true;
        }

        private void XYDiagram2D_Zoom(object sender, DevExpress.Xpf.Charts.XYDiagram2DZoomEventArgs e)
        {
            XYDiagram2D diagram = (XYDiagram2D)sender;

            if (e.AxisX.ActualWholeRange.ActualMaxValueInternal == e.NewXRange.Max &&
               e.AxisX.ActualWholeRange.ActualMinValueInternal == e.NewXRange.Min)
                diagram.DefaultPane.AxisXScrollBarOptions.Visible = false;
            else
                diagram.DefaultPane.AxisXScrollBarOptions.Visible = true;
        }

        private void chart_CustomDrawCrosshair(object sender, CustomDrawCrosshairEventArgs e)
        {
            foreach (CrosshairElementGroup group in e.CrosshairElementGroups)
            {
                CrosshairGroupHeaderElement groupHeaderElement = group.HeaderElement;

                // Specify the text, text color and font for the crosshair group header element. 
                groupHeaderElement.FontSize = 12;
                groupHeaderElement.FontWeight = FontWeights.Bold;
            }
        }

        private void Legend_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
