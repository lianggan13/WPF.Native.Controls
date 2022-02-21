using System;
using System.Windows;

namespace WPFContentControl.Common
{
    public class ContentTypeDpObj : DependencyObject
    {
        /// <summary>
        /// 获取附加属性
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Type GetContentType(DependencyObject d)
        {
            return (Type)d.GetValue(ContentTypeProperty);
        }

        /// <summary>
        /// 设置附加属性
        /// </summary>
        /// <param name="d"></param>
        /// <param name="value"></param>
        public static void SetContentType(DependencyObject d, Type value)
        {
            d.SetValue(ContentTypeProperty, value);
        }
        // Using a DependencyProperty as the backing store for ContentType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentTypeProperty =
            DependencyProperty.Register("ContentType", typeof(Type), typeof(ContentTypeDpObj), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnPropertyChanged)));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}
