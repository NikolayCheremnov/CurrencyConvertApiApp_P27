namespace CurrencyConvertApiApp.Converter
{
    public class InvalidValueException : ApplicationException
    {
        public InvalidValueException(decimal value) :
            base ($"value '{value}' is invalid") { }
    }
}
