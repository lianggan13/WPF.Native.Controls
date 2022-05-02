// Page1.xaml.cs
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PageState
{
    public partial class Page1 : Page
    {
        int answer = (new Random()).Next();

        public Page1()
        {
            InitializeComponent();
            answerBox.Text = answer.ToString();
            //Loaded += Page1_Loaded;
            guessLink.Click += guessLink_Click;
        }

        void guessLink_Click(object sender, RoutedEventArgs e)
        {
            Page2 page2 = new Page2();
            page2.Answer = answer;
            page2.Guess = int.Parse(guessBox.Text);
            NavigationService.Navigate(page2);
        }

        //void Page1_Loaded(object sender, RoutedEventArgs e) {
        //  Window parent = Parent as Window;
        //  if( parent != null ) { parent.Width = 400; parent.Height = 200; }

        //  // Check for the magic word
        //  MagicWordPageFunction fn = new MagicWordPageFunction();
        //  fn.MagicWord = "please";
        //  fn.Return += fn_Return;
        //  NavigationService.Navigate(fn);
        //}

        //void fn_Return(object sender, ReturnEventArgs<string> e) {
        //  if( e == null ) { NavigationService.Navigate(new Uri("QuitterPage.xaml", UriKind.Relative)); }

        //  // double check...
        //  Debug.Assert(e.Result == "please");
        //}

    }
}
