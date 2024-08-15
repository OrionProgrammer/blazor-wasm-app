using Campus.Client.Helpers;
using Campus.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Campus.Client.Pages.Account
{
    public partial class Login : ComponentBase
    {
        [Inject] IAccountService? _accountService { get; set; }
        [Inject] public NavigationManager? navManager { get; set; }
        [Inject] IJSRuntime js { get; set; }
        [Inject] AuthenticationStateProvider? authStateProvider { get; set; }

        public LoginModel? loginModel { get; set; } = new LoginModel();
        public string FullName { get; set; } = "";



        public void Return()
        {
            navManager!.NavigateTo("/");
        }

        public async void HandleSubmit()
        {
            var userSessionModel = await _accountService!.Login(loginModel!);

            if (userSessionModel is { })
            {
                //if login is successful, then let's create a new claims auth profile and save to session.

                var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(userSessionModel);
                navManager.NavigateTo("/", true);
            }
            else
            {
                await js.InvokeVoidAsync("alert", "Invalid User Name or Password");
                return;
            }
        }
    }
}

