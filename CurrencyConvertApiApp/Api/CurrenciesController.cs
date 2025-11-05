using CurrencyConvertApiApp.Converter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;

namespace CurrencyConvertApiApp.Api
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        // сервис конвертирования валют
        private readonly CurrencyConverter _converter;

        public CurrenciesController(CurrencyConverter converter)
        {
            _converter = converter;
        }

        // GET /api/currencies
        [HttpGet]
        public string[] GetCurrencies()
        {
            return _converter.SupportedCurrencies();
        }

        // GET /api/currencies/convert/v1
        [HttpGet("convert/v1")]
        public async void Convert()
        {
            // 1. считать исходные данные

            // 1.1) считать from
            ErrorMessage? fromError = CheckCurrencyCode("from");
            if (fromError != null)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsJsonAsync(fromError);
                return;
            }
            string from = Request.Query["from"]![0]!;

            // 1.2) считать to
            ErrorMessage? toError = CheckCurrencyCode("to");
            if (toError != null)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsJsonAsync(toError);
                return;
            }
            string to = Request.Query["to"]![0]!;

            // 1.3) считать value
            decimal value;
            ErrorMessage? valueError = CheckValue(out value);
            if (valueError != null)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsJsonAsync(valueError);
                return;
            }

            // 2. выполнить вычисления и сформировать и отправить результат
            try
            {
                decimal resultValue = _converter.Convert(from, to, value);
                // 200 
                Response.StatusCode = StatusCodes.Status200OK;
                ConversionResultMessage result = new ConversionResultMessage(Currency: to, Value: resultValue);
                await Response.WriteAsJsonAsync(result);
            }
            catch (InvalidValueException ex)
            {
                // 400
                Response.StatusCode = StatusCodes.Status400BadRequest;
                ErrorMessage error = new ErrorMessage(Type: ex.GetType().Name, ex.Message);
                await Response.WriteAsJsonAsync(error);
                return;
            }
            catch (UnsupportedCurrencyException ex)
            {
                // 404
                Response.StatusCode = StatusCodes.Status404NotFound;
                ErrorMessage error = new ErrorMessage(Type: ex.GetType().Name, ex.Message);
                await Response.WriteAsJsonAsync(error);
                return;
            }
        }

        // GET /api/currencies/convert/v2
        [HttpGet("convert/v2")]
        public IActionResult Convert(string from, string to, decimal value)
        {
            try
            {
                decimal resultValue = _converter.Convert(from, to, value);
                // 200 
                ConversionResultMessage result = new ConversionResultMessage(Currency: to, Value: resultValue);
                return Ok(result);
            }
            catch (InvalidValueException ex)
            {
                // 400
                ErrorMessage error = new ErrorMessage(Type: ex.GetType().Name, ex.Message);
                return BadRequest(error);
            }
            catch (UnsupportedCurrencyException ex)
            {
                // 404
                ErrorMessage error = new ErrorMessage(Type: ex.GetType().Name, ex.Message);
                return NotFound(error);
            }
        }

        // вспомогательные методы контроллера

        // CheckCurrencyCode - проверка кода валюты из get-параметров
        // вход: param - название параметра
        // выход: ошибка либо null

        private ErrorMessage? CheckCurrencyCode(string param)
        {
            StringValues paramValues = new StringValues();
            bool exists = Request.Query.TryGetValue(param, out paramValues);
            if (!exists)
            {
                return new ErrorMessage(
                    Type: ErrorCodes.MissingQueryParameter,
                    $"query paramter '{param}' is missing"
                );
            }
            if (paramValues.Count != 1)
            {
                return new ErrorMessage(
                    Type: ErrorCodes.InvalidQueryParamter,
                    $"query paramter '{param}' must contain only single value"
                );
            }
            if (string.IsNullOrEmpty(paramValues[0]))
            {
                return new ErrorMessage(
                    Type: ErrorCodes.InvalidQueryParamter,
                    $"query paramter '{param}' is empty"
                );
            }
            return null;
        }

        public ErrorMessage? CheckValue(out decimal value)
        {
            value = 0;
            StringValues valueValues = new StringValues();
            bool exists = Request.Query.TryGetValue("value", out valueValues);
            if (!exists)
            {
                return new ErrorMessage(
                    Type: ErrorCodes.MissingQueryParameter,
                    "query paramter 'value' is missing"
                );
            }
            if (valueValues.Count != 1)
            {
                return new ErrorMessage(
                    Type: ErrorCodes.InvalidQueryParamter,
                    "query paramter 'value' must contain only single value"
                );
            }
            if (string.IsNullOrEmpty(valueValues[0]))
            {
                return new ErrorMessage(
                    Type: ErrorCodes.InvalidQueryParamter,
                    "query paramter 'value' is empty"
                );
            }
            if (!decimal.TryParse(valueValues[0], out value))
            {
                return new ErrorMessage(
                    Type: ErrorCodes.InvalidQueryParamter,
                    "query paramter 'value' must be a number"
                );
            }

            return null;
        } 
    }
}
