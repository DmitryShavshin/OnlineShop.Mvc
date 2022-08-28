using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class EditViewModel
    {
        [NotMapped]
        public Guid ModelId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [ValidateNever]
        public IEnumerable<Product> Products { get; set; }

    }
}
