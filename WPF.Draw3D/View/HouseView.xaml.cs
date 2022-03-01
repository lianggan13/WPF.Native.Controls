using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WPF.Draw3D.Model;

namespace WPF.Draw3D.View
{
    /// <summary>
    /// HouseView.xaml 的交互逻辑
    /// </summary>
    public partial class HouseView : UserControl
    {
        public HouseView()
        {
            InitializeComponent();
            InitModel3D();
        }

        private void InitModel3D()
        {
            circle3Dm.Content = StereoModels.DrawCircle(new Vector3D(0, 0, 0), new Vector3D(0, 0, 1), radius: 5, Brushes.LightGreen, Brushes.Brown);
        }

        private void Door_Clicked(object sender, RoutedEventArgs e)
        {
        }
    }
}
