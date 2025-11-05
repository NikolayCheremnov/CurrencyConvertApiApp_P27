namespace CurrencyConvertApiApp.Api
{
    // api-сообщения

    // StringMessage - тип строкового сообщения с указанием времени
    public record StringMessage(string Message, DateTime Time);

    // ConversionResultMessage - результат конвертации
    public record ConversionResultMessage(string Currency, decimal Value);

    // ErrorMessage - сообщение об ошибке
    public record ErrorMessage(string Type, string Message);
}
