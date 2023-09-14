using System;
using System.Windows;
using System.Windows.Controls;

namespace WPF.Animation.View
{

    using System.Windows.Media.Animation;
    /// <summary>
    /// ImageOpacity.xaml 的交互逻辑
    /// </summary>
    public partial class ImageOpacity : UserControl
    {
        public ImageOpacity()
        {
            InitializeComponent();
        }

        private void storyboard_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            // Sender is the clock that was created for this storyboard.
            Clock storyboardClock = (Clock)sender;

            if (storyboardClock.CurrentProgress == null)
            {
                lblTime.Text = "[[ stopped ]]";
                progressBar.Value = 0;
            }
            else
            {
                lblTime.Text = storyboardClock.CurrentTime.ToString();
                progressBar.Value = (double)storyboardClock.CurrentProgress;
            }
        }

        private void sldSpeed_ValueChanged(object sender, RoutedEventArgs e)
        {
            fadeStoryboard.SetSpeedRatio(this, sldSpeed.Value);
        }
    }
}
