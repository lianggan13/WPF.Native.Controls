//----------------------------------------------------
// ClassHierarchyTreeView (c) 2006 by Charles Petzold
//----------------------------------------------------
namespace WPF.TreeView.ClassHierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    public class TypeTreeView : TreeView
    {
        public Type TypeRoot
        {
            get { return (Type)GetValue(TypeRootProperty); }
            set { SetValue(TypeRootProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TypeRoot.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeRootProperty =
            DependencyProperty.Register(nameof(TypeRoot), typeof(Type), typeof(TypeTreeView), new PropertyMetadata(default(Type), OnTypeRootChanged));

        private static void OnTypeRootChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TypeTreeView).LoadTypeRootClass();
        }

        private void LoadTypeRootClass()
        {
            // Make sure PresentationCore is loaded.
            UIElement dummy = new UIElement();

            // Put all the referenced assemblies in a List.
            List<Assembly> assemblies = new List<Assembly>();

            // Get all referenced assemblies.
            AssemblyName[] anames =
                    Assembly.GetExecutingAssembly().GetReferencedAssemblies();

            // Add to assemblies list.
            foreach (AssemblyName aname in anames)
                assemblies.Add(Assembly.Load(aname));

            // Store descendants of typeRoot in a sorted list.
            SortedList<string, Type> classes = new SortedList<string, Type>();
            classes.Add(TypeRoot.Name, TypeRoot);

            // Get all the types in the assembly.
            foreach (Assembly assembly in assemblies)
                foreach (Type typ in assembly.GetTypes())
                    if (typ.IsPublic && typ.IsSubclassOf(TypeRoot))
                        classes.Add(typ.Name, typ);

            Items.Clear();

            // Create root item.
            TypeTreeViewItem item = new TypeTreeViewItem(TypeRoot);
            Items.Add(item);

            // Call recursive method.
            CreateLinkedItems(item, classes);
        }

        private void CreateLinkedItems(TypeTreeViewItem itemBase,
                                 SortedList<string, Type> list)
        {
            foreach (KeyValuePair<string, Type> kvp in list)
                if (kvp.Value.BaseType == itemBase.Type)
                {
                    TypeTreeViewItem item = new TypeTreeViewItem(kvp.Value);
                    itemBase.Items.Add(item);
                    CreateLinkedItems(item, list);
                }
        }
    }
}
