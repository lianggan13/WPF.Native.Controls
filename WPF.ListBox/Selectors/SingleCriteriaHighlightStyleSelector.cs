using System;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using WPF.Common.Database;

namespace WPF.ListBox.Selectors
{
    public class SingleCriteriaHighlightStyleSelector : StyleSelector
    {
        public Style DefaultStyle
        {
            get;
            set;
        }

        public Style HighlightStyle
        {
            get;
            set;
        }

        public string PropertyToEvaluate
        {
            get;
            set;
        }

        public string PropertyValueToHighlight
        {
            get;
            set;
        }

        public override Style SelectStyle(object item,
          DependencyObject container)
        {
            Product product = (Product)item;

            // Use reflection to get the property to check.
            Type type = product.GetType();
            PropertyInfo property = type.GetProperty(PropertyToEvaluate);

            // Decide if this product should be highlighted
            // based on the property value.
            if (property.GetValue(product, null).ToString() == PropertyValueToHighlight)
            {
                return HighlightStyle;
            }
            else
            {
                return DefaultStyle;
            }
        }
    }

}