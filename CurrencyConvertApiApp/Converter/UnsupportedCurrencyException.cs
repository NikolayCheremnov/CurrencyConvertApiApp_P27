namespace CurrencyConvertApiApp.Converter
{
    public class UnsupportedCurrencyException : ApplicationException
    {
        public UnsupportedCurrencyException(string code) : 
            base($"currency code '{code}' is unsupported") { }
    }
}
