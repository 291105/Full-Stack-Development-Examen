using FlightProject.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace FlightProject.Services
{
    public class HotelsApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public HotelsApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string?> GetDestinationIdAsync(string city)
        {
            var url = $"{_config["HotelsApi:BaseUrl"]}/v1/hotels/locations?locale=en-gb&name={Uri.EscapeDataString(city)}";

            using var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Add("X-RapidAPI-Key", _config["HotelsApi:ApiKey"]);
            req.Headers.Add("X-RapidAPI-Host", _config["HotelsApi:Host"]);

            using var res = await _httpClient.SendAsync(req);
            res.EnsureSuccessStatusCode();

            using var doc = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
            var root = doc.RootElement;

            if (root.GetArrayLength() > 0)
            {
                return root[0].GetProperty("dest_id").GetString();
            }

            return null;
        }
        public async Task<List<HotelSearchResult>> SearchHotelsAsync(string city)
        {
            var destId = await GetDestinationIdAsync(city);
            if (string.IsNullOrEmpty(destId)) return new();

            var checkIn = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
            var checkOut = DateTime.Today.AddDays(2).ToString("yyyy-MM-dd");

            var url = $"{_config["HotelsApi:BaseUrl"]}/v1/hotels/search" +
                      $"?dest_id={destId}" +
                      $"&dest_type=city" +
                      $"&checkin_date={checkIn}" +
                      $"&checkout_date={checkOut}" +
                      $"&adults_number=2" +
                      $"&room_number=1" +
                      $"&children_number=2" +
                      $"&children_ages=5,0" +
                      $"&order_by=popularity" +
                      $"&locale=en-gb" +
                      $"&units=metric" +
                      $"&filter_by_currency=AED";

            using var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Add("X-RapidAPI-Key", _config["HotelsApi:ApiKey"]);
            req.Headers.Add("X-RapidAPI-Host", _config["HotelsApi:Host"]);

            using var res = await _httpClient.SendAsync(req);
            res.EnsureSuccessStatusCode(); // Error thrown here if 422

            using var doc = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
            var list = new List<HotelSearchResult>();

            foreach (var prop in doc.RootElement.GetProperty("result").EnumerateArray())
            {
                list.Add(new HotelSearchResult
                {
                    Name = prop.GetProperty("hotel_name").GetString() ?? "",
                    Address = prop.GetProperty("address").GetString() ?? "",
                    StarRating = prop.GetProperty("class").GetDouble(),
                    HotelUrl = prop.GetProperty("url").GetString() ?? ""
                });
            }

            return list;
        }


    }
}
