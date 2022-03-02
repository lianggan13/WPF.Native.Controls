using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ListViewImgTurn
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ImageList = new List<string>();
            ImageList.Add("https://img1.baidu.com/it/u=1416457311,1216095499&fm=26&fmt=auto");
            ImageList.Add("https://img2.baidu.com/it/u=3885337919,326828042&fm=26&fmt=auto");
            ImageList.Add("https://img2.baidu.com/it/u=175327901,886211028&fm=26&fmt=auto");
            ImageList.Add("https://img0.baidu.com/it/u=3066308965,3675412486&fm=26&fmt=auto");
            ImageList.Add("https://img2.baidu.com/it/u=771563104,2912096467&fm=26&fmt=auto");
            ImageList.Add("https://img0.baidu.com/it/u=4091531897,1713912937&fm=26&fmt=auto");
            DataContext = this;

            this.Loaded += MainWindow_Loaded;
            var boxs = FindVisualChild<Viewbox>(this, v => true);

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var boxs = FindVisualChild<Viewbox>(this, v => true);

        }

        public List<string> ImageList { get; set; }
        Task task;
        CancellationTokenSource ts = new CancellationTokenSource();
        
        // 设想这样一种场景,按钮点击事件,进入 while 循环,异步执行..., 
        // 如果第二次点击,又进入 while 循环,此如,怎样让 第一个 while 循环 退出呢?
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CancellationToken token = ts.Token;

            if (task?.Status == TaskStatus.Running)
            {
                ts.Cancel();

                // 任务取消后如果想重开任务,不能使用已经取消的token,需要重新声明一个对象.
                ts = new CancellationTokenSource();
                token = ts.Token;
            }
            

            task = new Task(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        return ;
                    }

                    this.Dispatcher.Invoke(() =>
                    {
                        // To do something....
                        if (lvImg.SelectedIndex < ImageList.Count - 1)
                        {
                            lvImg.SelectedIndex++;
                        }
                        else
                        {
                            lvImg.SelectedIndex = 0;
                        }
                    });

                    Thread.Sleep(3000);
                    //await Task.Delay(3000);         // 异步延时 不卡界面
                }
            }, token);

            task.Start();
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            double step = 0.02;
            double scale = st.ScaleX;

            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                if(e.Delta > 0 && scale <10)
                {
                    // 放大
                    scale += step;
                }
                else
                {
                    // 缩小
                    if (scale > 0.2)
                        scale -= step;
                }

                st.ScaleX = st.ScaleY = scale;
                //vb.RenderTransform = new ScaleTransform(scale, scale);
            }

        }

        public static List<T> FindVisualChild<T>(DependencyObject obj, Func<T, bool> where) where T : DependencyObject
        {
            List<T> childList = new List<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is DependencyObject dChild)
                {
                    if (child is T tChild && (where?.Invoke(tChild) == true))
                    {
                        childList.Add(tChild);
                    }
                    childList.AddRange(FindVisualChild<T>(dChild, where));
                }
            }
            return childList;
        }
    }
}
