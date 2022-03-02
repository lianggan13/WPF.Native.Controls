using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace Frame界面导航过度
{
    /// <summary>
    /// LayFrame.xaml 的交互逻辑
    /// </summary>
    public partial class LayFrame : UserControl
    {
        public LayFrame()
        {
            InitializeComponent();
        }
        public Uri Source
        {
            get { return (Uri)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(Uri), typeof(LayFrame), new PropertyMetadata(SourceChange));

        private async static void SourceChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LayFrame layFrame = d as LayFrame;
            if (e.NewValue == null && e.OldValue == null) return;
            if (e.NewValue != null && e.OldValue != null)
            {
                if (e.NewValue.ToString() == e.OldValue.ToString()) return;
            }
            if (layFrame.IsLoaded)
            {
                if (layFrame.NewPage.Source == null)
                {
                    layFrame.NewPage.Source = layFrame.Source;
                    ThicknessAnimationUsingKeyFrames thicknessAnimationUsingKeyFrames = new ThicknessAnimationUsingKeyFrames();
                    thicknessAnimationUsingKeyFrames.KeyFrames.Add(new EasingThicknessKeyFrame()
                    {
                        KeyTime = TimeSpan.FromSeconds(0),
                        Value = new Thickness()
                        {
                            Left = layFrame.ActualWidth,
                            Top = 0,
                            Bottom = 0,
                            Right = 0
                        }
                    });
                    thicknessAnimationUsingKeyFrames.KeyFrames.Add(new EasingThicknessKeyFrame()
                    {
                        KeyTime = TimeSpan.FromSeconds(0.5),
                        Value = new Thickness()
                        {
                            Left = 0,
                            Top = 0,
                            Bottom = 0,
                            Right = 0
                        }
                    });
                    layFrame.NewPage.BeginAnimation(MarginProperty, thicknessAnimationUsingKeyFrames);
                    Panel.SetZIndex(layFrame.NewPage, 1);
                    Panel.SetZIndex(layFrame.OldPage, 0);
                    await Task.Delay(500);
                    layFrame.OldPage.Source = null;
                }
                else
                {
                    layFrame.OldPage.Source = layFrame.Source;
                    ThicknessAnimationUsingKeyFrames thicknessAnimationUsingKeyFrames = new ThicknessAnimationUsingKeyFrames();
                    thicknessAnimationUsingKeyFrames.KeyFrames.Add(new EasingThicknessKeyFrame()
                    {
                        KeyTime = TimeSpan.FromSeconds(0),
                        Value = new Thickness()
                        {
                            Left = layFrame.ActualWidth,
                            Top = 0,
                            Bottom = 0,
                            Right = 0
                        }
                    });
                    thicknessAnimationUsingKeyFrames.KeyFrames.Add(new EasingThicknessKeyFrame()
                    {
                        KeyTime = TimeSpan.FromSeconds(0.5),
                        Value = new Thickness()
                        {
                            Left = 0,
                            Top = 0,
                            Bottom = 0,
                            Right = 0
                        }
                    });
                    layFrame.OldPage.BeginAnimation(MarginProperty, thicknessAnimationUsingKeyFrames);
                    Panel.SetZIndex(layFrame.OldPage, 1);
                    Panel.SetZIndex(layFrame.NewPage, 0);
                    await Task.Delay(500);
                    layFrame.NewPage.Source = null;
                }
            }
            else
            {
                layFrame.OldPage.Source = layFrame.Source;
            }
        }

        private void Page_Navigated(object sender, NavigationEventArgs e)
        {
            (sender as Frame).NavigationService.RemoveBackEntry();
        }
    }
}
