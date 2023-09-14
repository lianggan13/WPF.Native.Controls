using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WPF.Common.Utility
{
    public static class VisualTreeSearcher
    {
        public static List<T> FindVisualChildren<T>(DependencyObject obj, Func<T, bool> where) where T : DependencyObject
        {
            List<T> childList = new List<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is DependencyObject dChild)
                {
                    if (child is T tChild && (where?.Invoke(tChild) == true))
                    {
                        childList.Add(tChild);
                    }
                    childList.AddRange(FindVisualChildren<T>(dChild, where));
                }
            }
            return childList;
        }

        public static IEnumerable<T> GetVisualChildren<T>(DependencyObject dp) where T : DependencyObject
        {
            if (dp != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dp); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(dp, i);

                    if (child != null && child is T t)
                        yield return t;

                    foreach (T childOfChild in GetVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }

        public static T GetVisualParent<T>(DependencyObject dp) where T : DependencyObject
        {
            if (dp != null)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(dp);
                if (parent is T t)
                {
                    return t;
                }
                if (parent == null)
                {
                    return default;
                }
                return GetVisualParent<T>(parent);
            }
            return default;
        }
    }
}
