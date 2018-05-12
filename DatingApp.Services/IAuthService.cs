namespace DatingApp.Services
{
    using DatingApp.Data.Models;
    using System.Threading.Tasks;

    public interface IAuthService
    {
        Task<User> Register(User user, string password);

        Task<User> Login(string username, string password);

        Task<bool> IsUserExist(string username);
    }
}
