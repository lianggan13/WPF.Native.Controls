using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using WPF.Common.Database;

namespace WPF.ViewCollection.View
{
    /// <summary>
    /// SelectViewItem.xaml 的交互逻辑
    /// </summary>
    public partial class SelectViewItem : UserControl
    {
        private ObservableCollection<Product> products;

        public SelectViewItem()
        {
            InitializeComponent();

            products = new ObservableCollection<Product>(App.StoreDb.GetProducts());

            DataContext = products;

            navbar.Collection = products;
            navbar.ItemType = typeof(Product);

            lstProducts.ItemsSource = products;
        }

        void NewOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            products.Add(new Product());
        }

        const string strFilter = "People XML files (*.PeopleXml)|" +
                                 "*.PeopleXml|All files (*.*)|*.*";

        void OpenOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = strFilter;

            if ((bool)dlg.ShowDialog(Application.Current.MainWindow))
            {
                try
                {
                    StreamReader reader = new StreamReader(dlg.FileName);
                    XmlSerializer xml = new XmlSerializer(typeof(Product));
                    products = (ObservableCollection<Product>)xml.Deserialize(reader);
                    reader.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Could not load file: " + exc.Message, "Open",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);
                }
            }

            navbar.Refresh();
        }
        void SaveOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = strFilter;

            if ((bool)dlg.ShowDialog(Application.Current.MainWindow))
            {
                try
                {
                    StreamWriter writer = new StreamWriter(dlg.FileName);
                    XmlSerializer xml = new XmlSerializer(GetType());
                    xml.Serialize(writer, products);
                    writer.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Could not save file: " + exc.Message,
                                   "Save", MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);
                }
            }
        }


    }
}
