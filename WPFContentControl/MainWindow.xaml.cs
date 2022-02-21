using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCommon.MVVMFoundation;
using WPFContentControl.Common;
using WPFContentControl.View;

namespace WPFContentControl
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class MainVM : NotifyPropertyChanged
    {
        private ICommand navigateCmd;
        public ICommand NavigateCmd
        {
            get => navigateCmd;
        }

        private FrameworkElement mainContent;
        public FrameworkElement MainContent
        {
            get { return mainContent; }
            set { mainContent = value; OnPropertyChanged(); }
        }

        public MainVM()
        {
            navigateCmd = new RelayCommand<object>((para) =>
            {
                Type type = GetContentType(para);
                ConstructorInfo cti = type.GetConstructor(Type.EmptyTypes);
                this.MainContent = (FrameworkElement)cti.Invoke(null);

            }, (para) => true);

            NavigateCmd.Execute(typeof(HomePage));
        }

        private Type GetContentType(object para)
        {
            Type type = null;
            if (para is Type)
            {
                type = para as Type;
            }
            else if (para is RadioButton rabtn)
            {
                // 获取 依赖对象 的 附加属性
                // var ss  = rabtn.GetValue(ContentTypeDp.ContentTypeProperty);
                type = ContentTypeDpObj.GetContentType(rabtn);
            }
            return type;
        }
    }
}
