//---------------------------------------------
// TypeToString.cs (c) 2006 by Charles Petzold
//---------------------------------------------
using System;
using System.Globalization;
using System.Windows.Data;

namespace WPF.TreeView.ClassHierarchy
{
    class TypeToString : IValueConverter
    {
        public object Convert(object obj, Type type, object param,
                              CultureInfo culture)
        {
            return (obj as Type).Name;
        }
        public object ConvertBack(object obj, Type type, object param,
                                  CultureInfo culture)
        {
            return null;
        }
    }
}
