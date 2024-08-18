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

        //encrpt password
        public string GetHashedPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
