using System.Collections.ObjectModel;
using WPFCommon.MVVMFoundation;

namespace WPF.TabControl.Model
{
    public class PBomItemModel : NotifyPropertyChanged
    {
        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty(ref _isExpanded, value); }
        }

        // 产品基本信息
        public int Index { get; set; }
        public string PName { get; set; }
        public string PNum { get; set; }

        // 相关零件列表 
        public ObservableCollection<string> PartList { get; set; }

        public RelayCommand<PBomItemModel> ExpandedCommand { get; set; }

        public PBomItemModel()
        {
            ExpandedCommand = new RelayCommand<PBomItemModel>(model =>
            {
                model.IsExpanded = !model.IsExpanded;
            }, DoCanE);

            PartList = new ObservableCollection<string>();
            // 监听集合的变化   不管是集合子项的增加、减少
            PartList.CollectionChanged += (se, ev) =>
            {
                // 需要让对应的命令去执行对应 检查
                //ExpandedCommand.RasieCanExecuteChanged();
            };
            for (int i = 0; i < 5; i++)
            {
                PartList.Add("");
            }

        }

        private bool DoCanE(object obj)
        {
            // 只是向Command提供一个条件，但是这个条件什么时候判断？不确定
            return PartList.Count > 0;
        }
    }
}
