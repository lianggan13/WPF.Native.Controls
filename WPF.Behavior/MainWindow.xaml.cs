using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
namespace BehaviorAnimation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class TestPanel : Panel
    {
        public TestPanel()
        {

        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {

            double elementWidth = arrangeSize.Width / Children.Count;
            double elementHeight = arrangeSize.Height;
            double currentX = 0;

            for (int i = 0; i < Children.Count; i++)
            {
                //(Children[i] as FrameworkElement).Margin = i != Children.Count - 1
                //    ? new Thickness(0, 0, 5, 0)
                //    : new Thickness(0);
                Children[i].Arrange(new Rect(currentX, 0, elementWidth, elementHeight));

                var item = Children[i] as FrameworkElement;

                item.Margin = new Thickness(item.ActualWidth, 0, 0, 0);
                ThicknessAnimation animation = new ThicknessAnimation();
                animation.From = item.Margin;
                animation.To = new Thickness();
                animation.BeginTime = TimeSpan.FromMilliseconds(i * 100);
                animation.Duration = TimeSpan.FromMilliseconds(800);
                item.BeginAnimation(FrameworkElement.MarginProperty, animation);

                currentX += elementWidth;
            }
            return arrangeSize;
        }
    }
}
