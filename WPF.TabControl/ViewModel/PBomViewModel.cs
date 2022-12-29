using System.Collections.ObjectModel;
using WPF.TabControl.Model;
using WPFCommon.MVVMFoundation;

namespace WPF.TabControl.ViewModel
{
    public class PBomViewModel
    {
        public ObservableCollection<PBomItemModel> PBOMList { get; set; }

        public PBomViewModel()
        {
            PBOMList = new ObservableCollection<PBomItemModel>();
            // 正常情况从数据获取
            for (int i = 0; i < 5; i++)
            {
                PBOMList.Add(new PBomItemModel
                {
                    Index = i + 1,
                    PNum = "PD190717001",
                    PName = "产品1产品1产品1"
                });
            }

            NewPBOMCommand = new RelayCommand<object>(DoNewCommand);
        }

        public RelayCommand<object> NewPBOMCommand { get; set; }
        private void DoNewCommand(object obj)
        {
            // 这里需要打开编辑窗口，不能这么处理，如何处理？
            // 解耦架构    MvvMLight   Prism   MVVM框架    解耦设计：中间容器   IoC第三方
            //new PBOMEditWin
            // VM-》第三方-》View    new View
            // 原生   WPF框架
            // 实现这个第三方
            // 第三方容器放  View（窗口）   View-》VM      VM-》VM之间的数据交互

            // 第三方   -》   行为


            // 第一步理解 ：从第三方中获取需要执行的逻辑
            //ActionManager.Execute("PBOM");

            // 第二步理解： 功能扩展：希望打开窗口并且带数据 
            // 能打开一个窗口  数据如何过去？ 需要传参
            //PBomItemModel model = new PBomItemModel();
            //model.PName = "产品二";
            //ActionManager.Execute("PBOM", model);

            // 第三步升级：
            // 如果子窗口出现了取消   需要判断Dialog窗口关闭的状态
            PBomItemModel model = new PBomItemModel();
            model.PartList.Clear();// 保证新对象中没有零件子项
            model.PName = "产品二";

        }
    }
}
