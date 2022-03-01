using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFMouse
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Line line = null;
        private void Canvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var border = e.Source as Border;
            if (border == null) return;

            line = new Line
            {
                Stroke = Brushes.Chocolate,
                StrokeThickness = 2
            };
            line.X1 = Canvas.GetLeft(border) + border.ActualWidth / 2;
            line.Y1 = Canvas.GetTop(border) + border.ActualHeight;
            line.X2 = line.X1;
            line.Y2 = line.Y1;

            canvas.Children.Add(line);

            LineNode.SetNextLine(border, line);

            canvas.PreviewMouseMove -= Canvas_PreviewMouseMove_Line;
            canvas.PreviewMouseMove += Canvas_PreviewMouseMove_Line;
        }

        private void Canvas_PreviewMouseMove_Line(object sender, MouseEventArgs e)
        {
            if (line == null)
                return;

            Point relativePos = e.GetPosition(canvas);
            double offset = 2;
            line.X2 = relativePos.X;
            line.Y2 = relativePos.Y - offset;
        }

        private void Canvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            canvas.PreviewMouseMove -= Canvas_PreviewMouseMove_Line;

            Mouse.Capture(null);

            IInputElement element = Mouse.DirectlyOver;
            Border border = GetNodeParent((DependencyObject)element);
            if (border == null || !(element is Border))
            {
                canvas.Children.Remove(line);
                return;
            }

            line.X2 = Canvas.GetLeft(border) + border.ActualWidth / 2;
            line.Y2 = Canvas.GetTop(border);// + border.ActualHeight;

            LineNode.SetPreLine(border, line);
        }


        private Border GetNodeParent(DependencyObject obj)
        {
            if (obj is null)
            {
                return null;
            }
            else if (obj is Border border)
            {
                return border;
            }
            else
            {
                var obj2 = VisualTreeHelper.GetParent(obj);
                return GetNodeParent(obj2);
            }
        }


        Point downPoint;
        private void Canvas_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Border border)
            {
                downPoint = e.GetPosition(border);

                Mouse.Capture((IInputElement)e.Source);        // 捕获鼠标按下的元素
                border.SetValue(Panel.ZIndexProperty, 100);  // 设置元素在最顶层 

                canvas.PreviewMouseMove += Canvas_PreviewMouseMove_Border;
            }

        }

        private void Canvas_PreviewMouseMove_Border(object sender, MouseEventArgs e)
        {
            var border = e.Source as Border;

            Point relativePos = e.GetPosition(canvas);

            Canvas.SetLeft(border, relativePos.X - downPoint.X);
            Canvas.SetTop(border, relativePos.Y - downPoint.Y);

            Line preLine = LineNode.GetPreLine(border);
            Line nextLine = LineNode.GetNextLine(border);

            if (preLine != null)
            {
                preLine.X2 = Canvas.GetLeft(border) + border.ActualWidth / 2;
                preLine.Y2 = Canvas.GetTop(border);
            }

            if (nextLine != null)
            {
                nextLine.X1 = Canvas.GetLeft(border) + border.ActualWidth / 2;
                nextLine.Y1 = Canvas.GetTop(border) + border.ActualHeight;
            }
        }

        private void Canvas_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            (e.Source as FrameworkElement).SetValue(Panel.ZIndexProperty, 0);

            canvas.PreviewMouseMove -= Canvas_PreviewMouseMove_Border;
        }
    }

    public class LineNode
    {
        public static Line GetPreLine(DependencyObject obj)
        {
            return (Line)obj.GetValue(PreLineProperty);
        }

        public static void SetPreLine(DependencyObject obj, Line value)
        {
            obj.SetValue(PreLineProperty, value);
        }

        // Using a DependencyProperty as the backing store for PreLine.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreLineProperty =
            DependencyProperty.RegisterAttached("PreLine", typeof(Line), typeof(LineNode), new PropertyMetadata(null));



        public static Line GetNextLine(DependencyObject obj)
        {
            return (Line)obj.GetValue(NextLineProperty);
        }

        public static void SetNextLine(DependencyObject obj, Line value)
        {
            obj.SetValue(NextLineProperty, value);
        }

        // Using a DependencyProperty as the backing store for NextLine.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextLineProperty =
            DependencyProperty.RegisterAttached("NextLine", typeof(Line), typeof(LineNode), new PropertyMetadata(null));



    }
}
