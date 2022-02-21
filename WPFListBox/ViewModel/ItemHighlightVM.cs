using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using WPFListBox.Model;

namespace WPFListBox.ViewModel
{
    public class ItemHighlightVM
    {
        ResourceDictionary dict = Application.LoadComponent(new Uri("WPFCommon;component/Assets/Styles/Path.xaml", UriKind.RelativeOrAbsolute)) as ResourceDictionary;

        public ObservableCollection<GetFileDetails> GetFileDetails { get; private set; }// = new ObservableCollection<GetFileDetails>();
        public ItemHighlightVM()
        {
            GetFileDetails = new ObservableCollection<GetFileDetails> {
            new GetFileDetails(){ FileThumb=(PathGeometry)dict["Pdf"], FileName="File 1", Fill="Red", FileProgram="Adobe Acrobat", ModifiedOn="12.01.2020", FileType=".pdf"},
            new GetFileDetails(){ FileThumb=(PathGeometry)dict["Png"], FileName="File 2", Fill="Green", FileProgram="Photo Viewer", ModifiedOn="18.02.2020", FileType=".png"},
            new GetFileDetails(){ FileThumb=(PathGeometry)dict["txt"], FileName="File 3", Fill="CadetBlue", FileProgram="Notepad", ModifiedOn="15.07.2020", FileType=".txt"},
            new GetFileDetails(){ FileThumb=(PathGeometry)dict["Pdf"], FileName="File 4", Fill="Green", FileProgram="Adobe Acrobat", ModifiedOn="22.07.2020", FileType=".pdf"}
            };
        }
    }
}
