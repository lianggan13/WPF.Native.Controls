﻿using System;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WPF.Brush.Brushes
{
    using System.Windows.Media;

    /// <summary>
    /// 光栅状态
    /// </summary>
    public enum GratingState
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown,

        /// <summary>
        /// 布防
        /// </summary>
        Defense,

        /// <summary>
        /// 布防报警
        /// </summary>
        DefenseWarning,

        /// <summary>
        /// 设备故障
        /// </summary>
        Error,
    }

    /// <summary>
    /// GrateLine.xaml 的交互逻辑
    /// </summary>
    [TemplatePart(Name = PART_Rect, Type = typeof(Rectangle))]
    public partial class GrateLine : Control
    {
        const string PART_Rect = nameof(PART_Rect);
        Rectangle? rect;

        public GrateLine()
        {
            InitializeComponent();
        }

        public Point StartPoint
        {
            get { return (Point)GetValue(StartPointProperty); }
            set { SetValue(StartPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartPointProperty =
            DependencyProperty.Register(nameof(StartPoint), typeof(Point), typeof(GrateLine), new PropertyMetadata(new Point(0.5, 0)));

        public Point EndPoint
        {
            get { return (Point)GetValue(EndPointProperty); }
            set { SetValue(EndPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndPointProperty =
            DependencyProperty.Register(nameof(EndPoint), typeof(Point), typeof(GrateLine), new PropertyMetadata(new Point(0.5, 1)));

        public bool Defense
        {
            get { return (bool)GetValue(DefenseProperty); }
            set { SetValue(DefenseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Defence.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefenseProperty =
            DependencyProperty.Register(nameof(Defense), typeof(bool), typeof(GrateLine), new PropertyMetadata(false, (d, e) => { (d as GrateLine).DefenseOrStateChanged(); }));

        public GratingState State
        {
            get { return (GratingState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register(nameof(State), typeof(GratingState), typeof(GrateLine), new PropertyMetadata(GratingState.Unknown, (d, e) => { (d as GrateLine).DefenseOrStateChanged(); }));

        Storyboard storyboard = new Storyboard();
        private void DefenseOrStateChanged()
        {
            if (rect == null)
                return;

            if (Defense && (State == GratingState.Defense || State == GratingState.DefenseWarning))
            {
                // Defense
                rect.Fill = new LinearGradientBrush(((LinearGradientBrush)FindResource("LinearGradientBrush.Defense")).GradientStops, StartPoint, EndPoint);
            }
            else if (State != GratingState.Unknown)
            {
                // NoDefense
                rect.Fill = new LinearGradientBrush(((LinearGradientBrush)FindResource("LinearGradientBrush.NoDefense")).GradientStops, StartPoint, EndPoint);
            }
            else
            {
                // Unknown
                rect.Fill = Brushes.Transparent;
            }


            // Clear Animation
            if (storyboard.Children.Count > 0)
            {
                storyboard.Stop(rect);
                storyboard.Remove(rect);

                storyboard.Children.Clear();
            }
            // Add Animation
            if (Defense && State == GratingState.DefenseWarning)
            {
                PointAnimation pointAnimation = new PointAnimation();
                pointAnimation.AutoReverse = true;
                pointAnimation.FillBehavior = FillBehavior.Stop;
                pointAnimation.RepeatBehavior = RepeatBehavior.Forever;
                pointAnimation.To = EndPoint;
                pointAnimation.BeginTime = TimeSpan.FromMilliseconds(30);
                pointAnimation.Duration = new Duration(TimeSpan.FromSeconds(2));

                Storyboard.SetTargetName(pointAnimation, rect.Name);
                Storyboard.SetTargetProperty(pointAnimation, new PropertyPath("(Rectangle.Fill).(LinearGradientBrush.StartPoint)"));

                storyboard.Children.Add(pointAnimation);

                storyboard.Begin(rect, isControllable: true);
            }
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            rect = (Rectangle)Template.FindName(PART_Rect, this);

            DefenseOrStateChanged();
        }
    }
}
