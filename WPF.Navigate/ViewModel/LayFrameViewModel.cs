using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

namespace Frame界面导航过度.ViewModel
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
    public class LayFrameViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public LayFrameViewModel()
        {
            ContentPage = new Uri(@"/View/Page1.xaml", UriKind.RelativeOrAbsolute);
        }
        private Uri _ContentPage;

        public Uri ContentPage
        {
            get { return _ContentPage; }
            set { _ContentPage = value; RaisePropertyChanged(); }
        }
        public RelayCommand<string> GoCommand => new RelayCommand<string>((data) =>
        {
            ContentPage = new Uri(@""+ data + "", UriKind.RelativeOrAbsolute);
        });
    }
}