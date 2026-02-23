using Microsoft.Extensions.Options;
using NasaPhoto_WinApp.Application.Interfaces;
using NasaPhoto_WinApp.Domain.Entities;
using NasaPhoto_WinApp.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NasaPhoto_WinApp.Infrastructure.Repositories
{
    public class ApodRepository : IApodRepository
    {
        private readonly HttpClient _httpClient;
        private readonly NasaApiSettings _settings;

        public ApodRepository(IOptions<NasaApiSettings> options)
        {
            _settings = options.Value;
            _httpClient = new HttpClient();
        }

        public async Task<List<Apod>> GetApodsAsync(DateTime startDate, DateTime endDate)
        {
            var url =
                $"{_settings.BaseUrl}?api_key={_settings.ApiKey}" +
                $"&start_date={startDate:yyyy-MM-dd}" +
                $"&end_date={endDate:yyyy-MM-dd}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var items = JsonSerializer.Deserialize<List<ApodDto>>(json, options);

            return items!
                .Select(x => new Apod(
                    DateTime.Parse(x.date),
                    x.title,
                    x.explanation,
                    x.url,
                    x.hdurl,
                    x.media_type,
                    x.copyright,
                    x.service_version))
                .ToList();
        }

        private class ApodDto
        {
            public string date { get; set; }
            public string title { get; set; }
            public string explanation { get; set; }
            public string url { get; set; }
            public string hdurl { get; set; }
            public string media_type { get; set; }
            public string? copyright { get; set; }
            public string service_version { get; set; }
        }
    }
}
