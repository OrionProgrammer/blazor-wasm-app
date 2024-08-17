
using System;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using Campus.Client.Services.Interfaces;
using Campus.Model;
using Newtonsoft.Json;

namespace Campus.Client.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly ISecurityService _securityService;
        public AccountService(HttpClient httpClient,
                              ISecurityService securityService)
        {
            _httpClient = httpClient;
            _securityService = securityService;
        }

        //Method inlined to be faster
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private async void AddAntiforgeryToken()
        {
            _httpClient.DefaultRequestHeaders.Add("X-CSRF-TOKEN", await _securityService.GetAntiforgeryToken());
        }

        public async Task<UserSessionModel> Login(LoginModel loginModel)
        {
            AddAntiforgeryToken();

            var response = await _httpClient.PostAsJsonAsync("account/login", loginModel);
            var returnValue = await response.Content.ReadAsStringAsync();
            UserSessionModel studentModel = JsonConvert.DeserializeObject<UserSessionModel>(returnValue);

            return studentModel;
        }

        public async Task<bool> Register(StudentModel studentModel)
        {
            AddAntiforgeryToken();

            var response = await _httpClient.PostAsJsonAsync("account/register", studentModel);

            return response.IsSuccessStatusCode;
        }
    }
}
