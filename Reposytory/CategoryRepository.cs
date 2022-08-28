using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Reposytory
{
    public class CategoryRepository : ICategory
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webhost;

        public CategoryRepository(ApplicationDbContext context, IWebHostEnvironment webhost)
        {
            _context = context;
            _webhost = webhost;
        }

        public void CreateCategory(CreateViewModel model)
        {
            var result = _context.Categories.FirstOrDefault(c => c.Name == model.Name);

            if(result == null)
            {
                string titleImage = null;
                if (model.TitleImg != null)
                {
                    titleImage = UploadImage(model.TitleImg);
                }

                var category = new Category()
                {
                    Name = model.Name,
                    Description = model.Description,
                    ImgUrl = titleImage
                };
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
        }

        public void EditCategory(Category category)
        {
            if (category != null)
            {
                var result = _context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
                if (result != null)
                {
                    result.Name = category.Name;
                    result.Description = category.Description;
                    if (category.TitleImg != null)
                    {
                        var titleImage = UploadImage(category.TitleImg);
                        result.ImgUrl = titleImage;
                    }
                    _context.Categories.Update(result);
                    _context.SaveChanges();
                }
            }
        }

        public void RemoveCategory(Guid id)
        {
            var categoryToDelete = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            _context.Remove(categoryToDelete);
            _context.SaveChanges();
        }

        public void RemoveProductFromCategory(Guid categoryId, Guid productId)
        {
            var productToDelete = _context.CategoryProducts
                .FirstOrDefault(cp => cp.ProductId == productId && cp.CategoryId == categoryId);
            if (productToDelete != null)
            {
                _context.CategoryProducts.Remove(productToDelete);
                _context.SaveChanges();
            }
        }

        public Category GetCategoryById(Guid id)
        {
            return _context.Categories
                        .Include(cp => cp.CategoryProducts)
                        .ThenInclude(p => p.Product)
                        .FirstOrDefault(c => c.CategoryId == id);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories
                        .Include(cp => cp.CategoryProducts)
                        .ThenInclude(p => p.Product);
        }

        public EditViewModel GetCategoryViewModelById(Guid id)
        {
            var result = GetCategoryById(id);
            if (result == null) 
                return null;

            EditViewModel editViewModel = new EditViewModel()
            {
                ModelId = id,
                Name = result.Name,
                Description = result.Description
            };

            return editViewModel;
        }

        public List<SelectListItem> GetListCategories()
        {
            var categories = _context.Categories.OrderBy(c => c.Name).ToList()
                .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.Name }).ToList();
            return categories;
        }
        private string UploadImage(IFormFile file)
        {
            string fileName = null;
            if (file != null)
            {
                string uploadsFolder = Path.Combine(_webhost.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return fileName;
        }
    }
}

