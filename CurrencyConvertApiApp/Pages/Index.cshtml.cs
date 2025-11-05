using CurrencyConvertApiApp.Converter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CurrencyConvertApiApp.Pages
{
    public class IndexModel : PageModel
    {
        // используемый сервис конвертации валют
        // внедряется через конструктор с помощью механизма DI ASP.NET
        private readonly CurrencyConverter _converter;

        public IndexModel(CurrencyConverter converter)
        {
            _converter = converter;
        }

        // публичные свойства модели Razor-страницы (то, что будем выводить в html)

        public string[] SupportedCurrencies { get; private set; } = [];

        // входные данные
        public string From { get; private set; } = string.Empty;
        public string To {  get; private set; } = string.Empty;
        public decimal Value {  get; private set; }

        // результат
        public decimal ResultValue { get; private set; }
        public string? ErrorMessage { get; private set; }

        public void OnGet(string? from, string? to, decimal? value)
        {
            // всегда получать поддерживаемые коды валют
            SupportedCurrencies = _converter.SupportedCurrencies();

            // конвертация

            // задим входные данные
            From = from ?? SupportedCurrencies[0];
            To = to ?? SupportedCurrencies[0];
            Value = value ?? 1M;

            // сконвертировать
            try
            {
                ResultValue = _converter.Convert(From, To, Value);
            } catch (InvalidValueException)
            {
                ErrorMessage = "Отправлено некорректное значение для конвертации";
            } catch (UnsupportedCurrencyException)
            {
                ErrorMessage = "Одна из введенных валют не поддерживается";
            }
        }
    }
}
