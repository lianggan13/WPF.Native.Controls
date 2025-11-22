namespace WPF.Thumb.ThumbFlow
{
    public class ToolModel
    {
        public string Header { get; set; }

        public string Icon { get; set; }

        public string TargetObject { get; set; }

        public List<ToolModel> Children { get; set; } =
            new List<ToolModel>();
    }
}
