using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;

namespace WPF.ProgressBar.Behaviors
{
    using System.Windows.Controls;
    public class ProgressBarBehavior : Behavior<ProgressBar>
    {
        bool IsRun = false;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.ValueChanged += Progress_ValueChanged;

        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.ValueChanged -= Progress_ValueChanged;
        }


        private void Progress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsRun) return;
            IsRun = true;

            ProgressBar progress = sender as ProgressBar;
            DoubleAnimation doubleAnimation = new DoubleAnimation(e.OldValue, e.NewValue, new Duration(TimeSpan.FromSeconds(1)), FillBehavior.Stop);
            doubleAnimation.Completed += (s, a) => IsRun = false;
            progress.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);

        }
    }
}
