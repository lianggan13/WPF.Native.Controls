using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace WPFWindow.View
{
    /// <summary>
    /// DialogReplica.xaml 的交互逻辑
    /// </summary>

    public partial class DialogReplica : UserControl
    {
        private Cursor _cursor;

        public DialogReplica()
        {
            InitializeComponent();
        }

        private void OnResizeThumbDragStarted(object sender, DragStartedEventArgs e)
        {
            _cursor = Cursor;
            Cursor = Cursors.SizeNWSE;
        }

        private void OnResizeThumbDragCompleted(object sender, DragCompletedEventArgs e)
        {
            Cursor = _cursor;
        }

        private void OnResizeThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            double yAdjust = sizableContent.Height + e.VerticalChange;
            double xAdjust = sizableContent.Width + e.HorizontalChange;

            //make sure not to resize to negative width or heigth            
            xAdjust = (sizableContent.ActualWidth + xAdjust) > sizableContent.MinWidth ? xAdjust : sizableContent.MinWidth;
            yAdjust = (sizableContent.ActualHeight + yAdjust) > sizableContent.MinHeight ? yAdjust : sizableContent.MinHeight;

            sizableContent.Width = xAdjust;
            sizableContent.Height = yAdjust;
        }
    }
}
