using DevExpress.Xpf.Charts;
using System.Windows.Controls;

namespace WPFChart.UC
{
    /// <summary>
    /// GPSData2DChartUC.xaml 的交互逻辑
    /// </summary>
    public partial class GpsCnPowerChart : UserControl
    {
        public GpsCnPowerChart()
        {
            InitializeComponent();
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
    }
}
