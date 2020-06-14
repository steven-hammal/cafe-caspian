using CafeCaspian.Application.Config;
using CafeCaspian.Domain;
using Microsoft.Extensions.Options;

namespace CafeCaspian.Application
{
    public interface ISurchargeService
    {
        decimal GetSurchargeFor(Cheque cheque);
    }

    public class SurchargeService : ISurchargeService
    {
        private readonly SurchargeOptions _options;

        public SurchargeService(IOptions<SurchargeOptions> options)
        {
            _options = options.Value;
        }

        public decimal GetSurchargeFor(Cheque cheque)
        {
            var rate = GetRateFor(cheque);
            return GetServiceCharge(cheque.NetTotal, rate);
        }

        private decimal GetRateFor(Cheque cheque)
        {
            if (cheque.ContainsHotFood())
            {
                return _options.HotFoodServiceRate;
            }

            if (cheque.ContainsFood())
            {
                return _options.FoodServiceRate;
            }

            return _options.DefaultServiceRate;
        }

        private decimal GetServiceCharge(decimal chequeNetTotal, decimal rate)
        {
            var totalServiceCharge = (chequeNetTotal * rate) - chequeNetTotal;

            return totalServiceCharge > _options.MaxServiceCharge ? _options.MaxServiceCharge : totalServiceCharge;
        }
    }
}
