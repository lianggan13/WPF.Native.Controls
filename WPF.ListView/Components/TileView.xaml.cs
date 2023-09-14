using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF.ListView.Components
{
    /// <summary>
    /// TileView.xaml 的交互逻辑
    /// </summary>
    public class TileView : ViewBase
    {
        //public TileView()
        //{
        //    InitializeComponent();
        //}

        static TileView()
        {
        }

        private DataTemplate itemTemplate;
        public DataTemplate ItemTemplate
        {
            get { return itemTemplate; }
            set { itemTemplate = value; }
        }

        private Brush selectedBackground = Brushes.Transparent;
        public Brush SelectedBackground
        {
            get { return selectedBackground; }
            set { selectedBackground = value; }
        }

        private Brush selectedBorderBrush = Brushes.Black;
        public Brush SelectedBorderBrush
        {
            get { return selectedBorderBrush; }
            set { selectedBorderBrush = value; }
        }

        protected override object DefaultStyleKey
        {
            get { return new ComponentResourceKey(GetType(), "TileView"); }
        }

        protected override object ItemContainerDefaultStyleKey
        {
            get { return new ComponentResourceKey(GetType(), "TileViewItem"); }
        }
    }
}
