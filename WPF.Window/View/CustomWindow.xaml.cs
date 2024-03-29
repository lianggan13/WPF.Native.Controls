﻿using System;
using System.Windows.Input;
using System.Windows.Shapes;

namespace WPF.Window.View
{
    using System.Windows;
    /// <summary>
    /// CustomWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CustomWindow : Window
    {
        public CustomWindow()
        {
            InitializeComponent();
        }

        private bool isResizing = false;
        [Flags()]
        private enum ResizeType
        {
            Width, Height
        }
        private ResizeType resizeType;


        private void window_initiateResizeWE(object sender, System.Windows.Input.MouseEventArgs e)
        {
            isResizing = true;
            resizeType = ResizeType.Width;
        }
        private void window_initiateResizeNS(object sender, System.Windows.Input.MouseEventArgs e)
        {
            isResizing = true;
            resizeType = ResizeType.Height;
        }

        private void window_endResize(object sender, System.Windows.Input.MouseEventArgs e)
        {
            isResizing = false;

            // Make sure capture is released.
            Rectangle rect = (Rectangle)sender;
            rect.ReleaseMouseCapture();
        }

        private void window_Resize(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            Window win = (Window)rect.TemplatedParent;

            if (isResizing)
            {
                rect.CaptureMouse();
                if (resizeType == ResizeType.Width)
                {
                    double width = e.GetPosition(win).X + 5;
                    if (width > 0) win.Width = width;
                }
                if (resizeType == ResizeType.Height)
                {
                    double height = e.GetPosition(win).Y + 5;
                    if (height > 0) win.Height = height;
                }
            }
        }




        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window win = this;//(Window)
                              //((FrameworkElement)sender).TemplatedParent;
            win.DragMove();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            Window win = this;//(Window)
                              //((FrameworkElement)sender).TemplatedParent;
            win.Close();
        }
    }
}
