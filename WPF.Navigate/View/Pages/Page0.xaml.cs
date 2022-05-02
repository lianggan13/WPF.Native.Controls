// Page0.xaml.cs
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PageState
{
    public partial class Page0 : Page
    {
        public Page0()
        {
            InitializeComponent();
            Loaded += Page0_Loaded;
            playLink.Click += playLink_Click;
        }

        void Page0_Loaded(object sender, RoutedEventArgs e)
        {
            Window parent = Parent as Window;
            if (parent != null) { parent.Width = 400; parent.Height = 200; }
        }

        void playLink_Click(object sender, RoutedEventArgs e)
        {
            MagicWordPageFunction fn = new MagicWordPageFunction();
            fn.MagicWord = "please";
            fn.Return += fn_Return;
            NavigationService.Navigate(fn);
        }

        void fn_Return(object sender, ReturnEventArgs<string> e)
        {
            // Get the navigation service from the sender
            // (the current page's hasn't yet been restored)
            NavigationService navService = ((PageFunctionBase)sender).NavigationService;

            // User canceled
            if (e == null) { navService.Navigate(new Uri("/View/Pages/QuitterPage.xaml", UriKind.Relative)); }

            // Double check the magic word
            else if (e.Result == "please") { navService.Navigate(new Uri("/View/Pages/Page1.xaml", UriKind.Relative)); }
        }

    }
}
