using Microsoft.Win32;

namespace WPF.Window.Common
{
    using System.Windows;

    public class WindowPositionHelper
    {
        public static string RegPath = @"Software\MyApp\";

        public static void SaveSize(Window win)
        {
            // Create or retrieve a reference to a key where the settings
            // will be stored.
            RegistryKey key;
            key = Registry.CurrentUser.CreateSubKey(RegPath + win.Name);

            key.SetValue("Bounds", win.RestoreBounds.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }

        public static void SetSize(Window win)
        {
            RegistryKey key;
            key = Registry.CurrentUser.OpenSubKey(RegPath + win.Name);

            if (key != null)
            {
                Rect bounds = Rect.Parse(key.GetValue("Bounds").ToString());

                win.Top = bounds.Top;
                win.Left = bounds.Left;

                // Only restore the size for a manually sized
                // window.
                if (win.SizeToContent == SizeToContent.Manual)
                {
                    win.Width = bounds.Width;
                    win.Height = bounds.Height;
                }
            }
        }
    }
}
