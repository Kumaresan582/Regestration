using System.ComponentModel.DataAnnotations;

namespace Regestration.Models
{
    public class LoginModel
    {
        [EmailAddress]
        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Enter a valid email.")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}