using System.Windows;
using System.Windows.Controls;


namespace WPF.Event.CC
{
    public class CustomButton : Button
    {

        // Create a custom routed event by first registering a RoutedEventID
        // This event uses the bubbling routing strategy
        public static readonly RoutedEvent TapEvent = EventManager.RegisterRoutedEvent(
            "Tap", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CustomButton));

        // Provide CLR accessors for the event
        public event RoutedEventHandler Tap
        {
            add { AddHandler(TapEvent, value); }
            remove { RemoveHandler(TapEvent, value); }
        }

        // This method raises the Tap event
        private void RaiseTapEvent()
        {
            var newEventArgs = new RoutedEventArgs(TapEvent);
            RaiseEvent(newEventArgs);
        }

        // For demonstration purposes we raise the event when the MyButtonSimple is clicked
        protected override void OnClick()
        {
            RaiseTapEvent();
        }
    }
}
