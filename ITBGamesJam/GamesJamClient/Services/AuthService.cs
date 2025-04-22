namespace GamesJamClient.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _configuration = configuration;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                var loginData = new { email, password };

                // Usa una ruta relativa (se combinará con el BaseAddress)
                var response = await _httpClient.PostAsJsonAsync("Auth/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResult>();
                    // Guarda el token, etc.
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // Log del error
                Console.WriteLine($"Error en login: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RegisterAsync(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/registre", new { email, password});
            return response.IsSuccessStatusCode;
        }
    }


    public class AuthResult
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
