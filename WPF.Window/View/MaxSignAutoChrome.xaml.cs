using System;
using System.Windows.Input;

namespace WPF.Window.View
{
    using System.Windows;
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MaxSignAutoChrome : Window
    {
        public MaxSignAutoChrome()
        {
            InitializeComponent();
            //this.MaxHeight = SystemParameters.PrimaryScreenHeight;
        }

        #region frame ui
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);

        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ShowCloseErr()
        {
            return;
        }
        #endregion

        #region language
        /// <summary>
        /// 点击切换语言
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion



    }
}
