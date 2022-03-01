using System.Collections.ObjectModel;
using WPFItemsControl.Model;

namespace WPFItemsControl.ViewModel
{
    public class RealTimeLogVM
    {
        public ObservableCollection<LogModel> Logs { get; set; } = new ObservableCollection<LogModel>();

        public RealTimeLogVM()
        {
            Logs.Add(new LogModel { Num = 1, DeviceId = "CT001", DeviceName = "冷却塔1#", LogInfo = "已启动", LogType = 0 });
            Logs.Add(new LogModel { Num = 2, DeviceId = "CT002", DeviceName = "冷却塔2#", LogInfo = "已启动", LogType = 0 });
            Logs.Add(new LogModel { Num = 3, DeviceId = "CT003", DeviceName = "冷却塔3#", LogInfo = "液位极低", LogType = (LogType)1 });
            Logs.Add(new LogModel { Num = 4, DeviceId = "CP001", DeviceName = "循环水泵1#", LogInfo = "频率过大", LogType = (LogType)1 });
            Logs.Add(new LogModel { Num = 5, DeviceId = "CP002", DeviceName = "循环水泵2#", LogInfo = "已停机", LogType = 0 });
            Logs.Add(new LogModel { Num = 6, DeviceId = "CP003", DeviceName = "循环水泵3#", LogInfo = "已停机", LogType = 0 });
        }
    }
}
