using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace WPF.Template.Views
{
    /// <summary>
    /// ControlTemplateBrowser.xaml 的交互逻辑
    /// </summary>
    public partial class ControlTemplateBrowser : UserControl
    {
        public ControlTemplateBrowser()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Type controlType = typeof(Control);
            List<Type> derivedTypes = new List<Type>();

            // Search all the types in the assembly where the Control class is defined.
            Assembly assembly = Assembly.GetAssembly(typeof(Control));
            foreach (Type type in assembly.GetTypes())
            {
                // Only add a type of the list if it's a Control, a concrete class, and public.
                if (type.IsSubclassOf(controlType) && !type.IsAbstract && type.IsPublic)
                {
                    derivedTypes.Add(type);
                }
            }

            // Sort the types by type name.
            derivedTypes.Sort(new TypeComparer());

            // Show the list of types.
            lstTypes.ItemsSource = derivedTypes;
        }

        private void lstTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Get the selected type.
                Type type = (Type)lstTypes.SelectedItem;

                // Instantiate the type.
                ConstructorInfo info = type.GetConstructor(System.Type.EmptyTypes);
                Control control = (Control)info.Invoke(null);

                Window win = control as Window;
                if (win != null)
                {
                    // Create the window (but keep it minimized).
                    win.WindowState = System.Windows.WindowState.Minimized;
                    win.ShowInTaskbar = false;
                    win.Show();
                }
                else
                {
                    // Add it to the grid (but keep it hidden).
                    control.Visibility = Visibility.Collapsed;
                    grid.Children.Add(control);
                }

                // Get the template.
                ControlTemplate template = control.Template;

                // Get the XAML for the template.
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                StringBuilder sb = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(sb, settings);
                XamlWriter.Save(template, writer);

                // Display the template.
                txtTemplate.Text = sb.ToString();

                // Remove the control from the grid.
                if (win != null)
                {
                    win.Close();
                }
                else
                {
                    grid.Children.Remove(control);
                }
            }
            catch (Exception err)
            {
                txtTemplate.Text = "<< Error generating template: " + err.Message + ">>";
            }
        }

        public class TypeComparer : IComparer<Type>
        {
            public int Compare(Type x, Type y)
            {
                return x.Name.CompareTo(y.Name);
            }
        }


    }



}
