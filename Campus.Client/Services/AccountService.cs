
using System;
using System.Net.Http.Json;
using Campus.Client.Services.Interfaces;
using Campus.Model;
using Newtonsoft.Json;

namespace Campus.Client.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserSessionModel> Login(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("account/login", loginModel);
            var returnValue = await response.Content.ReadAsStringAsync();
            UserSessionModel studentModel = JsonConvert.DeserializeObject<UserSessionModel>(returnValue);

            return studentModel;
        }

        public async Task<bool> Register(StudentModel studentModel)
        {
            var response = await _httpClient.PostAsJsonAsync("account/register", studentModel);

            return response.IsSuccessStatusCode;
        }
    }
}
