﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WPF.Panel.Components
{
    public class DiagonalPanel : FrameworkElement
    {
        // Private children collection.
        List<UIElement> children = new List<UIElement>();

        // Private field.
        Size sizeChildrenTotal;

        // Dependency Property.
        public static readonly DependencyProperty BackgroundProperty;

        // Static constructor to create Background dependency property.
        static DiagonalPanel()
        {
            BackgroundProperty =
                DependencyProperty.Register(
                    "Background", typeof(Brush), typeof(DiagonalPanel),
                    new FrameworkPropertyMetadata(null,
                            FrameworkPropertyMetadataOptions.AffectsRender));
        }
        // Background property.
        public Brush Background
        {
            set { SetValue(BackgroundProperty, value); }
            get { return (Brush)GetValue(BackgroundProperty); }
        }
        // Methods to access child collection.
        public void Add(UIElement el)
        {
            children.Add(el);
            AddVisualChild(el);
            AddLogicalChild(el);
            InvalidateMeasure();
        }
        public void Remove(UIElement el)
        {
            children.Remove(el);
            RemoveVisualChild(el);
            RemoveLogicalChild(el);
            InvalidateMeasure();
        }
        public int IndexOf(UIElement el)
        {
            return children.IndexOf(el);
        }

        // Overridden properties and methods.
        protected override int VisualChildrenCount
        {
            get { return children.Count; }
        }
        protected override Visual GetVisualChild(int index)
        {
            if (index >= children.Count)
                throw new ArgumentOutOfRangeException("index");

            return children[index];
        }
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            sizeChildrenTotal = new Size(0, 0);

            foreach (UIElement child in children)
            {
                // Call Measure for each child ...
                //child.Measure(sizeAvailable);
                child.Measure(new Size(Double.PositiveInfinity,
                                       Double.PositiveInfinity));


                // ... and then examine DesiredSize property of child.
                sizeChildrenTotal.Width += child.DesiredSize.Width;
                sizeChildrenTotal.Height += child.DesiredSize.Height;
            }
            return sizeChildrenTotal;
        }
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            Point ptChild = new Point(0, 0);

            foreach (UIElement child in children)
            {
                Size sizeChild = new Size(0, 0);

                sizeChild.Width = child.DesiredSize.Width *
                                    (sizeFinal.Width / sizeChildrenTotal.Width);
                sizeChild.Height = child.DesiredSize.Height *
                                    (sizeFinal.Height / sizeChildrenTotal.Height);

                child.Arrange(new Rect(ptChild, sizeChild));

                ptChild.X += sizeChild.Width;
                ptChild.Y += sizeChild.Height;
            }
            return sizeFinal;
        }
        protected override void OnRender(DrawingContext dc)
        {
            dc.DrawRectangle(Background, null,
                             new Rect(new Point(0, 0), RenderSize));
        }
    }
}
