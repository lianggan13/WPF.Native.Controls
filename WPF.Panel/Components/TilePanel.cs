namespace WPF.Panel.Components
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// 瀑布流面板容器
    /// </summary>
    public partial class TilePanel : Panel
    {
        public TilePanel()
        {
        }

        // 计算位置
        protected override Size ArrangeOverride(Size finalSize)
        {
            var children = InternalChildren.OfType<UIElement>().ToList();

            // 每 3个 元素为一组，每一组作为一行 进行布置
            int colNum = 3;
            double accumlateX = 0;
            double accumlateY = 0;

            for (int i = 0, count = children.Count; i < count; i++)
            {
                UIElement child = children[i] as UIElement;
                if (child == null) continue;

                Size sz = child.DesiredSize;
                accumlateY = 0;

                if (i % colNum == 0)
                {
                    // 换行
                    accumlateX = 0;
                }

                // 查找所有前排的元素, 累加元素高度
                int preIndex = i;
                while (preIndex >= colNum)
                {
                    preIndex -= colNum;
                    accumlateY += children[preIndex].DesiredSize.Height;
                }

                // 放置
                ArrageChild(i, accumlateX, accumlateY);
                accumlateX += sz.Width;
            }

            return finalSize;
        }

        private void ArrageChild(int index, double x, double y)
        {
            UIElement child = InternalChildren[index] as UIElement;
            if (child != null)
            {
                child.Arrange(new Rect(
                    x,
                    y,
                    child.DesiredSize.Width,
                    child.DesiredSize.Height));
            }

        }


        // 测量大小
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement uie in InternalChildren)
            {
                uie.Measure(availableSize); // 根据 Measure方法 测量出 Item 的 DesiredSize
            }

            return availableSize;
        }
    }
}
