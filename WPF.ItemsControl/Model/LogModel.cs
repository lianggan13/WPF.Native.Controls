namespace WPFItemsControl.Model
{
    public class LogModel
    {
        public string DeviceId { get; set; }
        public int Num { get; set; }
        public string DeviceName { get; set; }
        public string LogInfo { get; set; }
        public string Message { get; set; }
        public double Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public LogType LogType { get; set; } = LogType.Info;
    }

    public enum LogType
    {
        Info = 0, Warn = 1, Fault = 2
    }
}
