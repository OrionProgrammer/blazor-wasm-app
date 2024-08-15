using Campus.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Campus.Client.Pages.Account
{
    public partial class Create : ComponentBase
    {
        [Inject]
        IAccountService? _accountService { get; set; }

        [Inject] 
        public NavigationManager? navManager { get; set; }

        public StudentModel? studentModel { get; set; } = new StudentModel();

        public bool IsNotRegistered { get; set; } = true;

        public void Return()
        {
            navManager!.NavigateTo("/");
        }

        public async void HandleSubmit()
        {
            await _accountService!.Register(studentModel!);

            IsNotRegistered = false;
        }
    }
}
