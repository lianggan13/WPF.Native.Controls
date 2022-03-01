using System.Windows.Media;
using WPFCommon.MVVMFoundation;

namespace WPFListBox.Model
{
    class ItemHighlightModel
    {
    }

    public class GetFileDetails : NotifyPropertyChanged
    {
        public PathGeometry FileThumb { get; set; }
        public string Fill { get; set; }
        public string FileName { get; set; }
        public string FileProgram { get; set; }
        public string ModifiedOn { get; set; }
        public string FileType { get; set; }
    }
}
