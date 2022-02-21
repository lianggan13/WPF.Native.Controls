using LiveCharts;
using System.Collections.ObjectModel;

namespace WPFItemsControl.Model
{
    public class CourseSeriesModel
    {
        public string CourseName { get; set; }

        public SeriesCollection SeriesColection { get; set; }

        public ObservableCollection<SeriesModel> SeriesList { get; set; }

        public bool IsShowSkeleton { get; set; }

    }
}
