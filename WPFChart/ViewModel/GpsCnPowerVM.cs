using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WPFChart.Model;
using WPFCommon.Algorithm;
using WPFCommon.MVVMFoundation;

namespace WPFChart.ViewModel
{
    public class GpsCnPowerVM : NotifyPropertyChanged
    {
        private string formula;
        public string Formula
        {
            get { return formula; }
            set { formula = value; OnPropertyChanged(); }
        }

        public ObservableCollection<GpsCnPowerModel> CnPowerPoints { get; set; } = new ObservableCollection<GpsCnPowerModel>();
        public ObservableCollection<GpsCnPowerModel> FittedPoints { get; set; } = new ObservableCollection<GpsCnPowerModel>();

        public GpsCnPowerVM()
        {

        }

        public void AddGpsCnPowerPoint(GpsCnPowerModel point)
        {
            CnPowerPoints.Add(point);
        }

        public void AddGpsCnPowerPoint(List<GpsCnPowerModel> points)
        {
            points.ForEach(p =>
            {
                CnPowerPoints.Add(p);
            });
        }

        public void AddFittedPoint(GpsCnPowerModel point)
        {
            FittedPoints.Add(point);
        }

        public void AddFittedPoint(List<GpsCnPowerModel> points)
        {
            points.ForEach(p =>
            {
                FittedPoints.Add(p);
            });
        }

        public void ClearAll()
        {
            CnPowerPoints.Clear();
            FittedPoints.Clear();
            Formula = null;
        }

        public void TestCode()
        {
            ClearAll();
            if (CnPowerPoints.Count > 0)
            {

            }
            CnPowerPoints.Add(new GpsCnPowerModel(90, -140));
            CnPowerPoints.Add(new GpsCnPowerModel(97.2, -138));
            CnPowerPoints.Add(new GpsCnPowerModel(99, -136));
            CnPowerPoints.Add(new GpsCnPowerModel(101, -134));
            CnPowerPoints.Add(new GpsCnPowerModel(103, -132));
            CnPowerPoints.Add(new GpsCnPowerModel(104.5, -130));
            CnPowerPoints.Add(new GpsCnPowerModel(106, -128));
            CnPowerPoints.Add(new GpsCnPowerModel(107.2, -126));
            CnPowerPoints.Add(new GpsCnPowerModel(110, -124));
            CnPowerPoints.Add(new GpsCnPowerModel(112, -122));
            CnPowerPoints.Add(new GpsCnPowerModel(115.3, -120));
            CnPowerPoints.Add(new GpsCnPowerModel(120, -118));
            CnPowerPoints.Add(new GpsCnPowerModel(130, -116));

            var ps = CnPowerPoints.Select(c => new Point(c.Cn, c.Power)).ToList();
            Utils.LinearRegression(ps.ToArray(), out double a, out double b);

            var linePoints = CnPowerPoints.Select(c =>
             {
                 var linePower = Utils.LinearEquation(a, b, c.Cn);
                 return new GpsCnPowerModel(c.Cn, linePower);

             });


            foreach (var p in linePoints)
            {
                FittedPoints.Add(p);
            }

            Formula = "f(x)=ax+b";
        }
    }
}
