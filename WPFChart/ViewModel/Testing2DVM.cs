using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using WPFChart.Model;
using WPFCommon.MVVMFoundation;

namespace WPFChart.ViewModel
{
    public class Testing2DVM : NotifyPropertyChanged
    {
        private string remainingTime;
        public string RemainingTime
        {
            get { return remainingTime; }
            set { remainingTime = value; OnPropertyChanged(); }
        }
        public ChkCmbModel Freq { get; set; } = new ChkCmbModel();
        public ChkCmbModel Polar { get; set; } = new ChkCmbModel();
        public ChkCmbModel TurnAngle { get; set; } = new ChkCmbModel();

        public ObservableCollection<TestPointModel> TestPoints { get; set; } = new ObservableCollection<TestPointModel>();
        public ObservableCollection<LegendModel> Legends { get; set; } = new ObservableCollection<LegendModel>();

        public Testing2DVM()
        {
            for (int i = 0; i < 13; i++)
            {
                int f = 800 + i * 100;
                Freq.Items.Add(new ChkCmbItemModel() { Content = f.ToString() });
            }

            Polar.Items.Add(new ChkCmbItemModel() { Content = PolarType.Theta });
            Polar.Items.Add(new ChkCmbItemModel() { Content = PolarType.Phi });
            Polar.Items.Add(new ChkCmbItemModel() { Content = PolarType.Total });

            for (double i = 0; i < 360; i += 0.5)
            {
                TurnAngle.Items.Add(new ChkCmbItemModel() { Content = $"{i}°" });
            }


            Freq.RegisterItemPropertyChanged(FreqItem_PropertyChanged);
            Polar.RegisterItemPropertyChanged(PolarItem_PropertyChanged);
            TurnAngle.RegisterItemPropertyChanged(AngleItem_PropertyChanged);


            Freq.PropertyChanged += FreqOption_PropertyChanged;
            Polar.PropertyChanged += PolarOption_PropertyChanged;
            TurnAngle.PropertyChanged += TurnAngle_PropertyChanged;

            Legends.CollectionChanged += Legends_CollectionChanged;
        }



        private void FreqOption_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChkCmbModel.Text))
            {
                UpdatePoints();
            }
        }

        private void PolarOption_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChkCmbModel.Text))
            {
                UpdatePoints();
            }
        }

        private void TurnAngle_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChkCmbModel.Text))
            {
                TestPoints.Clear();
                Legends.Clear();
                UpdatePoints();
            }
        }

        private void UpdatePoints()
        {
            var freqs = Freq.Items.Where(f => f.IsChecked).Select(f => f.Content);
            var polars = Polar.Items.Where(f => f.IsChecked).Select(f => f.Content);
            var angle = TurnAngle.Items.FirstOrDefault(a => a.IsChecked);

            if (angle == null) return;

            // Poar_Freq  _TurnAngle
            var newfps = (from f in freqs
                          from p in polars
                          select new { f, p }).
                          Select(s =>
                          {
                              string p = PolarType.Dict[PolarType.Collection.IndexOf(s.p)].Item2;
                              return $"{p}_{s.f}";
                          });

            var oldlfps = Legends.Select(l => l.Series.DisplayName);

            var addfps = newfps?.Except(oldlfps ?? new List<string>()).ToList();
            var delfps = oldlfps?.Except(newfps ?? new List<string>()).ToList();

            foreach (var add in addfps)
            {
                var ss = add.Split('_');
                var tp = PolarType.Dict.Values.FirstOrDefault(t => t.Item2 == ss[0]);
                var polar = tp.Item3;
                var freq = double.Parse(ss[1]);
                CreateTestPoints(freq, polar, double.Parse(angle.Content.Trim('°')));
            }

            foreach (var del in delfps)
            {
                var d = Legends.FirstOrDefault(l => l.Series.DisplayName == del);
                var dps = TestPoints.Where(p => p.Legend == d.Series.DisplayName);
                dps?.ToList().ForEach(p => TestPoints.Remove(p));
                Legends.Remove(d);
            }
        }


        public void Legend_MouseDown(LegendModel legend)
        {
            legend.IsChecked = !legend.IsChecked;
            LegendModel.SetLegendsStyle(Legends);
        }


        public void TestbyZL()
        {
            var pp = TestPoints.Where(p => p.Freq == 800 && p.Polar == 1).ToList();

            foreach (var p in pp)
            {
                TestPoints.Remove(p);
            }
        }



        public void Series_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var series = e.NewItems.OfType<LineSeries2D>().FirstOrDefault();
                var legend = Legends.FirstOrDefault(xl => xl.Series.DisplayName == series.DisplayName);
                if (legend == null)
                {
                    LegendModel nlegd = new LegendModel(series, series.DisplayName.Split('_').Last(), nameof(Testing2DVM));
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





        Random rand = new Random(1);
        private void CreateTestPoints(double freq, int polar, double phi)
        {
            double startTheta = 0;
            double endTheta = 360;
            double stepTheta = 30;
            List<double> thetas = new List<double>();

            while (startTheta < endTheta)
            {
                thetas.Add(startTheta);
                startTheta += stepTheta;
            }

            foreach (var theta in thetas)
            {
                TestPointModel point = new TestPointModel()
                {
                    Freq = freq,
                    Polar = polar,
                    Phi = phi,
                    Theta = theta + 0.5,
                    Power = rand.Next(-60, 60) + 0.5,
                };
                TestPoints.Add(point);
            }
        }

        private void FreqItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChkCmbItemModel.IsChecked))
            {
                var chkeds = Freq.Items.Where(f => f.IsChecked);
                if (chkeds.Count() == 5)
                {
                    var unChkeds = Freq.Items.Where(f => !f.IsChecked);
                    unChkeds.ToList().ForEach(u => u.IsEnabled = false);
                }
                else
                {
                    Freq.Items.ToList().ForEach(f => f.IsEnabled = true);
                }
            }
        }

        private void PolarItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChkCmbItemModel.IsChecked))
            {
                var pm = sender as ChkCmbItemModel;
                int index = Polar.Items.IndexOf(pm);
                if (index == 0 || index == 1)
                {
                    if (pm.IsChecked) { Polar.Items.ElementAt(2).IsChecked = false; }
                }
                else if (index == 2)
                {
                    if (pm.IsChecked)
                    {
                        Polar.Items.ElementAt(0).IsChecked = false;
                        Polar.Items.ElementAt(1).IsChecked = false;
                    }
                }
            }
        }

        private void AngleItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChkCmbItemModel.IsChecked))
            {
                var chkeds = TurnAngle.Items.Where(f => f.IsChecked);
                if (chkeds.Count() == 1)
                {
                    var unChkeds = TurnAngle.Items.Where(f => !f.IsChecked);
                    unChkeds.ToList().ForEach(u => u.IsEnabled = false);
                }
                else
                {
                    TurnAngle.Items.ToList().ForEach(f => f.IsEnabled = true);
                }
            }
        }



    }
}
