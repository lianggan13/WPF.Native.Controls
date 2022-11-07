using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace WPF.Button.Controls
{
    /// <summary>
    /// TickToggleButton.xaml 的交互逻辑
    /// </summary>
    public partial class TickToggleButton : ToggleButton
    {
        public TickToggleButton()
        {
            InitializeComponent();
            SetCurrentValue(TickTimeProperty, new DateTime(Duration.Ticks));
        }

        public DateTime TickTime
        {
            get { return (DateTime)GetValue(TickTimeProperty); }
            set { SetValue(TickTimeProperty, value); }
        }

        //public event EventHandler TickTimout;

        // Using a DependencyProperty as the backing store for TickTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TickTimeProperty =
            DependencyProperty.Register("TickTime", typeof(DateTime), typeof(TickToggleButton), new PropertyMetadata(new DateTime()));


        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TickToggleButton), new PropertyMetadata(new CornerRadius(5)));


        public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(13);

        private CancellationTokenSource cts;//= new CancellationTokenSource();

        protected override void OnChecked(RoutedEventArgs e)
        {
            base.OnChecked(e);

            SetCurrentValue(TickTimeProperty, new DateTime(Duration.Ticks));

            cts = new CancellationTokenSource();
            //cts.CancelAfter(timout);

            var cr = new DateTime(TickTime.Ticks);
            Task.Run(async () =>
            {
                while (true)
                {
                    if (cts.IsCancellationRequested)
                    {
                        break;
                    }

                    cr = cr.Subtract(TimeSpan.FromSeconds(1));
                    if (cr.Second < 0.5)
                    {
                        break;
                    }

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        TickTime = new DateTime(cr.Ticks);
                    });

                    await Task.Delay(TimeSpan.FromSeconds(1), cts.Token);
                }

                // reset timeout 

            }, cts.Token).ContinueWith(t =>
            {
                if (t.IsCanceled)
                {
                    //cts = null;
                }
                else if (t.IsCompleted)
                {
                    // TickTimout?.Invoke(this, new EventArgs());
                }

                Application.Current.Dispatcher.Invoke(() =>
                {
                    IsChecked = false;
                });
            });


        }

        protected override void OnUnchecked(RoutedEventArgs e)
        {
            cts?.Cancel();
            base.OnUnchecked(e);
        }
    }
}
