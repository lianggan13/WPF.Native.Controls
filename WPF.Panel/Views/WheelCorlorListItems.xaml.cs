using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF.Panel.Views
{
    /// <summary>
    /// WheelCorlorListItems.xaml 的交互逻辑
    /// </summary>
    public partial class WheelCorlorListItems : UserControl
    {
        public static List<Brush> Items = new List<Brush>(typeof(Brushes).GetProperties().Select(p => (Brush)p.GetValue(null, null)));

        public WheelCorlorListItems()
        {
            InitializeComponent();
        }
    }
}
