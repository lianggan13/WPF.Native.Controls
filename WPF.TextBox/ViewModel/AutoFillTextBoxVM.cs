using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WPFCommon.MVVMFoundation;

namespace WPF.TextBox.ViewModel
{
    public class AutoFillTextBoxVM : NotifyPropertyChanged
    {
        private bool _IsOpen;
        public bool IsOpen
        {
            get { return _IsOpen; }
            set { SetProperty(ref _IsOpen, value); }
        }
        private List<string> List = new List<string>() {
        "1","23","12","13","25","3"
        };
        private ObservableCollection<string> _ListDatas;
        public ObservableCollection<string> ListDatas
        {
            get { return _ListDatas; }
            set { SetProperty(ref _ListDatas, value); }
        }

        private string _Value;
        public string Value
        {
            get { return _Value; }
            set { SetProperty(ref _Value, value); }
        }
        public AutoFillTextBoxVM()
        {

        }
        private RelayCommand _KeyUpCommand;
        public RelayCommand KeyUpCommand =>
            _KeyUpCommand ?? (_KeyUpCommand = new RelayCommand(ExecuteKeyUpCommand));

        void ExecuteKeyUpCommand()
        {
            if (string.IsNullOrEmpty(Value))
            {
                ListDatas = null;
            }
            else
            {
                ListDatas = new ObservableCollection<string>(List.ToList().Where(o => o.Contains(Value)));
            }

        }
        private RelayCommand<object> _SelectionChangedCommand;
        public RelayCommand<object> SelectionChangedCommand =>
            _SelectionChangedCommand ?? (_SelectionChangedCommand = new RelayCommand<object>(ExecuteSelectionChangedCommand));

        void ExecuteSelectionChangedCommand(object value)
        {
            Value = value.ToString();
            ListDatas = null;
        }
    }
}
