using CurrencyConvertApiApp.Converter;
using CurrencyConvertApiApp.Stub;
using System.ComponentModel;
using System.Globalization;

// чтобы не было проблем при выводе decimal
var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var builder = WebApplication.CreateBuilder(args);

// добавление сервисов приложения:
builder.Services.AddSingleton<CurrencyConverter>(
    _ => new SimpleCurrencyConverter(new ExchangeRatesProviderStub())
);
builder.Services.AddControllers();
builder.Services.AddRazorPages();

// сборка приложения
var app = builder.Build();

// конфигурация приложения:
app.MapControllers();
app.MapRazorPages();

// запуск приложения
app.Run();
