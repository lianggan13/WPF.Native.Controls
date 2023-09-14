using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF.Mouse.Views
{
    /// <summary>
    /// DrawCircles.xaml 的交互逻辑
    /// </summary>
    public partial class DrawCircles : UserControl
    {
        public DrawCircles()
        {
            InitializeComponent();
        }

        // Drawing-Related fields.
        bool isDrawing;
        Ellipse elips;
        Point ptCenter;

        // Dragging-Related fields.
        bool isDragging;
        FrameworkElement elDragging;
        Point ptMouseStart, ptElementStart;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (isDragging)
                return;

            // Create a new Ellipse object and add it to canvas.
            ptCenter = e.GetPosition(canv);
            elips = new Ellipse();
            elips.Stroke = SystemColors.WindowTextBrush;
            elips.StrokeThickness = 1;
            elips.Width = 0;
            elips.Height = 0;
            canv.Children.Add(elips);
            Canvas.SetLeft(elips, ptCenter.X);
            Canvas.SetTop(elips, ptCenter.Y);

            // Capture the mouse and prepare for future events.
            CaptureMouse();
            isDrawing = true;
        }
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);

            if (isDrawing)
                return;

            // Get the clicked element and prepare for future events.
            ptMouseStart = e.GetPosition(canv);
            elDragging = canv.InputHitTest(ptMouseStart) as FrameworkElement;

            if (elDragging != null)
            {
                ptElementStart = new Point(Canvas.GetLeft(elDragging),
                                           Canvas.GetTop(elDragging));
                isDragging = true;
            }
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.ChangedButton == MouseButton.Middle)
            {
                Shape shape = canv.InputHitTest(e.GetPosition(canv)) as Shape;

                if (shape != null)
                    shape.Fill = (shape.Fill == Brushes.Red ?
                                        Brushes.Transparent : Brushes.Red);
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Point ptMouse = e.GetPosition(canv);

            // Move and resize the Ellipse.
            if (isDrawing)
            {
                // ptCenter: 最初的鼠标点击 确定 圆心
                // ptMouse:  随后移动中的鼠标位置 确定 半径
                double dRadius = Math.Sqrt(Math.Pow(ptCenter.X - ptMouse.X, 2) +
                                           Math.Pow(ptCenter.Y - ptMouse.Y, 2));

                Canvas.SetLeft(elips, ptCenter.X - dRadius);
                Canvas.SetTop(elips, ptCenter.Y - dRadius);
                elips.Width = 2 * dRadius;
                elips.Height = 2 * dRadius;
            }
            // Move the ellipse
            else if (isDragging)
            {
                Canvas.SetLeft(elDragging,
                    ptElementStart.X + ptMouse.X - ptMouseStart.X);
                Canvas.SetTop(elDragging,
                    ptElementStart.Y + ptMouse.Y - ptMouseStart.Y);
            }
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            // End the drawing operation.
            if (isDrawing && e.ChangedButton == MouseButton.Left)
            {
                elips.Stroke = Brushes.Blue;
                elips.StrokeThickness = Math.Min(24, elips.Width / 2);
                elips.Fill = Brushes.Red;

                isDrawing = false;
                ReleaseMouseCapture();
            }
            // End the capture operation.
            else if (isDragging && e.ChangedButton == MouseButton.Right)
            {
                isDragging = false;
            }
        }
        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);

            // End drawing or dragging with press of Escape key.
            if (e.Text.IndexOf('\x1B') != -1)
            {
                if (isDrawing)
                    ReleaseMouseCapture();

                else if (isDragging)
                {
                    Canvas.SetLeft(elDragging, ptElementStart.X);
                    Canvas.SetTop(elDragging, ptElementStart.Y);
                    isDragging = false;
                }
            }
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);

            // Abnormal end of drawing: Remove child Ellipse.
            if (isDrawing)
            {
                canv.Children.Remove(elips);
                isDrawing = false;
            }
        }
    }
}
