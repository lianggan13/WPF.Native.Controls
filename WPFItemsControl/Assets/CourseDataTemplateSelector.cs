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
            if ((item as CourseSeriesModel).IsShowSkeleton)
            {
                return SkeletonTemplate;
            }

            return RealTemplate;
            //return base.SelectTemplate(item, container);
        }
    }
}
