using WPF.ComboBox.View;
using WPFCommon.MVVMFoundation;

namespace WPF.ComboBox.Model
{
    public class ComboCheckBoxItem : NotifyPropertyChanged, IComboCheckBoxItem
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public object Content => Name;

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { SetProperty(ref isChecked, value); }
        }
    }
}
