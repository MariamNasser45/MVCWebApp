namespace ProductCatalog.Interfaces
{
    public interface IUserResolverService
    {
        string GetUserId();
        Task<bool> CheckRole(string userId, string roleName);
        Task<string> GetUserName(string userId);

    }
}
