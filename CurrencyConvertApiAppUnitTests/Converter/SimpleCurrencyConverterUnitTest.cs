using System;
using NUnit.Framework;
using CurrencyConvertApiApp.Converter;
using CurrencyConvertApiApp.Stub;

namespace CurrencyConvertApiAppUnitTests.Converter
{
    public class SimpleCurrencyConverterUnitTest
    {
        private SimpleCurrencyConverter _converter = null!;

        [SetUp]
        public void Setup()
        {
            _converter = new SimpleCurrencyConverter(new ExchangeRatesProviderStub());
        }

        [Test]
        public void Convert_WithValidCurrencies_ComputesExpectedValue()
        {
            // Given rates: RUB=1, USD=78.10, EUR=91.26
            // 1 USD -> EUR = 1 * 78.10 / 91.26
            decimal result = _converter.Convert("USD", "EUR", 1m);

            Assert.That(result, Is.EqualTo(78.10m / 91.26m).Within(0.0001m));
        }

        [Test]
        public void Convert_WithUnknownFrom_ThrowsUnsupportedCurrencyException()
        {
            Assert.Throws<UnsupportedCurrencyException>(() => _converter.Convert("ABC", "USD", 1m));
        }

        [Test]
        public void Convert_WithUnknownTo_ThrowsUnsupportedCurrencyException()
        {
            Assert.Throws<UnsupportedCurrencyException>(() => _converter.Convert("USD", "XYZ", 1m));
        }

        [Test]
        public void Convert_WithNegativeValue_ThrowsInvalidValueException()
        {
            Assert.Throws<InvalidValueException>(() => _converter.Convert("USD", "EUR", -5m));
        }
    }
}
