using Blazored.SessionStorage;
using Campus.API.Helpers;
using Campus.Client;
using Campus.Client.Helpers;
using Campus.Client.Services;
using Campus.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Get API Url from AppSettings
var appSettingsSection = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

builder.Services.AddScoped(sp => new HttpClient 
{
    BaseAddress = new Uri("http://localhost:5292/api/")
});

//add service dependencies
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
