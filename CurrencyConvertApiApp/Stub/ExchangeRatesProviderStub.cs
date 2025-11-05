using CurrencyConvertApiApp.ExchangeRates;

namespace CurrencyConvertApiApp.Stub
{
    // ExchangeRatesProviderStub - заглушка провайдера курсов валют
    public class ExchangeRatesProviderStub : Provider
    {
        public List<ExchangeRate> GetRates(string baseCurrency)
        {
            return new List<ExchangeRate>
            {
                new ExchangeRate{Currency = "RUB", Rate = 1},
                new ExchangeRate{Currency = "USD", Rate = 78.10M},
                new ExchangeRate{Currency = "EUR", Rate = 91.26M},
                new ExchangeRate{Currency = "KZT", Rate = 0.15M},
            };
        }
    }
}
