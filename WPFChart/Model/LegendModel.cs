using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

using WPFCommon.MVVMFoundation;

namespace WPFChart.Model
{
    public class PaletteManager
    {

        public static Dictionary<string, Dictionary<string, SolidColorBrush>> Palette = new Dictionary<string, Dictionary<string, SolidColorBrush>>();

        public static List<SolidColorBrush> ColorBrushSource { get; }

        static PaletteManager()
        {
            CustomPalette ss = Application.Current.FindResource("Style.DXChart.CustomPalette") as CustomPalette;
            ColorBrushSource = new List<SolidColorBrush>(ss.Colors.Select(c => new SolidColorBrush(c)));
        }

        public static SolidColorBrush GetColor(Dictionary<string, SolidColorBrush> palette)
        {
            var cb = ColorBrushSource.FirstOrDefault(c => !palette.Values.Contains(c));
            if (cb == null)
            {
                var rand = new Random(new Guid().GetHashCode());
                int n = rand.Next(ColorBrushSource.Count);
                cb = ColorBrushSource.ElementAt(n);
            }

            return cb;
        }
    }

    public class LegendModel : NotifyPropertyChanged
    {
        public string ChartName { get; set; }
        public string PaletteKey { get; set; }
        public LegendModel(LineSeries2D series, string paletteKey, string chartName)
        {
            Series = series;
            PaletteKey = paletteKey;
            ChartName = chartName;

            Dictionary<string, SolidColorBrush> palette = null;
            if (!PaletteManager.Palette.ContainsKey(chartName))
            {
                palette = new Dictionary<string, SolidColorBrush>();
            }
            else
            {
                palette = PaletteManager.Palette[chartName];
            }
            if (!palette.ContainsKey(paletteKey))
            {
                palette[paletteKey] = PaletteManager.GetColor(palette);
            }
            series.Brush = palette[paletteKey];
            PaletteManager.Palette[chartName] = palette;

            if (series.DisplayName.Contains(PolarType.cPhi))
            {
                series.LineStyle.DashStyle.Dashes = new DoubleCollection(new double[] { 1, 4, 1 });
            }
        }


        public void DisbandingSeries()
        {
            PaletteManager.Palette[ChartName].Remove(PaletteKey);
            // Series = null;
        }


        private LineSeries2D series;

        public LineSeries2D Series
        {
            get { return series; }
            set
            {
                series = value;
            }
        }


        private bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged();
            }
        }

        public void SetBrush(byte opacity)
        {
            Color color = Color.FromArgb(opacity, series.Brush.Color.R, series.Brush.Color.G, series.Brush.Color.B);
            series.Brush = new SolidColorBrush(color);
        }

        public void ResetBrush()
        {
            series.Brush = PaletteManager.Palette[ChartName][PaletteKey];
        }

        public static void OrderLegend(LegendModel nlegd, ObservableCollection<LegendModel> legends)
        {
            var fl = legends.FirstOrDefault(l => double.Parse(l.PaletteKey) > double.Parse(nlegd.PaletteKey));
            if (fl != null)
            {
                int newIndex = legends.IndexOf(fl);
                int oldIndex = legends.IndexOf(nlegd);
                legends.Move(oldIndex, newIndex);
            }
        }


        public static void SetLegendsStyle(ObservableCollection<LegendModel> legends)
        {
            var legsEnabled = legends;

            if (legsEnabled.Any(l => l.IsChecked) == true)
            {
                legsEnabled.Where(l => !l.IsChecked).ToList()
                 .ForEach(c =>
                 {
                     c.Series.CrosshairEnabled = false;
                     c.SetBrush(60);
                 });
            }
            else
            {
                legsEnabled.ToList().ForEach(c =>
                {
                    c.Series.CrosshairEnabled = true;
                    c.ResetBrush();
                });
            }

            legsEnabled.Where(l => l.IsChecked).ToList().ForEach(lc =>
            {
                lc.IsChecked = true;
                lc.Series.CrosshairEnabled = true;
                lc.ResetBrush();
            });
        }
    }
}
