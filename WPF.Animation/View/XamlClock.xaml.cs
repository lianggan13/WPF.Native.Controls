using System;
using System.Windows.Controls;

namespace WPF.Animation.View
{
    /// <summary>
    /// XamlClock.xaml 的交互逻辑
    /// </summary>
    public partial class XamlClock : UserControl
    {
        public static TimeSpan BeginTime { get => -DateTime.Now.TimeOfDay; }

        public XamlClock()
        {
            InitializeComponent();
        }
    }
}
