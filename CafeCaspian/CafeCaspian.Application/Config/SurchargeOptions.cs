namespace CafeCaspian.Application.Config
{
    public class SurchargeOptions
    {
        public const string Surcharge = "Surcharge";

        public decimal DefaultServiceRate { get; set; }
        public decimal MaxServiceCharge { get; set; }
        public decimal HotFoodServiceRate { get; set; }
        public decimal FoodServiceRate { get; set; }
    }
}
