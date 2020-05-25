using System.ComponentModel.DataAnnotations;

namespace ADEBT.Api.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        [StringLength(256, ErrorMessage = "{0} character limit of {1} exceeded.")]
        public string Email { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "{0} character limit of {1} exceeded.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(75, ErrorMessage = "{0} character limit of {1} exceeded.")]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
