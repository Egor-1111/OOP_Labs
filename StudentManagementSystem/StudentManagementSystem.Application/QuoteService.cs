using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using StudentManagementSystem.Application.DTOs;

namespace StudentManagementSystem.Application
{
    public class QuoteService : IQuoteService
    {
        private readonly HttpClient _httpClient;

        public QuoteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.quotable.io/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
        }

        public async Task<QuoteDto> GetMotivationalQuote()
        {
            try
            {
                // Пробуем основное API
                var response = await _httpClient.GetAsync("random");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                return new QuoteDto
                {
                    Content = root.GetProperty("content").GetString(),
                    Author = root.GetProperty("author").GetString()
                };
            }
            catch
            {
                // Если основное API не работает, пробуем резервное
                return await GetBackupQuote() ?? GetFallbackQuote();
            }
        }

        private async Task<QuoteDto?> GetBackupQuote()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://zenquotes.io/api/random");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement.EnumerateArray().First();

                return new QuoteDto
                {
                    Content = root.GetProperty("q").GetString(),
                    Author = root.GetProperty("a").GetString()
                };
            }
            catch
            {
                return null;
            }
        }

        private QuoteDto GetFallbackQuote()
        {
            return new QuoteDto
            {
                Content = "The expert in anything was once a beginner.",
                Author = "Helen Hayes"
            };
        }
    }
}