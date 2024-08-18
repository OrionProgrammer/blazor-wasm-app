
using System;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using Campus.Client.Services.Interfaces;
using Campus.Model;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Campus.Client.Helpers;

namespace Campus.Client.Services
{
    public class AccountService : IAccountService
    {
        [Inject] AuthenticationStateProvider? authStateProvider { get; set; }

        private readonly HttpClient _httpClient;
        private readonly ISecurityService _securityService;
        
        public AccountService(HttpClient httpClient,
                              ISecurityService securityService)
        {
            _httpClient = httpClient;
            _securityService = securityService;
        }

        //login to account
        public async Task<UserSessionModel> Login(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("account/login", loginModel);
            var returnValue = await response.Content.ReadAsStringAsync();
            UserSessionModel studentModel = JsonConvert.DeserializeObject<UserSessionModel>(returnValue);

            return studentModel;
        }

        //register an account
        public async Task<bool> Register(StudentModel studentModel)
        {
            // Hash the password before sending it to the server
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(studentModel.Password);
            studentModel.Password = hashedPassword;

            var response = await _httpClient.PostAsJsonAsync("account/register", studentModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> IsLogedIn()
        {
            var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
            var authState  =await customAuthStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            bool isLoggedIn = user.Identity != null && user.Identity.IsAuthenticated;

            return isLoggedIn;
        }
    }
}
