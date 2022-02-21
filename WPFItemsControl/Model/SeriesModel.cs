namespace WPFItemsControl.Model
{
    public class SeriesModel
    {
        public string SeriesName { get; set; }
        public decimal CurrentValue { get; set; }
        public bool IsGrowing { get; set; }
        public int ChangeRate { get; set; }
    }
}
