using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WebApplication2.Models;


namespace WebApplication2.ViewModels
{
    public class EditProductViewModel
    {
        public Product Product { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? CategoryId { get; set; }
        [ValidateNever]
        public IFormFile TitleImg { get; set; }
        [ValidateNever]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}