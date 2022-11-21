using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPF.Virtualization.DataVirtualization
{
    /// <summary>
    /// Pager.xaml 的交互逻辑
    /// </summary>
    public partial class PagerBar : UserControl
    {
        public static RoutedEvent FirstPageEvent;
        public static RoutedEvent PreviousPageEvent;
        public static RoutedEvent NextPageEvent;
        public static RoutedEvent LastPageEvent;

        public static readonly DependencyProperty CurrentPageProperty;
        public static readonly DependencyProperty TotalPageProperty;

        public string CurrentPage
        {
            get { return (string)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        public string TotalPage
        {
            get { return (string)GetValue(TotalPageProperty); }
            set { SetValue(TotalPageProperty, value); }
        }

        public PagerBar()
        {
            InitializeComponent();
        }

        static PagerBar()
        {
            FirstPageEvent = EventManager.RegisterRoutedEvent("FirstPage", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(PagerBar));
            PreviousPageEvent = EventManager.RegisterRoutedEvent("PreviousPage", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(PagerBar));
            NextPageEvent = EventManager.RegisterRoutedEvent("NextPage", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(PagerBar));
            LastPageEvent = EventManager.RegisterRoutedEvent("LastPage", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(PagerBar));

            CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(string), typeof(PagerBar), new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnCurrentPageChanged)));
            TotalPageProperty = DependencyProperty.Register("TotalPage", typeof(string), typeof(PagerBar), new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnTotalPageChanged)));
        }

        public event RoutedEventHandler FirstPage
        {
            add { AddHandler(FirstPageEvent, value); }
            remove { RemoveHandler(FirstPageEvent, value); }
        }

        public event RoutedEventHandler PreviousPage
        {
            add { AddHandler(PreviousPageEvent, value); }
            remove { RemoveHandler(PreviousPageEvent, value); }
        }

        public event RoutedEventHandler NextPage
        {
            add { AddHandler(NextPageEvent, value); }
            remove { RemoveHandler(NextPageEvent, value); }
        }

        public event RoutedEventHandler LastPage
        {
            add { AddHandler(LastPageEvent, value); }
            remove { RemoveHandler(LastPageEvent, value); }
        }

        public static void OnTotalPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PagerBar p = d as PagerBar;

            if (p != null)
            {
                Run rTotal = (Run)p.FindName("rTotal");

                rTotal.Text = (string)e.NewValue;
            }
        }

        private static void OnCurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PagerBar p = d as PagerBar;

            if (p != null)
            {
                Run rCurrrent = (Run)p.FindName("rCurrent");

                rCurrrent.Text = (string)e.NewValue;
            }
        }

        private void FirstPageButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(FirstPageEvent, this));
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(PreviousPageEvent, this));
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(NextPageEvent, this));
        }

        private void LastPageButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(LastPageEvent, this));
        }
    }
}
