using System.Net;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Actio.Frontend.Blazor.Data;

namespace Actio.Frontend.Blazor.Services
{
    public class ActivityService : IActivityService
    {
        private readonly string _apiBaseUrl = "http://localhost:5000/activities";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAuthService _authService;

        public ActivityService(IHttpClientFactory httpClientFactory,
            IAuthService authService)
        {
            _httpClientFactory = httpClientFactory;
            _authService = authService;
        }

        public async Task<List<Activity>> GetAsync()
        {
            var client = _httpClientFactory.CreateClient();

            // var token = await _authService.GetTokenAsync();
            // client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var response = await client.GetAsync(_apiBaseUrl);

            response.EnsureSuccessStatusCode();

            //using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Activity>>(responseBody);
        }

        public async Task<Activity> GetAsync(Guid id)
        {
            var client = _httpClientFactory.CreateClient();

            // var token = await _authService.GetTokenAsync();
            // client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var response = await client.GetAsync($"{_apiBaseUrl}/{id}");

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Activity>(responseBody);
        }
    }
}