using System.Windows;
using System.Windows.Controls;

namespace WPF.Thumb.ThumbFlow
{
    /// <summary>
    /// Interaction logic for ThumbFlow.xaml
    /// </summary>
    public partial class ThumbFlow : UserControl
    {
        public ThumbFlow()
        {
            InitializeComponent();

            this.DataContext = new ThumbFlowViewModel();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.canvas.Children.Count > 0 &&
                this.canvas.Children[0] is FlowNodeBase flowNodeBase)
            {
                var node = GetParentNode(flowNodeBase);
                if (node != null)
                {
                    node.Process();
                }
            }
        }

        private FlowNodeBase GetParentNode(FlowNodeBase flowNodeBase)
        {
            if (flowNodeBase.PreviousNode == null)
                return flowNodeBase;
            return GetParentNode(flowNodeBase.PreviousNode);
        }
    }
}