namespace DatingApp.API.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class UserForLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
