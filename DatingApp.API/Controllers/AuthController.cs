namespace DatingApp.API.Controllers
{
    using DatingApp.API.Dtos;
    using DatingApp.Data.Models;
    using DatingApp.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class AuthController : BaseController
    {
        private readonly IAuthService auth;
        private readonly IConfiguration config;

        public AuthController(IAuthService auth, IConfiguration config)
        {
            this.auth = auth;
            this.config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userData.Username = userData.Username.ToLower();

            if (await auth.IsUserExist(userData.Username))
            {
                ModelState.AddModelError("Username", "Username is already taken.");
            }

            var createdUser = auth.Register(userData.Username, userData.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userData)
        {
            var currentUser = await this.auth.Login(userData.Username.ToLower(), userData.Password);

            if (currentUser == null)
            {
                return Unauthorized();
            }

            var tokenString = CreateAuthToken(currentUser);

            return Ok(new { tokenString });
        }

        private string CreateAuthToken(User currentUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.config.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, currentUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, currentUser.Username)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
