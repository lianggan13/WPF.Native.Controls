using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WPF.Virtualization.DataVirtualization.Model;
using WPFCommon.MVVMFoundation;

namespace WPF.Virtualization.DataVirtualization.ViewModel
{
    public class PagerViewModel : NotifyPropertyChanged
    {
        private ICommand _firstPageCommand;

        public ICommand FirstPageCommand
        {
            get
            {
                return _firstPageCommand;
            }

            set
            {
                _firstPageCommand = value;
            }
        }

        private ICommand _previousPageCommand;

        public ICommand PreviousPageCommand
        {
            get
            {
                return _previousPageCommand;
            }

            set
            {
                _previousPageCommand = value;
            }
        }

        private ICommand _nextPageCommand;

        public ICommand NextPageCommand
        {
            get
            {
                return _nextPageCommand;
            }

            set
            {
                _nextPageCommand = value;
            }
        }

        private ICommand _lastPageCommand;

        public ICommand LastPageCommand
        {
            get
            {
                return _lastPageCommand;
            }

            set
            {
                _lastPageCommand = value;
            }
        }

        private int _pageSize;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (_pageSize != value)
                {
                    _pageSize = value;
                    OnPropertyChanged("PageSize");
                }
            }
        }

        private int _currentPage;

        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }

            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged("CurrentPage");
                }
            }
        }

        private int _totalPage;

        public int TotalPage
        {
            get
            {
                return _totalPage;
            }

            set
            {
                if (_totalPage != value)
                {
                    _totalPage = value;
                    OnPropertyChanged("TotalPage");
                }
            }
        }

        private ObservableCollection<Customer> customers;

        public ObservableCollection<Customer> Customers
        {
            get
            {
                return customers;
            }
            set
            {
                if (customers != value)
                {
                    customers = value;
                    OnPropertyChanged(nameof(Customers));
                }
            }
        }

        private List<Customer> _source;

        public PagerViewModel()
        {
            _currentPage = 1;

            _pageSize = 20;

            Customer fake = new Customer();

            _source = fake.GenerateCustomers();

            _totalPage = _source.Count / _pageSize;

            customers = new ObservableCollection<Customer>(_source.Take(_pageSize));

            _firstPageCommand = new RelayCommand(FirstPageAction);

            _previousPageCommand = new RelayCommand(PreviousPageAction);

            _nextPageCommand = new RelayCommand(NextPageAction);

            _lastPageCommand = new RelayCommand(LastPageAction);
        }

        private void FirstPageAction()
        {
            CurrentPage = 1;

            var result = _source.Take(_pageSize).ToList();

            customers.Clear();

            //_fakeSoruce.AddRange(result);

            Customers = new ObservableCollection<Customer>(result);
        }

        private void PreviousPageAction()
        {
            if (CurrentPage == 1)
            {
                return;
            }

            List<Customer> result = new List<Customer>();

            if (CurrentPage == 2)
            {
                result = _source.Take(_pageSize).ToList();
            }
            else
            {
                result = _source.Skip((CurrentPage - 2) * _pageSize).Take(_pageSize).ToList();
            }

            //_fakeSoruce.AddRange(result);

            Customers = new ObservableCollection<Customer>(result);

            CurrentPage--;
        }

        private void NextPageAction()
        {
            if (CurrentPage == _totalPage)
            {
                return;
            }

            List<Customer> result = new List<Customer>();

            result = _source.Skip(CurrentPage * _pageSize).Take(_pageSize).ToList();

            Customers = new ObservableCollection<Customer>(result);

            CurrentPage++;
        }

        private void LastPageAction()
        {
            CurrentPage = TotalPage;

            int skipCount = (_totalPage - 1) * _pageSize;
            int takeCount = _source.Count - skipCount;

            var result = _source.Skip(skipCount).Take(takeCount).ToList();

            Customers = new ObservableCollection<Customer>(result);
        }
    }
}
