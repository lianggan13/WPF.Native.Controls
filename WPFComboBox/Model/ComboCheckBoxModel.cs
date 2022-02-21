using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WPFCommon.MVVMFoundation;

namespace GTS.MaxSign.Controls.TemplateParam.Model
{
    public class ComboCheckBoxModel : NotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        private string option;
        public string Option
        {
            get => option;
            set
            {
                if (!value.Contains(".Model."))
                {
                    option = value;
                    OnPropertyChanged();
                }
            }
        }


        public void DropDownClosed()
        {
            var s = string.Join(",", Collection.Where(f => f.IsChecked).Select(f => f.Content));
            if (Option != s)
                Option = s;
        }

        public ObservableCollection<ComboCheckBoxItemModel> Collection { get; set; } = new ObservableCollection<ComboCheckBoxItemModel>();

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }


        public Func<string, Tuple<string, string>> Validator { get; set; }
        public string Error => string.Empty;

        public void Clear()
        {
            Option = null;
            Collection.Clear();
        }
    }

    public class ComboCheckBoxItemModel : NotifyPropertyChanged
    {
        private string content;
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                this.content = value;
                OnPropertyChanged();
            }
        }

        private bool isChecked;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
