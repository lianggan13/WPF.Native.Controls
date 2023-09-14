using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace WPF.ScrollViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static ObservableCollection<string> ItemsSource { get; set; } = new ObservableCollection<string>();

        private void AddButton_Clicked(object sender, RoutedEventArgs e)
        {
            foreach (var item in Enumerable.Range(0, 13).Select(i => i.ToString()))
                ItemsSource.Add(item);
        }
    }
}
