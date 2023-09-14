using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WPFItemsControl.Model;

namespace WPFItemsControl.Assets
{
    public class CourseDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RealTemplate { get; set; }
        public DataTemplate SkeletonTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var ds = item as CourseSeriesModel;
            if (ds == null)
            {
                return base.SelectTemplate(item, container);
            }

            PropertyChangedEventHandler lambda = null;
            lambda = (o, args) =>
            {
                if (args.PropertyName == nameof(CourseSeriesModel.IsShowSkeleton))
                {
                    ds.PropertyChanged -= lambda;
                    var cp = (ContentPresenter)container;
                    cp.ContentTemplateSelector = null;
                    cp.ContentTemplateSelector = this;
                }
            };
            ds.PropertyChanged += lambda;


            if ((item as CourseSeriesModel).IsShowSkeleton)
            {
                return SkeletonTemplate;
            }

            return RealTemplate;
        }
    }
}
