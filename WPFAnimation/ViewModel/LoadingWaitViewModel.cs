using WPFCommon.MVVMFoundation;

namespace WPFAnimation.ViewModel
{
    public class LoadingWaitViewModel : NotifyPropertyChanged
    {
        private bool isLoading;

        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; OnPropertyChanged(); }
        }

    }
}
