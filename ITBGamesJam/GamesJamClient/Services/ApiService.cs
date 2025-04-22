using System.Net.Http;
using System.Net.Http.Headers;
using GamesJamClient.Models;

namespace GamesJamClient.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Game>> GetTopGamesAsync()
        {
            var response = await _httpClient.GetAsync("api/Games");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Game>>();
        }

        public async Task<Game> GetGameDetailsAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Games/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Game>();
        }

        public async Task<bool> VoteForGameAsync(int gameId)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["authToken"];
            if (string.IsNullOrEmpty(token))
                return false;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync("api/Votes", new { gameId });
            return response.IsSuccessStatusCode;
        }
    }
}
