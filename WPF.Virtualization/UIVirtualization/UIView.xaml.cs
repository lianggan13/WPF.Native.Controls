using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF.Virtualization.UIVirtualization.Model;

namespace WPF.Virtualization.UIVirtualization
{
    public partial class UiView : UserControl
    {
        public UiView()
        {
            InitializeComponent();
        }


        private void ListView_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView colletion = CollectionViewSource.GetDefaultView(vm.Items);
            GridViewColumnHeader currentHeader = e.OriginalSource as GridViewColumnHeader;
            if (currentHeader != null && currentHeader.Role != GridViewColumnHeaderRole.Padding)
            {
                using (colletion.DeferRefresh())
                {
                    Func<SortDescription, bool> lamda = item => item.PropertyName.Equals(currentHeader.Column.Header.ToString());
                    if (colletion.SortDescriptions.Count(lamda) > 0)
                    {
                        SortDescription currentSortDescription = colletion.SortDescriptions.First(lamda);
                        ListSortDirection sortDescription = currentSortDescription.Direction == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;


                        currentHeader.Column.HeaderTemplate = currentSortDescription.Direction == ListSortDirection.Ascending ?
                            Resources["HeaderTemplateArrowDown"] as DataTemplate : Resources["HeaderTemplateArrowUp"] as DataTemplate;

                        colletion.SortDescriptions.Remove(currentSortDescription);
                        colletion.SortDescriptions.Insert(0, new SortDescription(currentHeader.Column.Header.ToString(), sortDescription));
                    }
                    else
                        colletion.SortDescriptions.Add(new SortDescription(currentHeader.Column.Header.ToString(), ListSortDirection.Ascending));
                }
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView colletion = CollectionViewSource.GetDefaultView(vm.Items);
            colletion.Filter = item =>
            {
                ViewItem vitem = item as ViewItem;
                if (vitem == null) return false;

                PropertyInfo info = item.GetType().GetProperty(cmbProperty.Text);
                if (info == null) return false;

                return info.GetValue(vitem, null).ToString().Contains(txtFilter.Text);
            };
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView colletion = CollectionViewSource.GetDefaultView(vm.Items);
            colletion.Filter = item => true;
        }

        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView colletion = CollectionViewSource.GetDefaultView(vm.Items);
            colletion.GroupDescriptions.Clear();

            PropertyInfo pinfo = typeof(ViewItem).GetProperty(cmbGroups.Text);
            if (pinfo != null)
            {
                using (colletion.DeferRefresh())
                {
                    colletion.GroupDescriptions.Add(new PropertyGroupDescription(pinfo.Name));
                }
            }
        }

        private void btnClearGr_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView colletion = CollectionViewSource.GetDefaultView(vm.Items);
            colletion.GroupDescriptions.Clear();
        }

        private void btnNavigation_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView colletion = CollectionViewSource.GetDefaultView(vm.Items);
            Button CurrentButton = sender as Button;

            switch (CurrentButton.Tag.ToString())
            {
                case "0":
                    colletion.MoveCurrentToFirst();
                    break;
                case "1":
                    colletion.MoveCurrentToPrevious();
                    break;
                case "2":
                    colletion.MoveCurrentToNext();
                    break;
                case "3":
                    colletion.MoveCurrentToLast();
                    break;
            }

        }

        private void btnEvaluate_Click(object sender, RoutedEventArgs e)
        {
            ViewItem item = this.lvItems.SelectedItem as ViewItem;

            string msg = string.Format("Hello {0}, Developer in {1} with Salary {2}", item.Name, item.Developer, item.Salary);
            MessageBox.Show(msg);
        }
    }

}
