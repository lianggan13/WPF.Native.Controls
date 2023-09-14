using System;
using System.Windows;
using System.Windows.Controls;

namespace WPF.Media
{
    /// <summary>
    /// Interaction logic for CodePlayback.xaml
    /// </summary>

    public partial class CodePlayback : System.Windows.Window
    {

        public CodePlayback()
        {
            InitializeComponent();


        }

        private void sliderSpeed_ValueChanged(object sender, RoutedEventArgs e)
        {
            media.SpeedRatio = ((Slider)sender).Value;
        }

        private void cmdPlay_Click(object sender, RoutedEventArgs e)
        {
            media.Play();
        }
        private void cmdPause_Click(object sender, RoutedEventArgs e)
        {
            media.Pause();
        }
        private void cmdStop_Click(object sender, RoutedEventArgs e)
        {
            media.Stop();
            media.SpeedRatio = 1;
        }
        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            sliderPosition.Maximum = media.NaturalDuration.TimeSpan.TotalSeconds;
        }


        private void sliderPosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Pause();
            media.Position = TimeSpan.FromSeconds(sliderPosition.Value);
            media.Play();
        }
    }
}