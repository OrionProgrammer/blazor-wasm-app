using Campus.Model;

namespace Campus.Client.Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserSessionModel> Login(LoginModel loginModel);
        Task<bool> Register(StudentModel studentModel);
        Task<bool> IsLogedIn();
        Task<UserSessionModel> GetUserSessionModel();
        Task<string> GetUserJWTToken();
    }
}
