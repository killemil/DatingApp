namespace DatingApp.API.Controllers
{
    using DatingApp.API.Dtos;
    using DatingApp.API.Infrastructure.Extensions;
    using DatingApp.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
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

        [HttpPut(WithId)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserForUpdateDto userData)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await this.users.GetUser(id);

            if (userFromRepo == null)
            {
                return NotFound($"Could not find user with and ID of {id}");
            }

            if (currentUserId != userFromRepo.Id)
            {
                return this.Unauthorized();
            }

            this.users.UpdateUser(id, userData.Interests, userData.LookingFor, userData.Introduction, userData.City, userData.Country);

            return this.NoContent();
        }
    }
}
