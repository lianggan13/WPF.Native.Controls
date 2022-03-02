using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ListViewAddAnimation.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ListData = new ObservableCollection<DataModel>();
            AddCommand = new RelayCommand<DataModel>(Add);
            DeleteCommand = new RelayCommand<DataModel>(Delete);
        }

        private async void Delete(DataModel obj)
        {
            await Task.Delay(1000);
            ListData.Remove(obj);
        }

        private void Add(DataModel obj)
        {
            ListData.Add(new DataModel() { Uid = Guid.NewGuid() });
        }
        public RelayCommand<DataModel> DeleteCommand { get; set; }
        public RelayCommand<DataModel> AddCommand { get; set; }
        public ObservableCollection<DataModel> ListData { get; set; }
        public class DataModel : ObservableObject
        {
            private Guid _Uid;

            public Guid Uid
            {
                get { return _Uid; }
                set { _Uid = value; RaisePropertyChanged(); }
            }

        }
    }
}