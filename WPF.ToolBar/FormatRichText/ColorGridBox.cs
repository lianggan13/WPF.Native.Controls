﻿//----------------------------------------------------
// FormatRichText.File.cs (c) 2006 by Charles Petzold
//----------------------------------------------------
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF.ToolBar.FormatRichText
{
    class ColorGridBox : ListBox
    {
        // The colors to be displayed.
        string[] strColors =
        {
            "Black", "Brown", "DarkGreen", "MidnightBlue",
                "Navy", "DarkBlue", "Indigo", "DimGray",
            "DarkRed", "OrangeRed", "Olive", "Green",
                "Teal", "Blue", "SlateGray", "Gray",
            "Red", "Orange", "YellowGreen", "SeaGreen",
                "Aqua", "LightBlue", "Violet", "DarkGray",
            "Pink", "Gold", "Yellow", "Lime",
                "Turquoise", "SkyBlue", "Plum", "LightGray",
            "LightPink", "Tan", "LightYellow", "LightGreen",
                "LightCyan", "LightSkyBlue", "Lavender", "White"
        };

        public ColorGridBox()
        {
            // Define the ItemsPanel template.
            FrameworkElementFactory factoryUnigrid =
                            new FrameworkElementFactory(typeof(UniformGrid));
            factoryUnigrid.SetValue(UniformGrid.ColumnsProperty, 8);
            ItemsPanel = new ItemsPanelTemplate(factoryUnigrid);

            // Add items to the ListBox.
            foreach (string strColor in strColors)
            {
                // Create Rectangle and add to ListBox.
                Rectangle rect = new Rectangle();
                rect.Width = 12;
                rect.Height = 12;
                rect.Margin = new Thickness(4);
                rect.Fill = (Brush)
                    typeof(Brushes).GetProperty(strColor).GetValue(null, null);
                Items.Add(rect);

                // Create ToolTip for Rectangle.
                ToolTip tip = new ToolTip();
                tip.Content = strColor;
                rect.ToolTip = tip;
            }
            // Indicate that SelectedValue is Fill property of Rectangle item.
            SelectedValuePath = "Fill";
        }
    }
}
