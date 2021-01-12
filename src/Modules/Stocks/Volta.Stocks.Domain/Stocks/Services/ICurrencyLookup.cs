using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Services
{
    public interface ICurrencyLookup
    {
        CurrencyDetails FindCurrency(string currencyCode);
    }

    public class CurrencyDetails : ValueObject
    {
        public string CurrencyCode { get; }
        public bool InUse { get; }
        public int DecimalPlaces { get; }
        
        private CurrencyDetails(string currencyCode, int decimalPlaces, bool inUse)
        {
            CurrencyCode = currencyCode;
            DecimalPlaces = decimalPlaces;
            InUse = inUse;
        }

        public static CurrencyDetails Of(string currencyCode, int decimalPlaces, bool inUse)
        {
            return new CurrencyDetails(currencyCode, decimalPlaces, inUse);
        }

        public static CurrencyDetails None => new CurrencyDetails("", 0, false);
    }
}