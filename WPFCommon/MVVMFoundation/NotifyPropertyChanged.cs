using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFCommon.MVVMFoundation
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 属性改变时触发
        /// </summary>
        /// <param name="propertyName"></param>
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
