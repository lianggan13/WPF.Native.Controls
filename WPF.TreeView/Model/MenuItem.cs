using System.Collections.ObjectModel;
using WPFCommon.MVVMFoundation;

namespace WPF.TreeView.Model
{
    public class MenuItemModel : NotifyPropertyChanged
    {
        public string IconCode { get; set; }

        public string Header { get; set; }

        public string TargetView { get; set; }

        public ObservableCollection<MenuItemModel> Children { get; set; } = new ObservableCollection<MenuItemModel>();


        private bool isDragOver = false;
        public bool IsDragOver
        {
            get
            {
                return isDragOver;
            }
            set
            {
                SetProperty(ref isDragOver, value);
            }
        }
    }
}
