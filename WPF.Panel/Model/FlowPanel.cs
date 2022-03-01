using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace WPFPanel.Model
{
    /// <summary>
    /// 浮动面板容器
    /// </summary>
    public partial class FlowPanel : Panel
    {
        public FlowPanel()
        {
        }

        // 计算位置
        protected override Size ArrangeOverride(Size finalSize)
        {
            var children = InternalChildren.OfType<UIElement>().ToList();

            int firstInLineIndex = 0;
            double accumulatedY = 0;
            Size curLineSize = new Size(0, 0);

            for (int i = 0, count = children.Count; i < count; i++)
            {
                UIElement child = children[i] as UIElement;
                if (child == null) continue;

                Size sz = child.DesiredSize;

                if (curLineSize.Width + sz.Width > finalSize.Width)
                {
                    ArrageLine(firstInLineIndex, i, accumulatedY, curLineSize.Height);

                    accumulatedY += curLineSize.Height;
                    curLineSize = sz;

                    if (sz.Width > finalSize.Width)
                    {
                        // switch to next line which only contain on element
                        ArrageLine(i, ++i, accumulatedY, sz.Height);

                        accumulatedY += sz.Height;
                        curLineSize = new Size(0, 0); // 到此已经换行了 且 没有元素，所以重置
                    }
                    firstInLineIndex = i;
                }
                else // continue to accumulate a line
                {
                    curLineSize.Width += sz.Width;
                    curLineSize.Height = Math.Max(sz.Height, curLineSize.Height);
                }

                // arrage the last line, if only
                if (firstInLineIndex < children.Count)
                {
                    ArrageLine(firstInLineIndex, children.Count, accumulatedY, curLineSize.Height);
                }
            }

            return finalSize;
        }

        private void ArrageLine(int start, int end, double y, double height)
        {
            double x = 0d;
            var children = base.InternalChildren;
            for (int i = start; i < end; i++)
            {
                UIElement child = children[i] as UIElement;
                if (child != null)
                {
                    child.Arrange(new Rect(
                        x,
                        y,
                        child.DesiredSize.Width,
                        height));
                    x += child.DesiredSize.Width;
                }

            }

        }

        // 测量大小
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement uie in base.InternalChildren)
            {
                uie.Measure(availableSize); // 根据 Measure方法 测量出 Item 的 DesiredSize
            }

            return availableSize;
        }
    }
}
