using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;

        [Required]
        [Display(Name ="Email")]
        public string Email { get; set; }
        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "password";        
        [NotMapped]
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string? PasswordConfirm { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string? PhoneNumber { get; set; }
        public DateTime DateTimeNow { get; set; }
        public string? ImgUrl { get; set; }
        public IEnumerable<Order> Order { get; set; }
        public UserHomeAddress HomeAddress { get; set; }
        public UserWorkAddress WorkAddress { get; set; }
        public UserContact UserContact { get; set; }


        [NotMapped]
        public IFormFile UserAvatar { get; set; }
    }
}
