using System.Collections.ObjectModel;
using WPFItemsControl.Model;

namespace WPFItemsControl.ViewModel
{
    public class GroupingVM
    {
        public ObservableCollection<ModelFile> CollectionModelFile { get; set; } = new ObservableCollection<ModelFile>();

        public GroupingVM()
        {
            CollectionModelFile.Add(new ModelFile() { FileName = "WPF进化史", AuthorName = "王鹏", UpTime = "2014-10-10" });
            CollectionModelFile.Add(new ModelFile() { FileName = "WPF概论", AuthorName = "大飞", UpTime = "2014-10-10" });
            CollectionModelFile.Add(new ModelFile() { FileName = "WPF之美", AuthorName = "小虫", UpTime = "2014-10-11" });
            CollectionModelFile.Add(new ModelFile() { FileName = "WPF之道", AuthorName = "青草", UpTime = "2014-11-11" });
            CollectionModelFile.Add(new ModelFile() { FileName = "WPF之禅", AuthorName = "得瑟鬼", UpTime = "2014-11-11" });
            CollectionModelFile.Add(new ModelFile() { FileName = "WPF入门", AuthorName = "今晚吃什么", UpTime = "2014-11-11" });
            CollectionModelFile.Add(new ModelFile() { FileName = "WPF神技", AuthorName = "无间道王二", UpTime = "2014-12-12" });
            CollectionModelFile.Add(new ModelFile() { FileName = "WPF不为人知之密", AuthorName = "星期八", UpTime = "2014-12-12" });
            CollectionModelFile.Add(new ModelFile() { FileName = "WPF之革命", AuthorName = "A两把刀", UpTime = "2014-12-12" });
            CollectionModelFile.Add(new ModelFile() { FileName = "WPF之革命", AuthorName = "B两把刀", UpTime = "2014-12-12" });
            CollectionModelFile.Add(new ModelFile() { FileName = "WPF之革命", AuthorName = "C两把刀", UpTime = "2014-12-12" });

            //ICollectionView cv = CollectionViewSource.GetDefaultView(CollectionModelFile);
            //cv.SortDescriptions.Add(new SortDescription(nameof(ModelFile.AuthorName), ListSortDirection.Descending));
            //cv.GroupDescriptions.Add(new PropertyGroupDescription("UpTime"));
        }
    }
}
