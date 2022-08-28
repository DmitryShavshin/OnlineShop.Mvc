using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Data;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Areas.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProduct _product;
        private readonly ICategory _category;
        private readonly IBrands _brand;

        public AdminController(ApplicationDbContext context, IProduct product, ICategory category,
            IBrands brand)
        {
            _context = context;
            _product = product;
            _category = category;
            _brand = brand;
        }

        // GET: Products
        public async Task<IActionResult> Index(string? sortOrder, string? searchString)
        {
            ProductViewModel model = new ProductViewModel();
            model = _product.GetProductsVM();
            return View(model);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var product = _product.Details(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        //GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();
            ViewBag.Categories = _category.GetListCategories();
            var product = _product.Update(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel model)
        {
            _product.Update(id, model);
            return RedirectToAction(nameof(Index), nameof(Admin));
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var product = _product.Delete(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        public async Task<IActionResult> AddCategory(Guid? id)
        {
            ViewBag.Categories = _category.GetListCategories();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.Categories = _category.GetListCategories();
            ViewBag.Brands = _brand.GetListBrands();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _category.GetListCategories();
                ViewBag.Brands = _brand.GetListBrands();
                return View(model);
            }


            _product.Create(model);
            return Redirect(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CreateBrand()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _brand.CreateBrand(model);
            return Redirect(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _category.CreateCategory(model);
            return Redirect(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(Guid id)
        {
            var result = _category.GetCategoryById(id);

            if (result == null)
                return Redirect(nameof(Index));

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category)
        {
            if (ModelState.IsValid) 
            {
                _category.EditCategory(category);
                var result = _product.GetProductsVM();
                return View("Index", result);
            }
                return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> EditBrand(Guid id)
        {
            var result = _brand.GetBrandById(id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditBrand(Brand brand)
        {
            if (!ModelState.IsValid)
                return View(brand);

            _brand.EditBrand(brand);

            var result = _product.GetProductsVM();
            return View("Index", result);
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(Guid id)
        {
            var result = _product.GetProductViewModelById(id);
            //var result = _product.GetProduct(id);
            ViewBag.Categories = _category.GetListCategories();
            ViewBag.Brands = _brand.GetListBrands();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _category.GetListCategories();
                ViewBag.Brands = _brand.GetListBrands();
                return View(model);
            }

            _product.EditProduct(model);
            var result = _product.GetProductsVM();
            return View("Index", result);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            var product = _product.ProductDetails(id);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = _product.Details(id);
            return View(product);
        }

        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _product.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CategoryDetails(Guid id)
        {
            var category = _category.GetCategoryById(id);
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = _category.GetCategoryById(id);
            return View(category);
        }

        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategoryConfirmed(Guid id)
        {
            _category.RemoveCategory(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> BrandDetails(Guid id)
        {
            var brand = _brand.GetBrandById(id);
            return View(brand);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            var category = _brand.GetBrandById(id);
            return View(category);
        }

        [HttpPost, ActionName("DeleteBrand")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBrandConfirmed(Guid id)
        {
            _brand.RemoveBrand(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<RedirectToActionResult> RemoveProductFromCategory(Guid categoryId, Guid productId)
        {
            _category.RemoveProductFromCategory(categoryId, productId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveBrandFromProduct(Guid productId)
        {
            _brand.RemoveBrandFromProduct(productId);
            return RedirectToAction(nameof(Index));
        }
    }
}