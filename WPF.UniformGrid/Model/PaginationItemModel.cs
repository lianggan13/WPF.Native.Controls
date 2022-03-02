using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Pagination.Model
{
    public class PaginationItemModel : ObservableObject
    {
        private string index;

        public string Index
        {
            get { return index; }
            set
            {
                index = value;
                OnPropertyChanged();
            }
        }

        private bool isEnabled = true;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool isCurrent = false;

        public bool IsCurrent
        {
            get { return isCurrent; }
            set
            {
                isCurrent = value;
                OnPropertyChanged();
            }
        }
    }
}
