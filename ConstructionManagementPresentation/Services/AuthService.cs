using ConstructionManagementPresentation.Dtos;
using ConstructionManagementPresentation.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ConstructionManagementPresentation.Services
{
    public class AuthService

    {
        private readonly HttpClient _httpClient;

    
        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<bool> Register(RegisterDto registerDto)
        {
            var response = await _httpClient.PostAsync("auth/register",
                new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode; 
        }

        public async Task<TokenResponse> Login(LoginDto loginDto)
        {
            var response = await _httpClient.PostAsync("auth/login",
              new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var tokenResponseJson = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(tokenResponseJson);

        
                if (!string.IsNullOrEmpty(tokenResponse.Token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", tokenResponse.Token);
                }

                return tokenResponse;
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Login failed: {errorContent}"); 
            return null;
        }
    }
}
