namespace DatingApp.Services
{
    using Data.Models;
    using System.Threading.Tasks;

    public interface IAuthService
    {
        Task<User> Register(string username, string password);

        Task<User> Login(string username, string password);

        Task<bool> IsUserExist(string username);
    }
}
