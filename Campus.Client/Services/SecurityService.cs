using Blazored.SessionStorage;
using Campus.Client.Services.Interfaces;

namespace Campus.Client.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly HttpClient _httpClient;

        public SecurityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get antiforgery token
        public async Task<string> GetAntiforgeryToken()
        {
            var tokenRequest = await _httpClient.GetAsync("antiforgery/token");
            if (!tokenRequest.IsSuccessStatusCode)
            {
                return "Unable to retrieve antiforgery token.";
            }

            return await tokenRequest.Content.ReadAsStringAsync();
        }

        //encrpt password
        public string GetHashedPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
