using System;
using System.Windows;
using System.Windows.Controls;

namespace WPFTextBox.ValidateTextBox
{
    [TemplateVisualState(Name = "ShowWatermarkState", GroupName = "WatermarkGroup")]
    [TemplateVisualState(Name = "HideWatermarkState", GroupName = "WatermarkGroup")]
    public class WatermarkTextBox : TextBox
    {
        static WatermarkTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WatermarkTextBox),
                    new FrameworkPropertyMetadata(typeof(WatermarkTextBox)));

            TextProperty.OverrideMetadata(typeof(WatermarkTextBox),
                new FrameworkPropertyMetadata(
                        new PropertyChangedCallback(TextPropertyChanged)));
        }

        public static readonly DependencyProperty WatermarkTextProperty =
            DependencyProperty.Register(nameof(WatermarkText), typeof(string),
            typeof(WatermarkTextBox), new FrameworkPropertyMetadata(String.Empty));

        public string WatermarkText
        {
            get { return (string)GetValue(WatermarkTextProperty); }
            set { SetValue(WatermarkTextProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            VisualStateManager.GoToState(this, "ShowWatermarkState", true);
        }


        static void TextPropertyChanged(DependencyObject sender,
                          DependencyPropertyChangedEventArgs args)
        {
            WatermarkTextBox watermarkTextBox = (WatermarkTextBox)sender;
            watermarkTextBox.UpdateState();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            UpdateState();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            UpdateState();
        }

        private void UpdateState()
        {
            bool textExists = Text.Length > 0;
            var watermark = GetTemplateChild("PART_Watermark") as FrameworkElement;
            var state = textExists || IsFocused ? "HideWatermarkState" : "ShowWatermarkState";

            VisualStateManager.GoToState(this, state, true);
        }




    }

}
