using System.ComponentModel.DataAnnotations;

namespace MovieInfo.Api.Controllers.Auth.Dto
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }
        public int Age { get; set; }
    }
}
