using System.Text;
using System.Windows;

namespace WPFWindow
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}\n", e.Exception.InnerException.Message);
            sb.AppendFormat("{0}\n", e.Exception.Message);
            sb.AppendFormat("Exception handled on main UI thread {0}.", e.Dispatcher.Thread.ManagedThreadId);
            MessageBox.Show(sb.ToString());

            // Keep application running in the face of this exception
            e.Handled = true;
        }
    }
}
