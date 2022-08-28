using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.Data;
using System.Text;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProduct _product;
        private readonly ICategory _category;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IProduct product, ICategory category, ApplicationDbContext context)
        {
            _logger = logger;
            _product = product;
            _category = category;
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _product.GetProductsVM();
            return View(products);
        }

        [HttpPost]
        public IActionResult Index(string[] searchParam)
        {
            //var model = _product.GetProductsVM();
            //model.Products = from p in model.Products
            //                  //where p.CategoryProducts.Any(c => c.CategoryId == p.ProductId)
            //                  where searchParam.Contains(p.Brand.Name)
            //                  select p;

            ProductViewModel model = new ProductViewModel();
            model.Categories = _context.Categories;
            model.Brands = _context.Brands;
            model.Products = _context.Products
                    .Where(p => searchParam.Contains(p.Brand.Name) && p.CategoryProducts.Any(cp => searchParam.Contains(cp.Category.Name))
                        || searchParam.Contains(p.Brand.Name)
                        || p.CategoryProducts.Any(cp => searchParam.Contains(cp.Category.Name)));


            
            return View(model);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            return View(_product.Details(id));
        }
        [HttpPost]
        public async Task<IActionResult> Details()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _category.GetCategories();
            return View();
        }

        public RedirectToActionResult SortParam(string[] model)
        {
           
            //foreach (var item in model)
            //{
            //    //viewModel.Products = _context.Products.Where(b => b.Brand.Name.Contains(item));
            //    //viewModel.Products = from p in _context.Products
            //    //                     where p.Brand.Name == item
            //    //                     select p;
            //}

          
            
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}