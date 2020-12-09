using System.Collections.Generic;
using System.Linq;
using Volta.Stocks.Domain.Stocks.Services;

namespace Volta.Stocks.Domain.Tests
{
    public class FakeCurrencyLookup : ICurrencyLookup
    {
        private static readonly IEnumerable<CurrencyDetails> Currencies = new[]
        {
            CurrencyDetails.Of("EUR",2, true),
            CurrencyDetails.Of("USD",2, true),
            CurrencyDetails.Of("JPY",0, true),
            CurrencyDetails.Of("DEM",2, false)
        };

        public CurrencyDetails FindCurrency(string currencyCode)
        {
            var currency = Currencies.FirstOrDefault(x => x.CurrencyCode == currencyCode);
            return currency ?? CurrencyDetails.None;
        }
    }
}