namespace Campus.Client.Services.Interfaces
{
    public interface ISecurityService
    {
        Task<string> GetAntiforgeryToken();

        string GetHashedPassword(string password);
    }
}
