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

namespace CustomDialogSample {
  public partial class Window1 : System.Windows.Window {

    public Window1() {
      InitializeComponent();

      settingsButton.Click += new RoutedEventHandler(settingsButton_Click);
      Closing += new System.ComponentModel.CancelEventHandler(Window1_Closing);
    }

    void Window1_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
      Properties.Settings.Default.Save();
    }

    void settingsButton_Click(object sender, RoutedEventArgs e) {
      // Create dialog and show it modally, letting it know its owner
      SettingsDialog dlg = new SettingsDialog();
      dlg.Owner = this;
      dlg.ReportColor = Properties.Settings.Default.ReportColor;
      dlg.ReportFolder = Properties.Settings.Default.ReportFolder;
      if (dlg.ShowDialog() == true) {
        Properties.Settings.Default.ReportColor = dlg.ReportColor;
        Properties.Settings.Default.ReportFolder = dlg.ReportFolder;
      }

      //// Create dialog and show it modelessly, letting it know its owner
      //SettingsDialog dlg = new SettingsDialog();
      //dlg.Owner = this;
      //dlg.Show();
    }

  }
}