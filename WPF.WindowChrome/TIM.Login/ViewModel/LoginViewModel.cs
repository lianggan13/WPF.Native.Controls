using WPFCommon.MVVMFoundation;

namespace TIM.Login.ViewModel
{
    public class LoginViewModel : NotifyPropertyChanged
    {

        private string account = "245798563";
        public string Account
        {
            get { return account; }
            set { account = value; OnPropertyChanged(); }
        }


        private string password = "123";

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }



        public LoginViewModel()
        {
            //RelayCommand<object>((obj) =>


        }




    }

}
