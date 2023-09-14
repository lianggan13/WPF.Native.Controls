using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;


namespace WPF.Animation.View
{
    /// <summary>
    /// DropdownView.xaml 的交互逻辑
    /// </summary>
    public partial class DropdownView : UserControl
    {
        private readonly Duration _openCloseDuration = new Duration(TimeSpan.FromSeconds(0.5));

        public DropdownView()
        {
            InitializeComponent();

            dropdownContent.Height = 0;
        }

        private void OpenDropdown(object sender, RoutedEventArgs e)
        {
            dropdownInnerContent.Measure(new Size(dropdownContent.MaxWidth, dropdownContent.MaxHeight));
            DoubleAnimation heightAnimation = new DoubleAnimation(0, dropdownInnerContent.DesiredSize.Height, _openCloseDuration);
            dropdownContent.BeginAnimation(HeightProperty, heightAnimation);
        }

        private void CloseDropdown(object sender, RoutedEventArgs e)
        {
            DoubleAnimation heightAnimation = new DoubleAnimation(0, _openCloseDuration);
            dropdownContent.BeginAnimation(HeightProperty, heightAnimation);
        }
    }
}
