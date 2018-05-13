namespace DatingApp.API.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class UserForRegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 50 characters long.")]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 8 characters long.")]
        public string Password { get; set; }
    }
}
