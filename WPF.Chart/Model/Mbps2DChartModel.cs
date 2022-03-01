using WPFCommon.MVVMFoundation;


namespace GTS.MaxSign.Controls.Model.UC
{
    public class Mbps2DChartModel : NotifyPropertyChanged
    {

    }

    public class MbpsPointModel : NotifyPropertyChanged
    {

        private double power;

        public double Power
        {
            get { return power; }
            set
            {
                power = value;
                OnPropertyChanged();
            }
        }


        private double mbps;

        public double Mbps
        {
            get { return mbps; }
            set
            {
                mbps = value;
                OnPropertyChanged();
            }
        }

        private double phi;

        public double Phi
        {
            get { return phi; }
            set
            {
                phi = value;
                OnPropertyChanged();
            }
        }
    }

    //public class LegendModel : NotifyPropertyChanged
    //{
    //    public LegendModel()
    //    { }
    //    public LegendModel(LineSeries2D series)
    //    {
    //        Series = series;
    //        IsChecked = false;
    //    }

    //    private string name;

    //    public string Name
    //    {
    //        get { return name; }
    //        set
    //        {
    //            name = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    private SolidColorBrush legendBrush;

    //    public SolidColorBrush LegendBrush
    //    {
    //        get { return legendBrush; }
    //        set
    //        {
    //            legendBrush = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    private LineSeries2D series;

    //    public LineSeries2D Series
    //    {
    //        get { return series; }
    //        set
    //        {
    //            series = value;
    //            Name = series.DisplayName;
    //            Phi = double.Parse(name);
    //            DevExpress.Xpf.Charts.ChartControl chart = (series.Parent as XYDiagram2D).Parent as DevExpress.Xpf.Charts.ChartControl;
    //            LegendBrush = new SolidColorBrush(chart.Palette[chart.Diagram.Series.IndexOf(series)]);
    //        }
    //    }


    //    private bool isChecked;

    //    public bool IsChecked
    //    {
    //        get { return isChecked; }
    //        set
    //        {
    //            isChecked = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    public SolidColorBrush GenerateSeriesBrush(byte opacity)
    //    {
    //        Color color = Color.FromArgb(opacity, LegendBrush.Color.R, LegendBrush.Color.G, LegendBrush.Color.B);
    //        return new SolidColorBrush(color);
    //    }


    //    private double phi;

    //    public double Phi
    //    {
    //        get { return phi; }
    //        set
    //        {
    //            phi = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //}
}
