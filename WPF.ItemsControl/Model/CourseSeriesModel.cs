using LiveCharts;
using System.Collections.ObjectModel;
using WPFCommon.MVVMFoundation;

namespace WPFItemsControl.Model
{
    public class CourseSeriesModel : NotifyPropertyChanged
    {
        public string CourseName { get; set; }

        public SeriesCollection SeriesColection { get; set; }

        public ObservableCollection<SeriesModel> SeriesList { get; set; }

        private bool isShowSkeleton;

        public bool IsShowSkeleton
        {
            get { return isShowSkeleton; }
            set { isShowSkeleton = value; OnPropertyChanged(); }
        }
    }
}
