using CurrencyConvertApiApp.ExchangeRates;
using System.Collections.ObjectModel;

namespace CurrencyConvertApiApp.Converter
{
    // SimpleCurrencyConverter - простой конвертер валют
    public class SimpleCurrencyConverter : CurrencyConverter
    {
        private readonly Dictionary<string, decimal> _rates = new(); // сохраненные курсы валют
        
        public SimpleCurrencyConverter(Provider provider)
        {
            List<ExchangeRate> exchangeRates = provider.GetRates("RUB");
            foreach (ExchangeRate er in exchangeRates)
            {
                _rates[er.Currency] = er.Rate;
            }
        }

        public string[] SupportedCurrencies()
        {
            return _rates.Keys.ToArray();
        }

        public decimal Convert(string from, string to, decimal value)
        {
            if (value < 0)
            {
                throw new InvalidValueException(value);
            }
            if (!_rates.ContainsKey(from))
            {
                throw new UnsupportedCurrencyException(from);
            }
            if (!_rates.ContainsKey(to))
            {
                throw new UnsupportedCurrencyException(to);
            }
            return value * _rates[from] / _rates[to];
        }
    }
}
