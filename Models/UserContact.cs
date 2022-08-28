using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication2.Models
{
    public class UserContact
    {
        [Key]
        public Guid UserContactId { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Linkedin { get; set; }
        public string? BirthDate { get; set; }
        public string? Gender { get; set; }

        [ForeignKey("ApplicationUser")]
        public Guid UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
