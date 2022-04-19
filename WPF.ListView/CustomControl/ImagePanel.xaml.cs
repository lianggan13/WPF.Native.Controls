using System;
using System.Windows;
using System.Windows.Controls;

namespace WPF.ListView.CustomControl
{
    /// <summary>
    /// ImagePanel.xaml 的交互逻辑
    /// </summary>
    public partial class ImagePanel : Panel
    {
        public ImagePanel()
        {
            InitializeComponent();
        }

        public readonly int LineInnerCount = 4;

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(ImagePanel), new FrameworkPropertyMetadata(0,
        FrameworkPropertyMetadataOptions.None, propertyChangedCallback));
        //FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, propertyChangedCallback));

        private static void propertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ImagePanel).Refresh(e);
        }

        private void Refresh(DependencyPropertyChangedEventArgs e)
        {
            int oldIndex = (int)e.OldValue;
            int newIndex = (int)e.NewValue;

            int oldGroup = Math.DivRem(oldIndex, LineInnerCount, out int oldRem);
            int newGroup = Math.DivRem(newIndex, LineInnerCount, out int newRem);

            if (oldGroup != newGroup) // 跨界了
            {
                InvalidateArrange();
            }
        }

        /// <summary>
        /// RenderSize & Arrange
        /// </summary>
        /// <param name="arrangeSize"></param>
        /// <returns></returns>

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            double arrageWidth = arrangeSize.Width / LineInnerCount;
            double arrageHeight = arrangeSize.Height;

            int group = Math.DivRem(SelectedIndex, LineInnerCount, out int innerIndex);
            int befNum = group * LineInnerCount;
            #region Skip & Take
            //var children = InternalChildren.OfType<UIElement>().ToList();
            //var befChild = children.Take(befNum);
            //var showChild = children.Skip(befNum).Take(LineInnerCount);
            //var afterChild = children.Skip(befNum + LineInnerCount); 
            #endregion

            UIElementCollection children = InternalChildren;

            for (int i = befNum - 1, j = 1; i >= 0; i--, j++)
            {
                double x = -arrageWidth * j;
                children[i].Arrange(new Rect(x, 0, arrageWidth, arrageHeight));
                (children[i] as FrameworkElement).Width = arrageWidth; // need set width again
            }

            //int afterNum = InternalChildren.Count - befNum;
            for (int i = befNum, j = 0; i < children.Count; i++, j++)
            {
                //(children[i] as FrameworkElement).Margin = i != children.Count - 1
                //    ? new Thickness(0, 0, 5, 0)
                //    : new Thickness(0);

                double x = arrageWidth * j;
                children[i].Arrange(new Rect(x, 0, arrageWidth, arrageHeight));
                (children[i] as FrameworkElement).Width = arrageWidth; // need set width again
            }

            return arrangeSize;

            #region old way
            //if (SelectedIndex < LineInnerCount)
            //{
            //    for (int i = 0; i < InternalChildren.Count; i++)
            //    {
            //        //(Children[i] as FrameworkElement).Margin = i != Children.Count - 1
            //        //    ? new Thickness(0, 0, 5, 0)
            //        //    : new Thickness(0);
            //        UIElement child = Children[i] as UIElement;
            //        //var rs = child.RenderSize;
            //        //var ds = child.DesiredSize;

            //        child.Arrange(new Rect(currentX, 0, arrageWidth, arrageHeight));
            //        //(Children[i] as FrameworkElement).Width = elementWidth;

            //        currentX += arrageWidth;
            //    }
            //}
            //else
            //{
            //    for (int i = SelectedIndex - 1, j = 1; i >= 0; i--, j++)
            //    {
            //        double x = -arrageWidth * j;
            //        Children[i].Arrange(new Rect(x, 0, arrageWidth, arrageHeight));
            //    }

            //    for (int i = SelectedIndex, j = 0; i < Children.Count; i++, j++)
            //    {
            //        double x = arrageWidth * j;
            //        Children[i].Arrange(new Rect(x, 0, arrageWidth, arrageHeight));
            //    }
            //}

            //return arrangeSize; 
            #endregion
        }

        /// <summary>
        /// DesiredSize & Measure
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                var ds1 = child.DesiredSize;
                var rs1 = child.RenderSize;
                child.Measure(availableSize); // 根据 Measure方法 测量出 Item 的 DesiredSize
                var ds2 = child.DesiredSize;
                var rs2 = child.RenderSize;

            }
            return availableSize;
        }
    }
}
