namespace WPF.Window
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;


    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Assembly assembly = Assembly.GetExecutingAssembly();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                string tyname = btn.Content.ToString();
                var tydest = assembly.DefinedTypes.OfType<TypeInfo>().FirstOrDefault(t => t.Name == tyname);
                var obj = Activator.CreateInstance(assembly.FullName, tydest.FullName);
                if (obj.Unwrap() is Window win)
                {
                    win.Owner = this;
                    win.ShowDialog();
                }
            }


        }
    }
}
