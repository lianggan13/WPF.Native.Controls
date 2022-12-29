using System.Windows.Input;
using WPFCommon.MVVMFoundation;

namespace WPF.TabControl.Model
{
    public class PageItemModel : NotifyPropertyChanged
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
        public string Header { get; set; }
        public object PageView { get; set; }

        public ICommand CloseTabCommand { get; set; }
    }
}
