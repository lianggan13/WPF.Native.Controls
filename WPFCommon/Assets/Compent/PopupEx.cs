using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;

namespace WPFCommon.Assets.Compent
{
    public class PopupEx : Popup
    {
        public static readonly DependencyProperty IsPositionUpdateProperty =
          DependencyProperty.Register("IsPositionUpdate",
                                      typeof(bool),
                                      typeof(PopupEx),
                                      new PropertyMetadata(true, new PropertyChangedCallback(OnIsPositionUpdateChanged)));

        //是否最前默认为非最前（false）  
        public static DependencyProperty TopmostProperty = Window.TopmostProperty.AddOwner(typeof(Popup),
                                                                                           new FrameworkPropertyMetadata(false, OnTopmostChanged));

        private static void OnIsPositionUpdateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PopupEx)
            {
                var pupEx = d as PopupEx;
                pupEx.pup_Loaded(pupEx, null);
                pupEx.RegisterEventsInWindow(pupEx.OwnerWindow, pupEx.IsPositionUpdate);
            }
        }

        private static void OnTopmostChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as PopupEx).UpdateWindow();
        }

        /// <summary>  
        /// 是否窗口随动，默认为随动（true）  
        /// </summary>  
        public bool IsPositionUpdate
        {
            get { return (bool)GetValue(IsPositionUpdateProperty); }
            set { SetValue(IsPositionUpdateProperty, value); }
        }

        public bool Topmost
        {
            get { return (bool)GetValue(TopmostProperty); }
            set { SetValue(TopmostProperty, value); }
        }

        /// <summary>
        /// PopupEx 所属的 Window
        /// </summary>
        public Window OwnerWindow { get; set; }

        /// <summary>
        /// Popup 更新位置内部方法信息
        /// </summary>
        public MethodInfo UpdatePositionMethodInfo { get; set; }

        /// <summary>  
        /// 加载窗口随动事件  
        /// </summary>  
        public PopupEx()
        {
            this.Loaded += pup_Loaded;

            // find UpdatePosition Method
            UpdatePositionMethodInfo = typeof(Popup).GetMethod("UpdatePosition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        }

        private void pup_Loaded(object sender, RoutedEventArgs e)
        {
            // find owner window
            OwnerWindow = Window.GetWindow(sender as PopupEx);
        }

        /// <summary>  
        /// 重写拉开方法，置于非最前  
        /// </summary>  
        /// <param name="e"></param>  
        protected override void OnOpened(EventArgs e)
        {
            UpdateWindow();
            RegisterEventsInWindow(OwnerWindow, IsPositionUpdate);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            CancelEventsListeningInWindow(OwnerWindow);
        }

        /// <summary>
        /// 给OwnerWindow注册更新Popup控件位置事件
        /// </summary>
        /// <param name="window"></param>
        /// <param name="isPositionUpdate"></param>
        public void RegisterEventsInWindow(Window window, bool isPositionUpdate)
        {
            CancelEventsListeningInWindow(window);
            if (isPositionUpdate)
            {
                window.LocationChanged += PositionChanged;
                window.SizeChanged += PositionChanged;
            }
        }

        public void CancelEventsListeningInWindow(Window window)
        {
            window.LocationChanged -= PositionChanged;
            window.SizeChanged -= PositionChanged;
        }

        /// <summary>  
        /// 刷新位置  
        /// </summary>  
        private void PositionChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.IsOpen)
                {
                    UpdatePositionMethodInfo?.Invoke(this, null);
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>  
        /// 更新Popup层级
        /// </summary>  
        private void UpdateWindow()
        {
            var hwnd = ((HwndSource)PresentationSource.FromVisual(this.Child)).Handle;
            RECT rect;
            if (NativeMethods.GetWindowRect(hwnd, out rect))
            {
                NativeMethods.SetWindowPos(hwnd, Topmost ? -1 : -2, rect.Left, rect.Top, (int)this.Width, (int)this.Height, 0);
            }

            NativeMethods.SetFocus(hwnd);

            //ControlHelper.SetChildFocus(this.Child);

            //if (this.Child?.IsFocused == true)
            //{
            //    Trace.WriteLine("Focused");
            //}
            //else
            //{
            //    Trace.WriteLine("No Focused");

            //}
        }


        #region P/Invoke imports & definitions  
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static class NativeMethods
        {
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
            [DllImport("user32", EntryPoint = "SetWindowPos")]
            internal static extern int SetWindowPos(IntPtr hWnd, int hwndInsertAfter, int x, int y, int cx, int cy, int wFlags);

            [DllImport("User32.dll")]
            public static extern IntPtr SetFocus(IntPtr hWnd);


        }
        #endregion
    }

    public class ControlHelper
    {
        /// <summary>
        /// 获取鼠标点击的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static T GetHitUIElement<T>(Control parent, Point pos) where T : class
        {
            T tobj = null;
            HitTestResult result = VisualTreeHelper.HitTest(parent, pos);
            if (result != null)
            {
                tobj = FindVisualParent<T>(result.VisualHit);
            }
            return tobj;
        }

        public static T FindVisualParent<T>(DependencyObject obj) where T : class
        {
            while (obj != null)
            {
                if (obj is T)
                    return obj as T;

                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }

        //public static bool SetChildFocus(UIElement obj)
        //{
        //    foreach (var child in obj.)
        //    {
        //        if (child is TextBox ui)
        //        {
        //            if (ui.Focusable)
        //            {
        //                ui.Focus();
        //                return true;
        //            }
        //        }
        //        if (SetChildFocus(child as DependencyObject))
        //            return true;
        //    }

        //    return false;
        //}

        public static List<T> GetChildObjects<T>(DependencyObject obj, Type typename) where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).GetType() == typename))
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child, typename));
            }
            return childList;
        }

        public static List<T> GetLogicChildObjects<T>(DependencyObject obj, Type typename) where T : FrameworkElement
        {
            List<T> childList = new List<T>();
            foreach (var child in LogicalTreeHelper.GetChildren(obj))
            {
                if (child is DependencyObject)
                {
                    if (child is T && (((T)child).GetType() == typename))
                    {
                        childList.Add((T)child);
                    }
                    childList.AddRange(GetLogicChildObjects<T>(child as DependencyObject, typename));
                }
            }

            return childList;
        }

        public static List<T> GetLogicFisrtChildObject<T>(DependencyObject obj, Type typename) where T : FrameworkElement
        {
            List<T> childList = new List<T>();
            foreach (var child in LogicalTreeHelper.GetChildren(obj))
            {
                if (child is DependencyObject)
                {
                    if (child is T && (((T)child).GetType() == typename))
                    {
                        childList.Add((T)child);
                    }
                    if (childList.Count == 0)
                    {
                        childList.AddRange(GetLogicChildObjects<T>(child as DependencyObject, typename));
                    }
                    else
                    {
                        return childList;
                    }
                }
            }

            return childList;
        }

        public static childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public static TreeViewItem GetParentObjectEx<TreeViewItem>(DependencyObject obj) where TreeViewItem : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                if (parent is TreeViewItem)
                {
                    return (TreeViewItem)parent;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }
    }

}
