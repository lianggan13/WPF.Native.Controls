using System;
using System.Windows.Controls;

namespace WPF.Panel.Views
{
    /// <summary>
    /// DiagonalizeButtons.xaml 的交互逻辑
    /// </summary>
    public partial class DiagonalizeButtons : UserControl
    {
        public DiagonalizeButtons()
        {
            InitializeComponent();

            Random rand = new Random();

            for (int i = 0; i < 5; i++)
            {
                Button btn = new Button();
                btn.Content = "Button Number " + (i + 1);
                btn.FontSize += rand.Next(20);
                pnl.Add(btn);
            }
        }
    }
}
