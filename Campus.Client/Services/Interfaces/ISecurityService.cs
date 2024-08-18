namespace Campus.Client.Services.Interfaces
{
    public interface ISecurityService
    {
        string GetHashedPassword(string password);
    }
}
