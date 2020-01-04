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
    public class AuthService : IAuthService
    {
        private readonly string _apiBaseUrl = "http://localhost:5051";
        private readonly IHttpClientFactory _httpClientFactory;
        private string _token;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task LoginAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_apiBaseUrl + "/login");

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            _token = JsonSerializer.Deserialize<string>(responseBody);
        }

        public async Task<string> GetTokenAsync()
        {
            if (string.IsNullOrWhiteSpace(_token)) await LoginAsync();

            return _token;
        }
    }
}