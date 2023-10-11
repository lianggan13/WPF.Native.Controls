using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF.Keyboard.Views
{
    /// <summary>
    /// ControlXCV.xaml 的交互逻辑
    /// </summary>
    public partial class ControlXCV : UserControl
    {
        KeyGesture gestCut = new KeyGesture(Key.X, ModifierKeys.Control, "CTRL+X");
        KeyGesture gestCopy = new KeyGesture(Key.C, ModifierKeys.Control, "CTRL+C");
        KeyGesture gestPaste = new KeyGesture(Key.V, ModifierKeys.Control, "CTRL+V");
        KeyGesture gestDelete = new KeyGesture(Key.Delete, ModifierKeys.None, "DELETE");

        public ControlXCV()
        {
            InitializeComponent();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs args)
        {
            base.OnKeyDown(args);
            args.Handled = true;

            string str = null;

            if (gestCut.Matches(this, args))
                str = gestCut.DisplayString;

            else if (gestCopy.Matches(this, args))
                str = gestCopy.DisplayString;

            else if (gestPaste.Matches(this, args))
                str = gestPaste.DisplayString;

            else if (gestDelete.Matches(this, args))
                str = gestDelete.DisplayString;

            else
                args.Handled = false;

            if (!string.IsNullOrEmpty(str))
                MessageBox.Show($"You pressed: {str}");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            scroll.Focus();
        }
    }
}
