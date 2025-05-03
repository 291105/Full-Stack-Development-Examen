using FlightProject.Domain.Data;
using FlightProject.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace FlightProject.Services
{
    // Serviceklasse voor communicatie met de externe Hotels API
    public class HotelsApiService : IHotelsApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        // Constructor die HttpClient en configuratie injecteert
        public HotelsApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        // Haalt een lijst van hotels op op basis van een opgegeven stad
        public async Task<List<Hotel>> GetHotelsAsync(string city)
        {
            // Ophalen van API-configuratiegegevens uit appsettings
            var apiKey = _configuration["HotelsApi:ApiKey"];
            var apiHost = _configuration["HotelsApi:Host"];
            var baseUrl = _configuration["HotelsApi:BaseUrl"];

            try
            {
                // Ophalen van de bestemming-ID op basis van de stad
                var destId = await GetDestinationIdAsync(city, apiKey, apiHost, baseUrl);
                if (string.IsNullOrEmpty(destId))
                {
                    throw new Exception("Bestemmings-ID niet gevonden.");
                }

                // Instellen van check-in en check-out data
                var checkIn = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                var checkOut = DateTime.Today.AddDays(2).ToString("yyyy-MM-dd");

                // Opbouwen van de URL voor het zoeken naar hotels
                var url = $"{baseUrl}/v1/hotels/search" +
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

                // Opstellen van de HTTP-aanvraag
                using var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("X-RapidAPI-Key", apiKey);
                request.Headers.Add("X-RapidAPI-Host", apiHost);

                // Verzenden aanvraag
                using var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // Inlezen van JSON
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(jsonString);
                var hotels = new List<Hotel>();

                // Uitlezen van de hotelgegevens uit het JSON-document
                foreach (var item in jsonDoc.RootElement.GetProperty("result").EnumerateArray())
                {
                    hotels.Add(new Hotel
                    {
                        Name = item.GetProperty("hotel_name").GetString() ?? "",
                        Address = item.GetProperty("address").GetString() ?? "",
                        StarRating = item.GetProperty("class").GetDouble(),
                        HotelUrl = item.GetProperty("url").GetString() ?? ""
                    });
                }

                return hotels;
            }
            catch (Exception ex)
            {
                // Foutafhandeling en logging
                Console.WriteLine($"Fout bij ophalen hotels: {ex.Message}");
                throw;
            }
        }

        // Haalt de bestemming-ID op aan de hand van de stad
        private async Task<string?> GetDestinationIdAsync(string city, string apiKey, string apiHost, string baseUrl)
        {
            var url = $"{baseUrl}/v1/hotels/locations?locale=en-gb&name={Uri.EscapeDataString(city)}";

            // HTTP aanvraag met aangepaste headers
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("X-RapidAPI-Key", apiKey);
            request.Headers.Add("X-RapidAPI-Host", apiHost);

            // Verzenden van de aanvraag en controleren op succesvolle status
            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // Uitlezen van de bestemmings-ID
            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(jsonString);
            var root = jsonDoc.RootElement;

            // Teruggeven van eerst gevonden bestemmings-ID (indien beschikbaar)
            if (root.GetArrayLength() > 0)
            {
                return root[0].GetProperty("dest_id").GetString();
            }

            // Geen bestemming gevonden
            return null;
        }
    }
}
