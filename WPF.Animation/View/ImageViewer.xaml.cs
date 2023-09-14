using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WPF.Animation.View
{
    /// <summary>
    /// ImageViewer.xaml 的交互逻辑
    /// </summary>
    public partial class ImageViewer : Page
    {

        object _sender;

        public ImageViewer()
        {
            InitializeComponent();

            TransformGroup group = new TransformGroup();
            ScaleTransform scale = new ScaleTransform();
            group.Children.Add(scale);
            RotateTransform rot = new RotateTransform();
            group.Children.Add(rot);
            this.imageCanvas.RenderTransform = group;
        }

        public static DependencyProperty AnimationDurationProperty =
          DependencyProperty.Register("AnimationDuration", typeof(double),
            typeof(ImageViewer), new PropertyMetadata(500.0, null));

        public double AnimationDuration
        {
            get { return (double)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }


        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            _sender = sender;
            if (this.imageCanvas.Background != null)
                FlyOutCurrentImage();
            else
                FlyInNewImage(sender);
        }
        private void FlyInNewImage(object sender)
        {

            if (this.imageCanvas.RenderTransform == null)
                return;

            string imageName = ((Image)sender).Source.ToString();
            imageName = imageName.Substring(imageName.LastIndexOf("/") + 1);
            this.txtImageName.Text = imageName;

            VisualBrush vb = new VisualBrush((Visual)sender);
            this.imageCanvas.Background = vb; // Backgroud -- ViusalBrush

            TransformGroup group = (TransformGroup)this.imageCanvas.RenderTransform;
            ScaleTransform scale = (ScaleTransform)group.Children[0];
            RotateTransform rot = (RotateTransform)group.Children[1];

            // Origin(0,0)
            // Scale(0.5,0.5) --> Scale(1,1)
            // Rotate(45°) --> Rotate(0°)
            this.imageCanvas.RenderTransformOrigin = new Point(0, 0);

            scale.ScaleX = .5;
            scale.ScaleY = .5;
            rot.Angle = 45;

            DoubleAnimation rotAnimation = new DoubleAnimation(0,
              TimeSpan.FromMilliseconds(AnimationDuration));
            rot.BeginAnimation(RotateTransform.AngleProperty, rotAnimation);
            DoubleAnimation scaleAnimation = new DoubleAnimation(1,
              TimeSpan.FromMilliseconds(AnimationDuration));
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        }
        private void FlyOutCurrentImage()
        {

            if (this.imageCanvas.RenderTransform == null)
                return;

            TransformGroup group = (TransformGroup)this.imageCanvas.RenderTransform;
            ScaleTransform scale = (ScaleTransform)group.Children[0];
            RotateTransform rot = (RotateTransform)group.Children[1];

            // Origin(1,0)
            // Scale(1,1) --> Scale(0.5,0.5)
            // Rotate(0°) --> Rotate(45°)
            this.imageCanvas.RenderTransformOrigin = new Point(1, 0);

            DoubleAnimation rotAnimation = new DoubleAnimation(45,
              TimeSpan.FromMilliseconds(AnimationDuration));
            rotAnimation.Completed += Animation_Completed;
            rotAnimation.AccelerationRatio = 0.2;
            rotAnimation.DecelerationRatio = 0.7;
            rot.BeginAnimation(RotateTransform.AngleProperty, rotAnimation);

            DoubleAnimation scaleAnimation = new DoubleAnimation(.5,
              TimeSpan.FromMilliseconds(AnimationDuration));
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        }

        void Animation_Completed(object sender, EventArgs e)
        {
            FlyInNewImage(_sender);
        }
    }
}
