using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ListViewLocation.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class LocationViewModel : ViewModelBase
    {
        private ItemData listViewItemData;

        public ItemData ListViewItemData
        {
            get { return listViewItemData; }
            set { listViewItemData = value; RaisePropertyChanged(); }
        }



        private ObservableCollection<ItemData> listViewData;

        public ObservableCollection<ItemData> ListViewData
        {
            get { return listViewData; }
            set { listViewData = value; RaisePropertyChanged(); }
        }

        public RelayCommand SelectedItemBtnCommand => new RelayCommand(async () =>
       {
           foreach (var item in ListViewData)
           {
               await Task.Delay(1000 / 2);
               ListViewItemData = item; // ½«»á´¥·¢ SelectionChangedEvent
           }
       });

        public RelayCommand<ListView> SelectionChangedCommand => new RelayCommand<ListView>((obj) =>
        {
            //if (obj.SelectedItem != null)
            //{
            //    object data = obj.SelectedItem;
            //    ListViewItem listViewItem = obj.ItemContainerGenerator.ContainerFromItem(data) as ListViewItem;
            //    listViewItem.Focus();
            //}

            obj.ScrollIntoView(obj?.SelectedItem);
        });


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public LocationViewModel()
        {
            ListViewData = new ObservableCollection<ItemData>();

            for (int i = 0; i < 30; i++)
            {
                ListViewData.Add(new ItemData { UID = Guid.NewGuid() });
            }


        }
    }
    public class ItemData : ObservableObject
    {
        private Guid uid;

        public Guid UID
        {
            get { return uid; }
            set { uid = value; }
        }

    }
}