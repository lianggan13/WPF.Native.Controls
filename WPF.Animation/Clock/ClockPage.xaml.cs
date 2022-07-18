namespace WPF.Animation.Clock
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;


    /// <summary>
    /// ClockPage.xaml 的交互逻辑
    /// </summary>
    public partial class ClockPage : Page
    {
        public ClockPage()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Example 16-33. Selecting a storyboard at runtime

                bool isMorning = DateTime.Now.Hour < 12;
                string storyboardKey = isMorning ? "sunrise" : "sunset";
                Storyboard sb = (Storyboard)FindResource(storyboardKey);

                sb.Begin(myEllipse);

                // End of Example 16-33.
            }
            else
            {
                if (clock == null)
                {
                    StartAnimation();
                }
                else if (paused)
                {
                    ResumeAnimation();
                    paused = false;
                }
                else
                {
                    PauseAnimation();
                    paused = true;
                }
            }
        }
        bool paused = false;


        // Example 16-34. Controlling animations with a clock

        AnimationClock clock;

        void StartAnimation()
        {
            DoubleAnimation animate = new DoubleAnimation();
            animate.To = 300;
            animate.Duration = new Duration(TimeSpan.FromSeconds(5));
            animate.RepeatBehavior = RepeatBehavior.Forever;

            clock = animate.CreateClock();
            myEllipse.ApplyAnimationClock(Ellipse.WidthProperty, clock);
        }

        void PauseAnimation()
        {
            clock.Controller.Pause();
        }

        void ResumeAnimation()
        {
            clock.Controller.Resume();
        }
    }
}
