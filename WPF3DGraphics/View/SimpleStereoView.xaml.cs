using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WPF3DGraphics.Model;


namespace WPF3DGraphics.View
{
    /// <summary>
    /// SimpleStereoView.xaml 的交互逻辑
    /// </summary>
    public partial class SimpleStereoView : UserControl
    {
        public SimpleStereoView()
        {
            InitializeComponent();

            {
                circle3dm.Content = StereoModels.DrawCircle(new Vector3D(0, 0, 0), new Vector3D(0, 0, 1), radius: 5, Brushes.LightGreen, Brushes.Brown);
                circle3dm2.Content = StereoModels.DrawCircle(new Vector3D(0, 6, 0), new Vector3D(0, 1, 1), radius: 5, Brushes.LightGreen, Brushes.Brown);
            }
            {
                cone3dm.Content = StereoModels.DrawCone(new Vector3D(0, 6, 0), new Vector3D(0, 0, 1), radius: 5, height: 8, Brushes.LightGreen, Brushes.Brown);
            }
            {
                cylinder3dm.Content = StereoModels.DrawCylinder(new Vector3D(0, 6, 0), new Vector3D(0, 0, 1), radius: 5, height: 8, Brushes.LightGreen, Brushes.Brown);
            }
        }
    }
}
