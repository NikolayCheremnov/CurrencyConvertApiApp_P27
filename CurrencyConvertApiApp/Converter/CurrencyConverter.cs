namespace CurrencyConvertApiApp.Converter
{
    // CurrencyConverter - конвертер валют
    public interface CurrencyConverter
    {
        // SupportedCurrencies - метод получения поддерживаемых для конвертации валют
        // вход: -
        // выход: массив с кодами валют
        // исключения: -
        string[] SupportedCurrencies();

        // Convert - метод конвертации значения одной валюты в другую
        // вход:
        //  - from - код исходной валюты
        //  - to - код целевой валюты
        //  - value - значение для конвертацииы
        // выход: сконвертированное значение
        // исключения: UnsupportedCurrencyException, InvalidValueException
        decimal Convert(string from, string to, decimal value);
    }
}
