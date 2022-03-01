namespace WPFChart.Model
{
    public class GpsCnPowerModel
    {
        public double Cn { get; private set; }
        public double Power { get; private set; }

        public GpsCnPowerModel(double cn, double power)
        {
            Cn = cn;
            Power = power;
        }
    }
}
