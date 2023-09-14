using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF.Animation.View
{
    /// <summary>
    /// RenderCache.xaml 的交互逻辑
    /// </summary>
    public partial class RenderCache : UserControl
    {
        public RenderCache()
        {
            InitializeComponent();
        }

        private void chkCache_Click(object sender, RoutedEventArgs e)
        {
            if (chkCache.IsChecked == true)
            {
                BitmapCache bitmapCache = new BitmapCache();
                pathBackground.CacheMode = new BitmapCache();
            }
            else
            {
                pathBackground.CacheMode = null;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();

            pathFigure.StartPoint = new Point(0, 0);

            PathSegmentCollection pathSegmentCollection = new PathSegmentCollection();

            int maxHeight = (int)this.ActualHeight;
            int maxWidth = (int)this.ActualWidth;
            Random rand = new Random();
            for (int i = 0; i < 500; i++)
            {
                LineSegment newSegment = new LineSegment();
                newSegment.Point = new Point(rand.Next(0, maxWidth), rand.Next(0, maxHeight));
                pathSegmentCollection.Add(newSegment);
            }

            pathFigure.Segments = pathSegmentCollection;
            pathGeometry.Figures.Add(pathFigure);

            pathBackground.Data = pathGeometry;
        }

        private void chkCache2_Click(object sender, RoutedEventArgs e)
        {
            if (chkCache.IsChecked == true)
            {
                BitmapCache bitmapCache = new BitmapCache();
                bitmapCache.RenderAtScale = 5;
                cmd.CacheMode = bitmapCache;
                img.CacheMode = new BitmapCache();
            }
            else
            {
                cmd.CacheMode = null;
                img.CacheMode = null;
            }
        }
    }
}
