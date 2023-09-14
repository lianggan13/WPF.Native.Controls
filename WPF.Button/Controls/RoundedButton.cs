//----------------------------------------------
// RoundedButton.cs (c) 2006 by Charles Petzold
//----------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF.Button.Controls
{
    public class RoundedButton : Control
    {
        // Public static ClickEvent.
        public static readonly RoutedEvent ClickEvent;

        public static readonly DependencyProperty IsPressedProperty;

        // Static Constructor.
        static RoundedButton()
        {
            ClickEvent =
                EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
                        typeof(RoutedEventHandler), typeof(RoundedButton));

            IsPressedProperty =
              DependencyProperty.Register("IsPressed", typeof(bool),
                      typeof(RoundedButton),
                      new FrameworkPropertyMetadata(false,
                              FrameworkPropertyMetadataOptions.AffectsRender));
        }

        public bool IsPressed
        {
            set { SetValue(IsPressedProperty, value); }
            get { return (bool)GetValue(IsPressedProperty); }
        }

        public RoundedButton()
        {
        }

        private UIElement child;
        public UIElement Child
        {
            get => child;
            set
            {
                if (child != null)
                {
                    RemoveVisualChild(child);
                    RemoveLogicalChild(child);
                }
                if ((child = value) != null)
                {
                    AddVisualChild(child);
                    AddLogicalChild(child);
                }
            }
        }

        // Public event.
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }
        // Overridden property and methods.
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }
        protected override Visual GetVisualChild(int index)
        {
            if (index > 0)
                throw new ArgumentOutOfRangeException("index");

            return Child;
        }
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            Size szDesired = new Size(2, 2);
            sizeAvailable.Width -= szDesired.Width;
            sizeAvailable.Height -= szDesired.Height;

            if (Child != null)
            {
                Child.Measure(sizeAvailable);
                szDesired.Width += Child.DesiredSize.Width;
                szDesired.Height += Child.DesiredSize.Height;
            }

            return szDesired;
        }
        protected override Size ArrangeOverride(Size sizeArrange)
        {
            if (Child != null)
            {
                Point ptChild =
                    new Point(Math.Max(1, (sizeArrange.Width -
                                            Child.DesiredSize.Width) / 2),
                              Math.Max(1, (sizeArrange.Height -
                                            Child.DesiredSize.Height) / 2));

                Child.Arrange(new Rect(ptChild, Child.DesiredSize));
            }
            return sizeArrange;
        }

        protected override void OnRender(DrawingContext dc)
        {
            RadialGradientBrush brush = new RadialGradientBrush(
                    IsPressed ? SystemColors.ControlDarkColor :
                                SystemColors.ControlLightLightColor,
                    SystemColors.ControlColor);

            brush.GradientOrigin = IsPressed ? new Point(0.75, 0.75) :
                                               new Point(0.25, 0.25);
            dc.DrawRoundedRectangle(brush,
                    new Pen(SystemColors.ControlDarkDarkBrush, 1),
                    new Rect(new Point(0, 0), RenderSize),
                             RenderSize.Height / 2, RenderSize.Height / 2);
        }


        protected override void OnMouseMove(MouseEventArgs args)
        {
            base.OnMouseMove(args);

            if (IsMouseCaptured)
                IsPressed = IsMouseReallyOver;
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs args)
        {
            base.OnMouseLeftButtonDown(args);
            CaptureMouse();
            IsPressed = true;
            args.Handled = true;
        }
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs args)
        {
            base.OnMouseRightButtonUp(args);

            if (IsMouseCaptured)
            {
                if (IsMouseReallyOver)
                    OnClick();

                Mouse.Capture(null);
                IsPressed = false;
                args.Handled = true;
            }
        }
        bool IsMouseReallyOver
        {
            get
            {
                Point pt = Mouse.GetPosition(this);
                return pt.X >= 0 && pt.X < ActualWidth &&
                        pt.Y >= 0 && pt.Y < ActualHeight;
            }
        }
        // Method to fire Click event.
        protected virtual void OnClick()
        {
            RoutedEventArgs argsEvent = new RoutedEventArgs();
            argsEvent.RoutedEvent = ClickEvent;
            argsEvent.Source = this;
            RaiseEvent(argsEvent);
        }
    }
}

