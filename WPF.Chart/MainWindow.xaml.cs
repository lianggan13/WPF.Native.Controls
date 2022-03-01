using DevExpress.Xpf.Core;
using GTS.MaxSign.Controls.Model.UC;
using System;
using System.Windows;

namespace WPFChart
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string name = Theme.Default.Name;
            ApplicationThemeHelper.ApplicationThemeName = Theme.Default.Name;

        }

        private void StackPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            mbps.VM.SetFreq(666);
            for (int i = 0; i < 32; i++)
            {
                for (int phi = 0; phi <= 360; phi++)
                {
                    MbpsPointModel p = new MbpsPointModel()
                    {
                        Phi = phi,
                        Power = i,
                        Mbps = rand.Next(-10, 100)
                    };
                    mbps.VM.AddMbpsPoint(p);

                }

            }
        }
    }
}
