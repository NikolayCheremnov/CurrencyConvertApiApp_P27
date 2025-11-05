namespace CurrencyConvertApiApp.ExchangeRates
{
    // ExchangeRate - класс, описывающий курс валюты по отношению к некоторой валюте
    public class ExchangeRate
    {
        // код валюты
        public string Currency { get; set; } = string.Empty;

        // курс
        public decimal Rate;
    }
}
