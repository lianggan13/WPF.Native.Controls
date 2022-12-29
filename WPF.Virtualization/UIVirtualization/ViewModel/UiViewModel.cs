using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WPF.Virtualization.UIVirtualization.Model;


namespace WPF.Virtualization.UIVirtualization.ViewModel
{
    public class UiViewModel
    {
        private ObservableCollection<ViewItem> items;
        public ObservableCollection<ViewItem> Items
        {
            get
            {
                items = items ?? LoadItems();
                return items;
            }
        }

        public IEnumerable<string> Columns
        {
            get
            {
                return from prop in typeof(ViewItem).GetProperties()
                       select prop.Name;
            }
        }
        public IEnumerable<string> DeveloperList
        {
            get
            {
                return (from developer in Items
                        select developer.Developer).Distinct();
            }
        }
        private ObservableCollection<ViewItem> LoadItems()
        {
            ObservableCollection<ViewItem> items = new ObservableCollection<ViewItem>();

            items.Add(new ViewItem { Id = "1", Name = "Abhishek", Developer = "WPF", Salary = 50000.20f });
            items.Add(new ViewItem { Id = "2", Name = "Abhijit", Developer = "ASP.NET", Salary = 89000.20f });
            items.Add(new ViewItem { Id = "3", Name = "Scott", Developer = "ASP.NET", Salary = 95000.20f });
            items.Add(new ViewItem { Id = "4", Name = "Kunal", Developer = "Silverlight", Salary = 26000.20f });
            items.Add(new ViewItem { Id = "5", Name = "Hanselman", Developer = "ASP.NET", Salary = 78000.20f });
            items.Add(new ViewItem { Id = "6", Name = "Peter", Developer = "WPF", Salary = 37000.20f });
            items.Add(new ViewItem { Id = "7", Name = "Tim", Developer = "Silverlight", Salary = 45000.20f });
            items.Add(new ViewItem { Id = "8", Name = "John", Developer = "ASP.NET", Salary = 70000.20f });
            items.Add(new ViewItem { Id = "9", Name = "Shibatosh", Developer = "WPF", Salary = 40000.20f });


            var ids = Enumerable.Range(10, 999);
            foreach (var i in ids)
            {
                items.Add(new ViewItem { Id = $"{i}", Name = $"Lianggan{i}", Developer = "WPF", Salary = 40000.20f });
            }

            return items;
        }
    }
}
