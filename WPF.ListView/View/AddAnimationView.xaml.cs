using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ListViewAddAnimation
{
    /// <summary>
    /// SlideImageView.xaml 的交互逻辑
    /// </summary>
    public partial class AddAnimationView : Window
    {
        public ObservableCollection<Guid> ListData { get; set; } = new ObservableCollection<Guid>();

        public AddAnimationView()
        {
            InitializeComponent();
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {

        }

        private void AddBtn_Clicked(object sender, RoutedEventArgs e)
        {
            ListData.Add(Guid.NewGuid());
        }

        private async void DeleteBtn_Clicked(object sender, RoutedEventArgs e)
        {
            await System.Threading.Tasks.Task.Delay(1000); // Wait for Storyboard_Completed
            var btn = (Button)sender;
            var guid = (Guid)btn.DataContext;
            ListData.Remove(guid);
        }
    }
}
