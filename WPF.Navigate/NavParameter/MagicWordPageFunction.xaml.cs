// MagicWordPageFunction.xaml.cs
using System.Windows;
using System.Windows.Navigation;

namespace WPF.Navigate.NavParameter
{
    public partial class MagicWordPageFunction : PageFunction<string>
    {
        public MagicWordPageFunction()
        {
            InitializeComponent();
            Loaded += MagicWordPageFunction_Loaded;
            playLink.Click += playLink_Click;
            quitLink.Click += quitLink_Click;
        }

        string magicWord;
        public string MagicWord
        {
            get { return magicWord; }
            set { magicWord = value; }
        }

        void MagicWordPageFunction_Loaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties.Contains("MagicWordEntered") &&
                (string)Application.Current.Properties["MagicWordEntered"] == magicWord)
            {
                // No need to re-enter the magic word for subsequent games
                OnReturn(new ReturnEventArgs<string>(magicWord));
            }
        }

        void playLink_Click(object sender, RoutedEventArgs e)
        {
            // Check to see if the magic word is the right one
            if (wordBox.Text == magicWord)
            {
                Application.Current.Properties["MagicWordEntered"] = wordBox.Text;
                OnReturn(new ReturnEventArgs<string>(wordBox.Text));
            }
        }

        void quitLink_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(null); // Cancel
        }

    }
}
