using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Pagination.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Pagination.ViewModel
{

    public class PaginationViewModel : ObservableObject
    {
        public ObservableCollection<PaginationItemModel> Pages { get; set; } = new ObservableCollection<PaginationItemModel>();

        private int pageIndex = 8;

        public int PageIndex
        {
            get { return pageIndex; }
            set
            {
                pageIndex = value;
                OnPropertyChanged();
            }
        }


        private int pageCount = 10;

        public int PageCount
        {
            get { return pageCount; }
            set
            {
                pageCount = value;
                OnPropertyChanged();
            }
        }

        public ICommand PgUpCommand { get; set; }
        public ICommand PgDnCommand { get; set; }
        public ICommand PgJumpCommand { get; set; }

        public PaginationViewModel()
        {
            // 上一页
            PgUpCommand = new RelayCommand(
                () =>
                {
                    FillPages(--PageIndex, PageCount);
                },
                () => PageIndex > 1);

            // 下一页
            PgDnCommand = new RelayCommand(
                () =>
                {
                    FillPages(++PageIndex, PageCount);
                },
                () => PageIndex < PageCount);

            PgJumpCommand = new RelayCommand<string>(param =>
            {
                if (int.TryParse(param, out int index))
                {
                    FillPages(PageIndex = index, PageCount);
                }
            });

            FillPages(PageIndex, PageCount);
        }

        public void FillPages(int pageIndex, int pageCount)
        {
            // 1 2 3 4 [5] 6 7 8 9 ... 15
            // 1 ... 3 4 5 [6] 7 8 9... 15
            // 1 ... 7 8 9 [10] 11 12 13 ... 15
            // 1 ... 8 9 10 [11] 12 13 14 15      10条    50条
            // 省略首页和尾页

            int left = pageIndex - 4;
            int right = pageIndex + 4;
            List<string> pageIndexes = new List<string>();

            if (left <= 1)
            {
                left = 1;
            }
            else
            {
                left++;
            }

            if (right >= pageCount)
            {
                right = pageCount;
            }
            else
            {
                right--;
            }


            if (left > 1)
            {
                pageIndexes.Add("1");
                pageIndexes.Add("...");
            }
            for (int i = left; i <= right; i++)
            {
                pageIndexes.Add(i.ToString());
            }
            if (right < pageCount)
            {
                pageIndexes.Add("...");
                pageIndexes.Add($"{pageCount}");
            }

            Pages.Clear();
            foreach (var item in pageIndexes)
            {
                bool state = int.TryParse(item, out int index);
                Pages.Add(new PaginationItemModel
                {
                    Index = state ? $"{index}" : item,
                    IsCurrent = index == pageIndex,
                    IsEnabled = state
                });
            }
        }
    }
}
