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

            LoadContentPropAttrs();
        }

        private void LoadContentPropAttrs()
        {
            // SortedList to store class and content property.
            SortedList<string, string> listClass = new SortedList<string, string>();

            // Formatting string.
            string strFormat = "{0,-35}{1}";

            // Loop through the loaded assemblies.
            foreach (AssemblyName asmblyname in
                        Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                // Loop through the types.
                foreach (Type type in Assembly.Load(asmblyname).GetTypes())
                {
                    // Loop through the custom attributes.
                    // (Set argument to 'false' for non-inherited only!)
                    foreach (object obj in type.GetCustomAttributes(
                                            typeof(ContentPropertyAttribute), true))
                    {
                        // Add to list if ContentPropertyAttribute.
                        if (type.IsPublic && obj as ContentPropertyAttribute != null)
                            listClass.Add(type.Name,
                                          (obj as ContentPropertyAttribute).Name);
                    }
                }
            }

            var sb = new StringBuilder();

            // Display the results.
            sb.AppendLine(string.Format(strFormat, "Class", "Content Property"));
            sb.AppendLine(string.Format(strFormat, "-----", "----------------"));

            foreach (string strClass in listClass.Keys)
                sb.AppendLine(string.Format(strFormat, strClass, listClass[strClass]));

            txtTemplate2.Text = sb.ToString();
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
