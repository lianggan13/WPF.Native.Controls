using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF.Brush.Brushes
{
    /// <summary>
    /// LinearGradientBrushes.xaml 的交互逻辑
    /// </summary>
    public partial class LinearGradientBrushes : UserControl
    {
        public LinearGradientBrushes()
        {
            InitializeComponent();
            this.Loaded += LinearGradientBrushes_Loaded;
        }

        private async void LinearGradientBrushes_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));

            //while (true)
            //{
            //    if (grateBottom.Defense)
            //    {
            //        grateBottom.Defense = false;
            //    }
            //    else if (grateBottom.State == GratingState.Error)
            //    {
            //        grateBottom.Defense = true;
            //        grateBottom.State = GratingState.DefenseWarning;
            //    }
            //    else
            //    {
            //        grateBottom.State = GratingState.Error;
            //    }
            //    await Task.Delay(TimeSpan.FromSeconds(8));
            //}

        }
    }
}
