using DevExpress.Xpf.Charts;
using GTS.MaxSign.Controls.Model.UC;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using WPFChart.Model;
using WPFCommon.MVVMFoundation;

namespace GTS.MaxSign.Controls.ViewModel
{
    public class Mbps2DChartVM : NotifyPropertyChanged
    {
        private double freq;

        public double Freq
        {
            get { return freq; }
            set
            {
                freq = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<MbpsPointModel> MbpsPoints { get; set; } = new ObservableCollection<MbpsPointModel>();
        public ObservableCollection<LegendModel> Legends { get; set; } = new ObservableCollection<LegendModel>();
        public Mbps2DChartVM()
        {
            Legends.CollectionChanged += Legends_CollectionChanged;
        }

        public void SetFreq(double freq)
        {
            this.Freq = freq;
        }

        public void AddMbpsPoint(MbpsPointModel point)
        {
            MbpsPoints.Add(point);
        }

        public void AddMbpsPoint(List<MbpsPointModel> points)
        {
            points.ForEach(p =>
            {
                MbpsPoints.Add(p);
            });
        }

        public void ClearMbpsPoint()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MbpsPoints.Clear();
                Legends.Clear();
            });
        }

        public void Legend_MouseDown(LegendModel legend)
        {
            legend.IsChecked = !legend.IsChecked;
            LegendModel.SetLegendsStyle(Legends);
        }

        public void Series_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var series = e.NewItems.OfType<LineSeries2D>().FirstOrDefault();
                var legend = Legends.FirstOrDefault(xl => xl.Series.DisplayName == series.DisplayName);
                if (legend == null)
                {
                    LegendModel nlegd = new LegendModel(series, series.DisplayName.Replace("°", ""), nameof(Mbps2DChartVM));
                    Legends.Add(nlegd);
                    LegendModel.OrderLegend(nlegd, Legends);
                    LegendModel.SetLegendsStyle(Legends);
                }
            }
        }

        private void Legends_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                var legend = e.OldItems.OfType<LegendModel>().FirstOrDefault();
                legend.DisbandingSeries();
            }
        }
    }
}
