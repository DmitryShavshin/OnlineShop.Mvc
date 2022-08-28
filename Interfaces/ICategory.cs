using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Interfaces
{
    public interface ICategory
    {
        public Category GetCategoryById(Guid id);
        public IEnumerable<Category> GetCategories();
        public List<SelectListItem> GetListCategories();
        public EditViewModel GetCategoryViewModelById(Guid id);
        public void CreateCategory(CreateViewModel model);
        public void EditCategory(Category category);
        public void RemoveCategory(Guid id);
        public void RemoveProductFromCategory(Guid categoryId, Guid productId);
    }
}
