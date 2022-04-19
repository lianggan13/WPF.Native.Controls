using System.Collections.ObjectModel;
using WPFCommon.MVVMFoundation;

namespace WPF.ListView.ViewModel
{
    public class ListImageViewModel : NotifyPropertyChanged//: ViewModelBase
    {
        public ObservableCollection<string> ImageList { get; set; } = new ObservableCollection<string>();

        public RelayCommand BackCommand => new RelayCommand(BackImage, CanBackImage);



        public RelayCommand ForeCommand => new RelayCommand(ForeImage, CanForeImage);


        private int selectedIndex = default;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }


        public ListImageViewModel()
        {
            ImageList.Add("/Assets/Images/1.jpg");
            ImageList.Add("/Assets/Images/2.jpg");
            ImageList.Add("/Assets/Images/3.jpg");
            ImageList.Add("/Assets/Images/4.jpg");
            ImageList.Add("/Assets/Images/5.jpg");
            ImageList.Add("/Assets/Images/6.jpg");

        }

        private void BackImage()
        {
            SelectedIndex--;
        }

        private bool CanBackImage(object _)
        {
            return SelectedIndex > 0;
        }

        private void ForeImage()
        {
            SelectedIndex++;
        }

        private bool CanForeImage(object _)
        {
            return SelectedIndex < ImageList.Count - 1;
        }

    }
}
