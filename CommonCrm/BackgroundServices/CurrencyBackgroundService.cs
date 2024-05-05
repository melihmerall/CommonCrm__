using System.Text.Json;
using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities;
using CommonCrm.Data.Entities.AppUser;
using Microsoft.AspNetCore.Identity;

namespace CommonCrm.BackgroundServices;



public class CurrencyBackgroundService : BackgroundService
{
    public static class SharedData
    {
        public static List<ExchangeRate> _exchangeRates { get; set; } = new List<ExchangeRate>();
    }



    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (SharedData._exchangeRates.Any(rate => rate.CurrencyCode == "DLR" || rate.CurrencyCode == "EUR"))
                {
                    RemoveExchangeRate("DLR");
                    RemoveExchangeRate("EUR");
                }

                var exchangeRateDolar = await FetchExchangeRateAsync();
                SharedData._exchangeRates.Add(new ExchangeRate { CurrencyCode = "DLR", Rate = exchangeRateDolar });
                var exchangeRateEuro = await FetchExchangeRateEuroAsync();
                SharedData._exchangeRates.Add(new ExchangeRate { CurrencyCode = "EUR", Rate = exchangeRateEuro });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
            }

            await Task.Delay(TimeSpan.FromHours(4), stoppingToken);
        }
    }

    private async Task<decimal> FetchExchangeRateAsync()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri =
                new Uri(
                    "https://currency-conversion-and-exchange-rates.p.rapidapi.com/convert?from=USD&to=TRY&amount=1"),
            Headers =
            {
                { "X-RapidAPI-Key", "e437cf0835msh87e8b9175c03851p1bc381jsnb16d88b1b8e2" },
                { "X-RapidAPI-Host", "currency-conversion-and-exchange-rates.p.rapidapi.com" },
            },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();

            var result = ExtractResultFromJson(jsonString);

            return result;
        }
    }
    private async Task<decimal> FetchExchangeRateEuroAsync()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri =
                new Uri(
                    "https://currency-conversion-and-exchange-rates.p.rapidapi.com/convert?from=EUR&to=TRY&amount=1"),
            Headers =
            {
                { "X-RapidAPI-Key", "e437cf0835msh87e8b9175c03851p1bc381jsnb16d88b1b8e2" },
                { "X-RapidAPI-Host", "currency-conversion-and-exchange-rates.p.rapidapi.com" },
            },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();

            var result = ExtractResultFromJson(jsonString);

            return result;
        }
    }

    private decimal ExtractResultFromJson(string jsonString)
    {
        using (JsonDocument document = JsonDocument.Parse(jsonString))
        {
            var root = document.RootElement;
            var result = root.GetProperty("result").GetDecimal();
            return result;
        }
    }

    public void RemoveExchangeRate(string currencyCode)
    {
        var rateToRemove = SharedData._exchangeRates.FirstOrDefault(rate => rate.CurrencyCode == currencyCode);
        if (rateToRemove != null)
        {
            SharedData._exchangeRates.Remove(rateToRemove);
        }
    }
}