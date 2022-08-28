using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Reposytory
{
    public class BrandRepository : IBrands
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webhost;
        public BrandRepository(ApplicationDbContext context, IWebHostEnvironment webhost)
        {
            _context = context;
            _webhost = webhost;
        }

        public void CreateBrand(CreateViewModel model)
        {
            var result = _context.Brands.FirstOrDefault(b => b.Name == model.Name);

            if (result == null)
            {
                string titleImage = "";
                if (model.TitleImg != null)
                    titleImage = UploadImage(model.TitleImg);
                var brand = new Brand()
                {
                    Name = model.Name,
                    Description = model.Description,
                    BrandImgUrl = titleImage
                };
                _context.Brands.Add(brand);
                _context.SaveChanges();
            }
        }

        public void EditBrand(Brand brand)
        {
            if (brand != null)
            {
                var result = _context.Brands.FirstOrDefault(b => b.BrandId == brand.BrandId);
                if (result != null)
                {
                    if (!String.IsNullOrEmpty(result.BrandImgUrl))
                    {
                        var titleImage = UploadImage(brand.TitleImg);
                        result.BrandImgUrl = titleImage;
                    }
                    result.Description = brand.Description;
                    result.Name = brand.Name;
                    _context.Brands.Update(result);
                    _context.SaveChanges();
                }    
            }
        }

        public void RemoveBrandFromProduct(Guid productId)
        {
            Product result = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (result != null)
            {
                result.BrandId = null;
                _context.Products.Update(result);
                _context.SaveChanges();
            }
        }

        public Brand GetBrandById(Guid id)
        {
            var brand = _context.Brands
                    .Include(p => p.Products)
                    .FirstOrDefault(b => b.BrandId == id);  
            return brand;
        }

        public IEnumerable<Brand> GetBrands()
        {
            var brand = _context.Brands;
            return brand;
        }

        public EditViewModel GetBrandViewModelById(Guid id)
        {
            var result = _context.Brands.FirstOrDefault(b => b.BrandId == id);

            if(result == null)
                return null;

            EditViewModel editViewModel = new EditViewModel()
            {
                ModelId = id,
                Name = result.Name,
                Description = result.Description
            };
            return editViewModel;
        }

        public List<SelectListItem> GetListBrands()
        {
            var brands = _context.Brands.OrderBy(b => b.Name).ToList().
                        Select(b => new SelectListItem { Value = b.BrandId.ToString(), Text = b.Name }).ToList();
            return brands;
        }

        public void RemoveBrand(Guid id)
        {
            var brandToDelete = _context.Brands.FirstOrDefault(b => b.BrandId == id);
            _context.Brands.Remove(brandToDelete);
            _context.SaveChanges();
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
