using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WPFCommon.MVVMFoundation;


namespace WPFChart.Model
{
    public class Testing2DModel
    {

    }

    public static class PolarType
    {

        static PolarType()
        {
            Collection = new List<string>() { Theta, Phi, Total };
            Dict = new Dictionary<int, Tuple<string, string, int>>();
            Dict.Add(Collection.IndexOf(Theta), new Tuple<string, string, int>(Theta, cTheta, 0));
            Dict.Add(Collection.IndexOf(Phi), new Tuple<string, string, int>(Theta, cPhi, 1));
            Dict.Add(Collection.IndexOf(Total), new Tuple<string, string, int>(Theta, Total, 2));

        }
        public const string Key = nameof(PolarType);
        public const string Theta = "Theta";
        public const string Phi = "Phi";
        public const string Total = "Total";

        public const string cTheta = "Θ";
        public const string cPhi = "φ";

        public static List<string> Collection { get; private set; }
        public static Dictionary<int, Tuple<string, string, int>> Dict;

        public static string Option = Theta;
    }




    public class ChkCmbModel : NotifyPropertyChanged
    {
        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                if ((Items.Any(i => i.IsChecked) && string.IsNullOrEmpty(value))
                    || text == value)
                    return;
                text = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ChkCmbItemModel> Items { get; set; } = new ObservableCollection<ChkCmbItemModel>();

        public void RegisterItemPropertyChanged(PropertyChangedEventHandler itemPropertyChangedhandler)
        {
            foreach (var item in Items)
            {
                item.PropertyChanged += itemPropertyChangedhandler;
            }

            Items.CollectionChanged += (s, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    var newItem = e.NewItems.OfType<ChkCmbItemModel>().First();
                    newItem.PropertyChanged += itemPropertyChangedhandler;
                }
            };
        }

        public void DropDownClosed()
        {
            if (this.dropDownClosedHandler == null)
            {
                Text = string.Join(",", Items.Where(f => f.IsChecked).Select(f => f.Content));
            }
            else
            {
                dropDownClosedHandler?.Invoke();
            }
        }
        private Action dropDownClosedHandler;
        public void OverrideDropDownClosedHandler(Action dropDownClosedHandler)
        {
            this.dropDownClosedHandler = dropDownClosedHandler;
        }
    }

    public class ChkCmbItemModel : NotifyPropertyChanged
    {
        private string content;
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                this.content = value;
                OnPropertyChanged();
            }
        }

        private bool isChecked;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                OnPropertyChanged();
            }
        }

        private bool isEnabled = true;
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }
    }

    public class TestPointModel : NotifyPropertyChanged
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

        private int polar;

        public int Polar
        {
            get { return polar; }
            set
            {
                polar = value;
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

        private double theta;

        public double Theta
        {
            get { return theta; }
            set
            {
                theta = value;
                OnPropertyChanged();
            }
        }

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

        public string Legend
        {
            get
            {
                string p = PolarType.Dict[polar].Item2;
                return $"{p}_{Freq}";
            }
        }
    }



}
