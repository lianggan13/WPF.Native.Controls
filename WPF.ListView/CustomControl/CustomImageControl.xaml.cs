using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF.ListView.CustomControl
{
    /// <summary>
    /// CustomImageControl.xaml 的交互逻辑
    /// </summary>
    [TemplatePart(Name = PART_Image, Type = typeof(CustomImageControl))]
    public partial class CustomImageControl : Control
    {
        private const string PART_Image = "PART_Image";

        private Image image;

        static CustomImageControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomImageControl),
                new FrameworkPropertyMetadata(typeof(CustomImageControl)));
        }

        public CustomImageControl()
        {
            InitializeComponent();
        }

        public int Channel
        {
            get { return (int)GetValue(ChannelProperty); }
            set { SetValue(ChannelProperty, value); }
        }

        public bool? IsSafe
        {
            get { return (bool?)GetValue(IsSafeProperty); }
            set { SetValue(IsSafeProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public ImageSource PlaceHolder
        {
            get { return (ImageSource)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        public bool Play
        {
            get { return (bool)GetValue(PlayProperty); }
            set { SetValue(PlayProperty, value); }
        }

        public int LinkCode
        {
            get { return (int)GetValue(LinkCodeProperty); }
            set { SetValue(LinkCodeProperty, value); }
        }

        public static readonly DependencyProperty ChannelProperty =
            DependencyProperty.Register("Channel", typeof(int), typeof(CustomImageControl), new PropertyMetadata(0, OnChannelChanged));

        public static readonly DependencyProperty IsSafeProperty =
            DependencyProperty.Register("IsSafe", typeof(bool?), typeof(CustomImageControl), new PropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(CustomImageControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty PlaceHolderProperty =
            DependencyProperty.Register("PlaceHolder", typeof(ImageSource), typeof(CustomImageControl), new PropertyMetadata(default));

        public static readonly DependencyProperty PlayProperty =
            DependencyProperty.Register("Play", typeof(bool), typeof(CustomImageControl), new PropertyMetadata(false, OnPlayChanged));

        public static readonly DependencyProperty LinkCodeProperty =
              DependencyProperty.Register(nameof(LinkCode), typeof(int), typeof(CustomImageControl), new PropertyMetadata(0, OnLinkCodeChanged));

        private static void OnPlayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomImageControl video = d as CustomImageControl;
            if ((bool)e.NewValue)
            {
                video.Start();
            }
            else
            {
                video.Stop();
            }
        }

        private static void OnChannelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int channel = (int)e.NewValue;
            if (channel != 0)
            {
                CustomImageControl video = d as CustomImageControl;
                video.ResetChannel(channel);
            }
        }

        private static void OnLinkCodeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int code = (int)e.NewValue;
            (d as CustomImageControl).LinkAction(code);
        }

        private void LinkAction(int code)
        {

        }


        private void Start()
        {
            //try
            //{
            //    HIKUtil.Preview(Channel, image);
            //}
            //catch (Exception ex)
            //{
            //    MessageWindow.Show($"{ex.Message}");
            //}
        }

        private void Stop()
        {
            //image.Source = PlaceHolder;
            //HIKUtil.StopPreview(image);
        }

        private void ResetChannel(int channel)
        {
            if (channel != 0)
            {
                if (Play)
                {
                    Start();
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            image = Template.FindName(PART_Image, this) as Image;
            image.MouseLeftButtonDown -= ImageMouseLeftButtonDown;
            image.MouseLeftButtonDown += ImageMouseLeftButtonDown;
            ResetChannel(Channel);
        }

        private void ImageMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //ViewVideoWindow window = new ViewVideoWindow(image.Source)
            //{
            //    Owner = VisualTreeUtil.GetParent<Window>(this)
            //};
            //void ImageSourceUpdated(object s, EventArgs args)
            //{
            //    Image image = s as Image;
            //    window.Source = image.Source;
            //};
            //image.SourceUpdated += ImageSourceUpdated;
            //window.Closed += (s, args) =>
            //{
            //    image.SourceUpdated -= ImageSourceUpdated;
            //};
            ////window.ShowDialog();
            //window.Show();
        }
    }
}
