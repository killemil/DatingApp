namespace DatingApp.API.Controllers
{
    using DatingApp.API.Infrastructure.Extensions;
    using DatingApp.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using static WebConstants;

    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserService users;

        public UsersController(IUserService users)
        {
            this.users = users;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await this.users.GetUsers();

            return this.Ok(users);
        }

        [HttpGet(WithId)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await this.users.GetUser(id);

            return this.OkOrNotFound(user);
        }
    }
}
