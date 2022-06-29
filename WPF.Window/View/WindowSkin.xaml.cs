using System;

using System.Windows.Controls;

namespace WPF.Window.View
{
    using System.Windows;
    /// <summary>
    /// WindowSkin.xaml 的交互逻辑
    /// </summary>
    public partial class WindowSkin : Window
    {
        public WindowSkin()
        {
            InitializeComponent();

            Application.Current.Properties["Blue"] = (ResourceDictionary)Application.LoadComponent(new Uri("/Assets/Styles/Style.BlueSkin.xaml", UriKind.Relative));
            Application.Current.Properties["Yellow"] = (ResourceDictionary)Application.LoadComponent(new Uri("/Assets/Styles/Style.YellowSkin.xaml", UriKind.Relative));

            // Add choices to combo box
            skinComboBox.Items.Add("Blue");
            skinComboBox.Items.Add("Yellow");
            skinComboBox.SelectedIndex = 0;

            // Set initial skin
            Application.Current.Resources = (ResourceDictionary)Application.Current.Properties["Blue"];

            // Detect when skin changes
            skinComboBox.SelectionChanged += skinComboBox_SelectionChanged;
        }

        private void newChildWindowButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new skind child window
            //var window = new ChildWindow();
            //window.Show();
        }

        private void skinComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Change the skin
            var selectedValue = (string)e.AddedItems[0];
            Application.Current.Resources = (ResourceDictionary)Application.Current.Properties[selectedValue];
        }
    }
}
