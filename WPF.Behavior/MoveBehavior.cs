using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;

namespace BehaviorAnimation
{
    public class MoveBehavior : Behavior<FrameworkElement>
    {

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is Panel panel)
            {
                int i = 1;
                foreach (FrameworkElement item in panel.Children)
                {
                    i++;
                    item.Margin = new Thickness(item.ActualWidth, 0, 0, 0);
                    ThicknessAnimation animation = new ThicknessAnimation();
                    animation.From = item.Margin;
                    animation.To = new Thickness();
                    animation.BeginTime = TimeSpan.FromMilliseconds(i * 100);
                    animation.Duration = TimeSpan.FromMilliseconds(800);
                    item.BeginAnimation(FrameworkElement.MarginProperty, animation);
                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }
    }
}
