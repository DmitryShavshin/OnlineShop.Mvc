using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication2.Models
{
    public class UserHomeAddress
    {
        [Key]
        public Guid HomeAddressId { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Street { get; set; }
        public int? PostalCode { get; set; }

        [ForeignKey("ApplicationUser")]
        public Guid UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
