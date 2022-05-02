// MagicWordPageFunction.xaml.cs
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
using System.ComponentModel;
using System.Diagnostics;

namespace PageState {
  public partial class MagicWordPageFunction : PageFunction<string> {
    public MagicWordPageFunction() {
      InitializeComponent();
      Loaded += MagicWordPageFunction_Loaded;
      playLink.Click += playLink_Click;
      quitLink.Click += quitLink_Click;
    }

    string magicWord;
    public string MagicWord {
      get { return magicWord; }
      set { magicWord = value; }
    }

    void MagicWordPageFunction_Loaded(object sender, RoutedEventArgs e) {
      if( Application.Current.Properties.Contains("MagicWordEntered") &&
          (string)Application.Current.Properties["MagicWordEntered"] == magicWord ) {
        // No need to re-enter the magic word for subsequent games
        OnReturn(new ReturnEventArgs<string>(magicWord));
      }
    }

    void playLink_Click(object sender, RoutedEventArgs e) {
      // Check to see if the magic word is the right one
      if( wordBox.Text == magicWord ) {
        Application.Current.Properties["MagicWordEntered"] = wordBox.Text;
        OnReturn(new ReturnEventArgs<string>(wordBox.Text));
      }
    }

    void quitLink_Click(object sender, RoutedEventArgs e) {
      OnReturn(null); // Cancel
    }

  }
}
