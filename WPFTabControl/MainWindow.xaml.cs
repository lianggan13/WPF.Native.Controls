using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WPFCommon.MVVMFoundation;

namespace WPFTabControl
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<TabItem> TabItems { get; set; } = new ObservableCollection<TabItem>();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;


            Dictionary<string, string> antDict = new Dictionary<string, string>();
            antDict.Add("Antenna 1", "Antenna 1");
            antDict.Add("Antenna 2", "Antenna 2");
            antDict.Add("Antenna 3", "Antenna 3");
            antDict.Add("Antenna 4", "Antenna 4");
            antDict.Add("Antenna 5", "Antenna 5");
            antDict.Add("Antenna 6", "Antenna 6");
            antDict.Add("Antenna 7", "Antenna 7");
            antDict.Add("Antenna 8", "Antenna 8");
            antDict.Add("Antenna 9", "Antenna 9");
            antDict.Add("Antenna 10", "Antenna 10");

            TabItems.Add(new TabItem() { Header = "Antenna 1~5" });
            TabItems.Add(new TabItem() { Header = "Antenna 6~10" });

            for (int i = 0; i < antDict.Count; i++)
            {
                AntennaPathloss a = new AntennaPathloss()
                {
                    Name = antDict.ElementAt(i).Key,
                    Pathloss = antDict.ElementAt(i).Value
                };
                if (i < 5)
                {
                    TabItems[0].AntennaPathlosses.Add(a);
                }
                else
                {
                    TabItems[1].AntennaPathlosses.Add(a);
                }
            }
        }

        private void AntennaPathlossBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class TabItem : NotifyPropertyChanged
    {
        private string header;

        public string Header
        {
            get { return header; }
            set { header = value; OnPropertyChanged(); }
        }

        public ObservableCollection<AntennaPathloss> AntennaPathlosses { get; set; } = new ObservableCollection<AntennaPathloss>();
    }

    public class AntennaPathloss : NotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private string pathloss;

        public string Pathloss
        {
            get { return pathloss; }
            set { pathloss = value; OnPropertyChanged(); }
        }
    }
}
