using System.Collections.ObjectModel;
using WPFCommon.MVVMFoundation;

namespace WPF.Expander.Model
{
    public class MenuItemModel : NotifyPropertyChanged
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(nameof(Title)); }
        }

        public ObservableCollection<MenuItemModel> SubItems { get; set; }


    }
}
