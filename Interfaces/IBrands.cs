using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Interfaces
{
    public interface IBrands
    {
        public Brand GetBrandById(Guid id);
        public void CreateBrand(CreateViewModel brand);
        public IEnumerable<Brand> GetBrands();
        public EditViewModel GetBrandViewModelById(Guid id);
        public List<SelectListItem> GetListBrands();
        public void EditBrand(Brand brand);
        public void RemoveBrand(Guid id);
        public void RemoveBrandFromProduct(Guid productId);

    }
}
