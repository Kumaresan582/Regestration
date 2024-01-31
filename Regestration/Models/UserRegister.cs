using System.ComponentModel.DataAnnotations;

namespace Regestration.Models
{
    public class UserRegister
    {
        [Key]
        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is Required")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Enter a valid email.")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public System.DateTime CreatedTime { get; set; }
        public Nullable<System.DateTime> LastLoggedOn { get; set; }
    }
}