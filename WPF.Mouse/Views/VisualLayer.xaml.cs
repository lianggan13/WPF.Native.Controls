using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF.Mouse.Views
{
    /// <summary>
    /// VisualLayer.xaml 的交互逻辑
    /// </summary>
    public partial class VisualLayer : UserControl
    {

        public VisualLayer()
        {
            InitializeComponent();
            DrawingVisual v = new DrawingVisual();
            DrawSquare(v, new Point(10, 10), false);
        }

        // Variables for dragging shapes.
        private bool isDragging = false;
        private Vector clickOffset;
        private DrawingVisual selectedVisual;

        // Variables for drawing the selection square.
        private bool isMultiSelecting = false;
        private Point selectionSquareTopLeft;

        private void drawingSurface_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pointClicked = e.GetPosition(drawingSurface);

            if (cmdAdd.IsChecked == true)
                AddVisual(pointClicked);
            else if (cmdDelete.IsChecked == true)
                DeleteVisual(pointClicked);
            else if (cmdSelectMove.IsChecked == true)
            {
                DrawingVisual visual = drawingSurface.GetVisual(pointClicked);
                if (visual != null)
                {
                    // Calculate the top-left corner of the square.
                    // This is done by looking at the current bounds and
                    // removing half the border (pen thickness).
                    // An alternate solution would be to store the top-left
                    // point of every visual in a collection in the 
                    // DrawingCanvas, and provide this point when hit testing.
                    Point topLeftCorner = new Point(
                        visual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2,
                        visual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2);
                    DrawSquare(visual, topLeftCorner, true);

                    clickOffset = topLeftCorner - pointClicked;
                    isDragging = true;

                    if (selectedVisual != null && selectedVisual != visual)
                    {
                        // The selection has changed. Clear the previous selection.
                        ClearSelection();
                    }
                    selectedVisual = visual;
                }
            }
            else if (cmdSelectMultiple.IsChecked == true)
            {

                selectionSquare = new DrawingVisual();

                drawingSurface.AddVisual(selectionSquare);

                selectionSquareTopLeft = pointClicked;
                isMultiSelecting = true;

                // Make sure we get the MouseLeftButtonUp event even if the user
                // moves off the Canvas. Otherwise, two selection squares could be drawn at once.
                drawingSurface.CaptureMouse();
            }
        }

        private void AddVisual(Point pointClicked)
        {
            DrawingVisual visual = new DrawingVisual();
            DrawSquare(visual, pointClicked, false);
            drawingSurface.AddVisual(visual);
        }

        private void DeleteVisual(Point pointClicked)
        {
            DrawingVisual visual = drawingSurface.GetVisual(pointClicked);
            if (visual != null) drawingSurface.DeleteVisual(visual);
        }

        // Drawing constants.
        private Brush drawingBrush = Brushes.AliceBlue;
        private Brush selectedDrawingBrush = Brushes.LightGoldenrodYellow;
        private Pen drawingPen = new Pen(Brushes.SteelBlue, 3);
        private Size squareSize = new Size(30, 30);
        private DrawingVisual selectionSquare;

        // Rendering the square.
        private void DrawSquare(DrawingVisual visual, Point topLeftCorner, bool isSelected)
        {
            using (DrawingContext dc = visual.RenderOpen())
            {
                Brush brush = drawingBrush;
                if (isSelected) brush = selectedDrawingBrush;

                dc.DrawRectangle(brush, drawingPen,
                    new Rect(topLeftCorner, squareSize));
            }
        }

        private void drawingSurface_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;

            if (isMultiSelecting)
            {
                // Display all the squares in this region.
                RectangleGeometry geometry = new RectangleGeometry(
                    new Rect(selectionSquareTopLeft, e.GetPosition(drawingSurface)));
                List<DrawingVisual> visualsInRegion =
                    drawingSurface.GetVisuals(geometry);
                MessageBox.Show(String.Format("You selected {0} square(s).", visualsInRegion.Count));

                isMultiSelecting = false;
                drawingSurface.DeleteVisual(selectionSquare);
                drawingSurface.ReleaseMouseCapture();
            }
        }

        private void ClearSelection()
        {
            Point topLeftCorner = new Point(
                        selectedVisual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2,
                        selectedVisual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2);
            DrawSquare(selectedVisual, topLeftCorner, false);
            selectedVisual = null;
        }

        private void drawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point pointDragged = e.GetPosition(drawingSurface) + clickOffset;
                DrawSquare(selectedVisual, pointDragged, true);
                //AddVisual(pointDragged);
            }
            else if (isMultiSelecting)
            {
                Point pointDragged = e.GetPosition(drawingSurface);
                DrawSelectionSquare(selectionSquareTopLeft, pointDragged);
            }
        }

        private Brush selectionSquareBrush = Brushes.Transparent;
        private Pen selectionSquarePen = new Pen(Brushes.Black, 2);

        private void DrawSelectionSquare(Point point1, Point point2)
        {
            selectionSquarePen.DashStyle = DashStyles.Dash;

            using (DrawingContext dc = selectionSquare.RenderOpen())
            {
                dc.DrawRectangle(selectionSquareBrush, selectionSquarePen,
                    new Rect(point1, point2));
            }
        }
    }

    public class DrawingCanvas : Canvas// Panel
    {
        private List<Visual> visuals = new List<Visual>();

        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }
        protected override int VisualChildrenCount
        {
            get
            {
                return visuals.Count;
            }
        }

        public void AddVisual(Visual visual)
        {
            visuals.Add(visual);

            base.AddVisualChild(visual);
            base.AddLogicalChild(visual);
        }

        public void DeleteVisual(Visual visual)
        {
            visuals.Remove(visual);

            base.RemoveVisualChild(visual);
            base.RemoveLogicalChild(visual);
        }

        public DrawingVisual GetVisual(Point point)
        {
            HitTestResult hitResult = VisualTreeHelper.HitTest(this, point);
            return hitResult.VisualHit as DrawingVisual;
        }

        private List<DrawingVisual> hits = new List<DrawingVisual>();
        public List<DrawingVisual> GetVisuals(Geometry region)
        {
            hits.Clear();
            HitTestFilterCallback filterCallback = null;
            HitTestResultCallback resultCallback = new HitTestResultCallback(this.HitTestCallback);
            GeometryHitTestParameters parameters = new GeometryHitTestParameters(region);
            VisualTreeHelper.HitTest(this, filterCallback, resultCallback, parameters);
            return hits;
        }

        private HitTestResultBehavior HitTestCallback(HitTestResult result)
        {
            // Point Hit Test:      HitTestResult --> PointHitTestResult.PointHit
            // Geometry Hit Test:   HitTestResult --> GeometryHitTestReult.IntersectionDetail
            GeometryHitTestResult geometryResult = (GeometryHitTestResult)result;
            DrawingVisual visual = result.VisualHit as DrawingVisual;
            if (visual != null &&
                geometryResult.IntersectionDetail == IntersectionDetail.FullyInside)
            {
                hits.Add(visual);
            }
            return HitTestResultBehavior.Stop;
        }
    }
}
