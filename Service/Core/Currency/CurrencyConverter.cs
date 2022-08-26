using Common.Enum;
using Domain.Models.Currency;
using Microsoft.Extensions.Primitives;
using Service.Messaging.Core;
using Service.ViewModel.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Entity = Domain;

namespace Service.Core.User
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private List<CurrencyModel> currencies = new();
        public CurrencyConverter()
        {
            currencies.Add(new CurrencyModel { Id = 1, From = "USD", To = "CAD", Rate = 1.34 });
            currencies.Add(new CurrencyModel { Id = 2, From = "CAD", To = "GBP", Rate = 0.58 });
            currencies.Add(new CurrencyModel { Id = 3, From = "USD", To = "EUR", Rate = 0.86 });
        }

        public void ClearConfiguration()
        {
            currencies = new List<CurrencyModel>();
        }

        public ConvertResponse Convert(ConvertRequest request)
        {
            ConvertResponse response = new();
            double res = 0;
            try
            {
                bool isCurrencyValid = IsCurrencyValid(request.ViewModel.From, request.ViewModel.To, request.ViewModel.Amount);
                if (isCurrencyValid)
                {
                    res = CalculateStraightPath(request.ViewModel.From, request.ViewModel.To, request.ViewModel.Amount);
                    if (res == 0)
                    {
                        res = CalculateComplexPath(request.ViewModel.From, request.ViewModel.To, request.ViewModel.Amount);
                    }
                    response.Result = res;
                    response.SuccessMessage();
                    response.Succeed();
                    return response;
                }
                response.Failed();
                response.FailedMessage("Please enter valid data");
                response.FailedMessage();
                return response;

            }
            catch (Exception ex)
            {
                response.Failed();
                response.FailedMessage(ex.Message);
                response.FailedMessage();

                return response;
            }
        }

        public UpdateResponse UpdateConfiguration(UpdateRequest conversionRates)
        {
            UpdateResponse response = new();
            try
            {
                foreach (var rate in conversionRates.ViewModel.ConversionRates)
                {
                    var currency = currencies.FirstOrDefault(x => x.From == rate.Item1 && x.To == rate.Item2);
                    if (currency is not null)
                    {
                        currency.Rate = rate.Item3;
                    }
                    else
                    {
                        currencies.Add(new CurrencyModel 
                        {
                            From = rate.Item1,
                            To = rate.Item2,
                            Rate = rate.Item3,
                        });
                    }
                }
                response.SuccessMessage();
                response.Succeed();
                return response;
            }
            catch (Exception ex)
            {
                response.Failed();
                response.FailedMessage(ex.Message);
                response.FailedMessage();

                return response;
            }
        }

        private double CalculateStraightPath(string from, string to, double amount)
        {
            var currencyMultify = FindCurrency(from, to);


            if (currencyMultify is not null)
            {
                return amount * currencyMultify.Rate;
            }
            else
            {

                var currencyDevide = FindCurrency(to, from);

                if (currencyDevide is not null)
                {
                    return amount / currencyDevide.Rate;
                }
                return 0;
            }


        }

        private double CalculateComplexPath(string from, string to, double amount)
        {
            List<CurrencyModel> iteratedCurrencies = new();
            double res = 0;
            double mediatorResult = 0;
            int count = 0;

            while (res == 0 && count < currencies.Count)
            {

                var currencyMultify = FindComplexCurrency(from, to, iteratedCurrencies.Count);

                if (currencyMultify is not null)
                {
                    iteratedCurrencies.Add(currencyMultify);
                    mediatorResult = amount * currencyMultify.Rate;
                    res = CalculateStraightPath(from, currencyMultify.From, mediatorResult);
                }
                else
                {

                    var currencyDevide = FindComplexCurrency(to, from, iteratedCurrencies.Count);

                    if (currencyDevide is not null)
                    {
                        iteratedCurrencies.Add(currencyDevide);
                        mediatorResult = amount / currencyDevide.Rate;
                        res = CalculateStraightPath(from, currencyDevide.To, mediatorResult);
                    }
                }
                count++;
            }
            return res;
        }
        private CurrencyModel FindCurrency(string from, string to)
        {

            var currency = currencies.FirstOrDefault(x =>
                 x.From.ToLower() == from.ToLower()
              && x.To.ToLower() == to.ToLower());

            return currency;
        }

        private CurrencyModel FindComplexCurrency(string from, string to, int skip)
        {

            var currency = currencies
                .Where(x => x.To.ToLower() == to.ToLower())
                .Skip(skip)
                .FirstOrDefault();

            return currency;
        }
        private bool IsCurrencyValid(string from, string to, double amount)
        {

            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to) || amount <= 0)
                return false;


            return true;
        }
    }
}
