using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Application.ViewModels.Authenticate
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Password { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string ConfirmPassword { get; set; }
    }
}
