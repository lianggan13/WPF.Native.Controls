using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace WPF.Navigate.NavAnimation
{
    /// <summary>
    /// Page0.xaml 的交互逻辑
    /// </summary>
    public partial class Page0 : Page
    {

        public Page0()
        {
            InitializeComponent();

            mainFrame.Navigating += new NavigatingCancelEventHandler(mainFrame_Navigating);
            mainFrame.Navigated += new NavigatedEventHandler(mainFrame_Navigated);
        }


        // Example 16-36. Copying the outgoing page to a VisualBrush

        VisualBrush lastPageBrush;
        void mainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            Page lastPage = mainFrame.Content as Page;
            if (lastPage != null)
            {
                lastPageBrush = new VisualBrush(lastPage);
                lastPageBrush.Viewbox = new Rect(0, 0, lastPage.ActualWidth,
                                                       lastPage.ActualHeight);
                lastPageBrush.ViewboxUnits = BrushMappingMode.Absolute;
                lastPageBrush.Stretch = Stretch.None;


                // Page won't be at origin, thanks to navigation bar.
                // Discover the offset.
                Point pageOffset =
                    lastPage.TransformToVisual(this).Transform(new Point());
                transitionPlaceholder.Margin = new Thickness(pageOffset.X, pageOffset.Y,
                                                             0, 0);
            }
            else
            {
                lastPageBrush = null;
            }
        }

        // End of Example 16-36.


        // Example 16-37. Running the transition animation

        void mainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (lastPageBrush != null)
            {
                transitionPlaceholder.Background = lastPageBrush;
                lastPageBrush = null;

                Storyboard sb = (Storyboard)FindResource("transitionAnimation");
                sb.Begin(this);
            }
        }

        // End of Example 16-37.

    }
}
