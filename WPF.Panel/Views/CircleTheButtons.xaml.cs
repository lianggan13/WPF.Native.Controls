using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace WPF.Panel.Views
{
    /// <summary>
    /// CircleTheButtons.xaml 的交互逻辑
    /// </summary>
    public partial class CircleTheButtons : UserControl
    {
        public static List<string> Items = new List<string>(Enumerable.Range(1, 10).Select(i => $"Button Number {i}"));

        public CircleTheButtons()
        {
            InitializeComponent();
        }
    }
}
