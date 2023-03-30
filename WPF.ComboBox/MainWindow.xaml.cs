using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;


namespace WPFComboBox
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

        }


        [DllImport("User32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        private IntPtr GetHwnd(Popup popup)
        {
            HwndSource source = (HwndSource)PresentationSource.FromVisual(popup.Child);
            return source.Handle;
        }

        private void PART_Popup_Opened(object sender, System.EventArgs e)
        {
            Popup popup = sender as Popup;
            IntPtr handle = GetHwnd(popup);
            SetFocus(handle);
        }

        private void PART_Popup_GotFocus(object sender, RoutedEventArgs e)
        {
            //Popup popup = sender as Popup;
            //var source = (HwndSource)PresentationSource.FromVisual(popup.Child);
            //if (source != null)
            //{
            //    SetFocus(source.Handle);
            //}
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {

        }
    }


}
