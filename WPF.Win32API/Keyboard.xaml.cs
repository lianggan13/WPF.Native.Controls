using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VisualKeyboard.Common;

namespace VisualKeyboard
{
    /// <summary>
    /// Keyboard.xaml 的交互逻辑
    /// </summary>
    public partial class Keyboard : UserControl
    {
        public Keyboard()
        {
            InitializeComponent();
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var KeyCode = (sender as Button).CommandParameter.ToString();
            if (KeyCode.Contains("P")) // # 51P
            {
                Win32API.OnKeyDown(Byte.Parse("161")); // Shift 161
                Win32API.OnKeyDown(Byte.Parse(KeyCode.Substring(0, KeyCode.Length - 1)));
                Win32API.OnKeyUp(Byte.Parse("161"));   // Shift 161
            }
            else
            {
                Win32API.OnKeyPress(Byte.Parse(KeyCode));
            }
        }
    }
}
