// Page2.xaml.cs
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Collections;


namespace PageState {
  public partial class Page2 : Page {
    public Page2() {
      InitializeComponent();
      Loaded += Page2_Loaded;
      tryAgainLink.Click += tryAgainLink_Click;
    }

    int answer;
    public int Answer {
      get { return answer; }
      set { answer = value; }
    }

    int guess;
    public int Guess {
      get { return guess; }
      set { guess = value; }
    }

    void Page2_Loaded(object sender, RoutedEventArgs e) {
      guessBlock.Text = guess.ToString();

      if( answer == guess ) { resultBlock.Text = "You win!"; TrackWin(); }
      else if( answer < guess ) { resultBlock.Text = "Guess lower..."; }
      else { resultBlock.Text = "Guess higher..."; }
    }

    void tryAgainLink_Click(object sender, RoutedEventArgs e) {
      // If they won, start a new game
      if( answer == guess ) {
        NavigationService.Navigate(new Uri("Page1.xaml", UriKind.Relative));
      }
      // Otherwise, let them guess again
      else { NavigationService.GoBack(); }
    }

    // NOTE: uniqueness testing to make sure that every won game
    // is only tracked once is left as an exercise to the reader
    // (Send answers to csells@sellsbrothers.com...)
    void TrackWin() {
      IDictionary properties = Application.Current.Properties;
      if( !properties.Contains("GamesWon") ) { properties["GamesWon"] = 0; }
      properties["GamesWon"] = (int)properties["GamesWon"] + 1;
    }

  }
}