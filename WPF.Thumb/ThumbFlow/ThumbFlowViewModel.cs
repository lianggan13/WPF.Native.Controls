using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF.Thumb.ThumbFlow
{
    public class ThumbFlowViewModel
    {
        public List<ToolModel> ToolList { get; set; } =
            new List<ToolModel>();

        public bool IsSwitchLink { get; set; }

        public ThumbFlowViewModel()
        {
            #region 工具箱初始化
            ToolList.Add(new ToolModel()
            {
                Header = "图像处理",
                Icon = "../Assets/Images/catalog.png",
                Children = new List<ToolModel>()
                {
                    new ToolModel()
                    {
                        Header = "图像采集", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "FlowNodeA"
                    },
                    new ToolModel()
                    {
                        Header = "图像保存", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "FlowNodeB",
                    },
                    new ToolModel()
                    {
                        Header = "图像显示", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "FlowNodeC",
                    }
                }
            });

            ToolList.Add(new ToolModel()
            {
                Header = "检测识别",
                Icon = "../Assets/Images/catalog.png",
                Children = new List<ToolModel>()
                {
                    new ToolModel(){
                        Header = "一维条形码识别",  Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "二维码识别", Icon = "../Assets/Images/m_act.png",
                         TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "颜色识别", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "创建区域", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "亮度检查", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "模板匹配", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "字符识别", Icon = "../Assets/Images/m_act.png",
                       TargetObject = "",
                    }
                }
            });

            ToolList.Add(new ToolModel()
            {
                Header = "几何测量",
                Icon = "../Assets/Images/catalog.png",
                Children = new List<ToolModel>()
                {
                    new ToolModel()
                    {
                        Header = "线线距离", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "圆心测量", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "椭圆测量", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "直线测量", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "间隙测量", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    }
                }
            });

            ToolList.Add(new ToolModel()
            {
                Header = "几何构建",
                Icon = "../Assets/Images/catalog.png",
                Children = new List<ToolModel>()
                {
                    new ToolModel()
                    {
                        Header = "线圆构建", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "线线交点", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "点线构建", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "点点构建", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "圆形拟合", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "直线拟合", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    }
                }
            });

            ToolList.Add(new ToolModel()
            {
                Header = "标定工具",
                Icon = "../Assets/Images/catalog.png",
                Children = new List<ToolModel>()
                {
                    new ToolModel()
                    {
                        Header = "畸形标定", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "测量标定", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    }
                }
            });

            ToolList.Add(new ToolModel()
            {
                Header = "定位工具",
                Icon = "../Assets/Images/catalog.png",
                Children = new List<ToolModel>()
                {
                    new ToolModel()
                    {
                        Header = "仿射变换", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "九点标定", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "手臂控制", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    }
                }
            });

            ToolList.Add(new ToolModel()
            {
                Header = "逻辑工具",
                Icon = "../Assets/Images/catalog.png",
                Children = new List<ToolModel>()
                {
                    new ToolModel()
                    {
                        Header = "循环执行", Icon = "../Assets/Images/m_act.png",
                         TargetObject = "ProcessLogicLoop",
                    },
                    new ToolModel()
                    {
                        Header = "如果判断", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "如果否则", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "跳转标签", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "跳转执行", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    }
                }
            });

            ToolList.Add(new ToolModel()
            {
                Header = "变量工具",
                Icon = "../Assets/Images/catalog.png",
                Children = new List<ToolModel>()
                {
                    new ToolModel()
                    {
                        Header = "创建文本", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "文本分割", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "数据入队", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "数据出队", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "队列清空", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "变量定义", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "变量设置", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    }
                }
            });

            ToolList.Add(new ToolModel()
            {
                Header = "系统工具",
                Icon = "../Assets/Images/catalog.png",
                Children = new List<ToolModel>()
                {
                    new ToolModel()
                    {
                        Header = "数据计算", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "数据保存", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "数据显示", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "延时工具", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "文件夹", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    }
                }
            });

            ToolList.Add(new ToolModel()
            {
                Header = "通信工具",
                Icon = "../Assets/Images/catalog.png",
                Children = new List<ToolModel>()
                {
                    new ToolModel()
                    {
                        Header = "接收命令", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "接收文本", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    },
                    new ToolModel()
                    {
                        Header = "发送数据", Icon = "../Assets/Images/m_act.png",
                        TargetObject = "",
                    }
                }
            });
            #endregion
        }


        public void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var data = (sender as FrameworkElement).DataContext;
            if ((data as ToolModel).Children.Count > 0) return;
            // 开始拖拽
            DragDrop.DoDragDrop((DependencyObject)sender, data, DragDropEffects.Copy);
        }

        public void Canvas_Drop(object sender, DragEventArgs e)
        {
            var model = e.Data.GetData(typeof(ToolModel)) as ToolModel;

            // 需要知道是创建哪个节点
            // 拿到一个对象的名称字符串，需要创建出这个对象的实例 ：  反射

            Type type = Assembly.GetExecutingAssembly()
                .GetType($"WPF.Thumb.ThumbFlow.{model.TargetObject}");

            var obj = (FlowNodeBase)Activator.CreateInstance(type);

            obj.DragDelta += Obj_DragDelta; // Thumb Draging
            obj.PreviewMouseLeftButtonDown += Obj_PreviewMouseLeftButtonDown;
            obj.PreviewMouseLeftButtonUp += Obj_PreviewMouseLeftButtonUp;
            (sender as Canvas).Children.Add(obj);

            Point current_point = e.GetPosition((IInputElement)sender);
            Canvas.SetLeft(obj, current_point.X - 75);
            Canvas.SetTop(obj, current_point.Y - 17);
        }

        private FlowNodeBase startObj;
        private FlowNodeBase endObj;
        private void Obj_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsSwitchLink)
            {
                startObj = (FlowNodeBase)sender;
                e.Handled = true;
            }
        }

        private void Obj_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsSwitchLink)
            {
                endObj = (FlowNodeBase)sender;
                if (startObj != endObj)
                {
                    double sx = Canvas.GetLeft(startObj);
                    double sy = Canvas.GetTop(startObj);
                    double ex = Canvas.GetLeft(endObj);
                    double ey = Canvas.GetTop(endObj);
                    // 创建一条线
                    // Path Data="Mx y Cx y   x y   x y"
                    Path path = new Path();
                    string dataStr = $"M{sx + 75} {sy + 34}" +
                        $"C{sx + 75} {(ey - (sy + 34)) / 2 + sy + 34} " +
                        $"{ex + 75} {(ey - (sy + 34)) / 2 + sy + 34} " +
                        $"{ex + 75} {ey}";
                    path.Data = PathGeometry.Parse(dataStr);
                    //Line line = new Line();
                    //line.X1 = sx + 75;
                    //line.Y1 = sy + 34;
                    //line.X2 = ex + 75;
                    //line.Y2 = ey;
                    path.Stroke = Brushes.Black;
                    path.StrokeThickness = 1;

                    // 获取节点对象的Canvas画布
                    GetCanvasParent(endObj).Children.Add(path);

                    startObj.NextNode = endObj;
                    endObj.PreviousNode = startObj;

                    startObj.OutputPath = path;
                    endObj.InputPath = path;
                }
            }
        }

        private void Obj_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double x = Canvas.GetLeft((UIElement)sender);
            double y = Canvas.GetTop((UIElement)sender);

            y += e.VerticalChange;
            x += e.HorizontalChange;

            Canvas.SetLeft((UIElement)sender, x);
            Canvas.SetTop((UIElement)sender, y);

            var node = (sender as FlowNodeBase);
            double cx = Canvas.GetLeft(node);
            double cy = Canvas.GetTop(node);
            if (node.PreviousNode != null)
            {
                double ix = Canvas.GetLeft(node.PreviousNode);
                double iy = Canvas.GetTop(node.PreviousNode);
                if (node.InputPath != null)
                {
                    string dataStr = $"M{ix + 75} {iy + 34}" +
                        $"C{ix + 75} {(cy - (iy + 34)) / 2 + iy + 34} " +
                        $"{cx + 75} {(cy - (iy + 34)) / 2 + iy + 34} " +
                        $"{cx + 75} {cy}";
                    node.InputPath.Data = PathGeometry.Parse(dataStr);
                }
            }

            if (node.NextNode != null)
            {
                double ox = Canvas.GetLeft(node.NextNode);
                double oy = Canvas.GetTop(node.NextNode);
                if (node.OutputPath != null)
                {
                    string dataStr = $"M{cx + 75} {cy + 34}" +
                        $"C{cx + 75} {(oy - (cy + 34)) / 2 + cy + 34} " +
                        $"{ox + 75} {(oy - (cy + 34)) / 2 + cy + 34} " +
                        $"{ox + 75} {oy}";
                    node.OutputPath.Data = PathGeometry.Parse(dataStr);
                }
            }
            //node.InputPath
        }

        private Canvas GetCanvasParent(UIElement element)
        {
            var obj = VisualTreeHelper.GetParent(element);
            if (obj is Canvas canvas)
            {
                return canvas;
            }
            return GetCanvasParent((UIElement)obj);
        }
    }
}
