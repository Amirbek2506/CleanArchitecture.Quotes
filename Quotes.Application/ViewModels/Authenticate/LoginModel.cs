using System.ComponentModel.DataAnnotations;

namespace Quotes.Application.ViewModels.Authenticate
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Password { get; set; }
    }
}
