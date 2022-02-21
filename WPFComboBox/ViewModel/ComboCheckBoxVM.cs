using GTS.MaxSign.Controls.TemplateParam.Model;
using System;

namespace GTS.MaxSign.Controls.TemplateParam.ViewModel
{
    public class ComboCheckBoxVM
    {
        public ComboCheckBoxModel Model { get; set; }


        private Action Save { get; set; }

        public ComboCheckBoxVM()
        {
            Model = new ComboCheckBoxModel();
            Model.Collection.Add(new ComboCheckBoxItemModel() { Content = "A", IsChecked = false });
            Model.Collection.Add(new ComboCheckBoxItemModel() { Content = "B", IsChecked = true });
            Model.Collection.Add(new ComboCheckBoxItemModel() { Content = "C", IsChecked = false });
        }
    }
}
