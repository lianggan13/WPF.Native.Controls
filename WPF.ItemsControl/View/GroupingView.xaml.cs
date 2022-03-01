using System.Windows;
using System.Windows.Controls;
using WPFItemsControl.Model;

namespace WPFItemsControl.View
{
    /// <summary>
    /// GroupingView.xaml 的交互逻辑
    /// </summary>
    public partial class GroupingView : UserControl
    {
        public GroupingView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ICollectionView cv = CollectionViewSource.GetDefaultView(ic.ItemsSource);
            //cv.GroupDescriptions.Add(new PropertyGroupDescription(nameof(ModelFile.UpTime)));
            VM.CollectionModelFile.Add(new ModelFile() { FileName = "WPF", AuthorName = "王鹏", UpTime = "2021-10-10" });

            //var gp = cv.GroupDescriptions[0].GroupNames[0];

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //IsBottomLevel = false
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {

        }
    }
}
