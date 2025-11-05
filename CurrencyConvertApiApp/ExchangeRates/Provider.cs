namespace CurrencyConvertApiApp.ExchangeRates
{
    // Provider - сервис для получения курсов валют
    public interface Provider
    {
        // GetRates - получить курсы валют
        // вход: код базовой валюты (по отношению к которой будут получены курсы)
        // выход: список курсов валют по отношению к базовой
        List<ExchangeRate> GetRates(string baseCurrency);
    }
}
