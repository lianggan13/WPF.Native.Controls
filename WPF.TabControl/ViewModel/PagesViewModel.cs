using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using WPF.TabControl.Model;
using WPFCommon.MVVMFoundation;

namespace WPF.TabControl.ViewModel
{
    public class PagesViewModel : NotifyPropertyChanged
    {
        public ObservableCollection<PageItemModel> Pages { get; set; }
          = new ObservableCollection<PageItemModel>();

        public PagesViewModel()
        {
            var pns = new string[] { "BlankPage", "PBomPage" };
            var pages = pns.Select(pn =>
            {
                Type type = Assembly.GetExecutingAssembly().GetType($"WPF.TabControl.View.Pages.{pn}");
                object view = Activator.CreateInstance(type);
                var page = new PageItemModel()
                {
                    Header = pn,
                    PageView = view,
                };
                return page;
            }).ToList();

            pages.ForEach(p => Pages.Add(p));
        }

    }
}
