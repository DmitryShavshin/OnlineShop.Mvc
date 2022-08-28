using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Reposytory
{
    public class ProductRepository : IProduct
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly ApplicationDbContext _context;
        private readonly ICategory _category;
        private readonly IBrands _brands;
        public ProductRepository(ApplicationDbContext context, IWebHostEnvironment webHost,
                ICategory category, IBrands brands)
        {
            _context = context; 
            _webHost = webHost;
            _category = category;
            _brands = brands;
        }

        public ProductViewModel Create(CreateProductViewModel model)
        {
            var product = new Product()
            {
                ProductId = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                BrandId = model.BrandId
            };

            if (model.CategoryId != null)
            {
                var categoryProduct = new CategoryProduct()
                {
                    ProductId = product.ProductId,
                    CategoryId = model.CategoryId
                };
                _context.CategoryProducts.Add(categoryProduct);
            }
           

            string fileName = UploadImage(model.TitleImg);
            product.ImgUrl = fileName;

            foreach (var item in model.Images)
            {
                fileName = UploadImage(item);
                var productImage = new ProductImage()
                {
                    ImageUrl = fileName,
                    ProductId = product.ProductId,
                };
                _context.ProductImages.Add(productImage);
            }
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Added;
            _context.Add(product);
            _context.SaveChanges();
            ProductViewModel viewModel = new ProductViewModel() { Product = product };
            return viewModel;
        }

        public Product Delete(Guid? id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
                                
            return product;
        }
   
        public Product Details(Guid? id)
        {
            var product = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.CategoryProducts)
                .ThenInclude(cp => cp.Category)
                .FirstOrDefault(m => m.ProductId == id);
            return product;
        }

      
        public ProductViewModel GetProductsVM()
        {           
            ProductViewModel model = new ProductViewModel();
            model.Categories = _category.GetCategories();
            model.Brands = _brands.GetBrands();
            model.Products = _context.Products
                    .Include(cp => cp.CategoryProducts)
                    .ThenInclude(c => c.Category)
                    //.Include(pi => pi.ProductImages)
                    .Include(b => b.Brand)
                    .ToList();        
            return model;
        }

      
        public ProductViewModel Update(Guid? id)
        {
            ProductViewModel model = new ProductViewModel();
            model.Product = _context.Products
                .Include(p => p.CategoryProducts)
                .ThenInclude(cp => cp.Category)
                .Include(p => p.Brand)
                .FirstOrDefault(p => p.ProductId == id);
            return model;
        }
     
        public void Update(Guid id, ProductViewModel model)
        {
            model.ProductId = id;
            var product = _context.Products
                .Include(cp => cp.CategoryProducts)
                .ThenInclude(c => c.Category)
                .FirstOrDefault(p => p.ProductId == model.ProductId);
            var catProducts = _context.CategoryProducts
                .FirstOrDefault(cp => cp.ProductId == model.ProductId && cp.CategoryId == model.CategoryId);
            var addCategory = new CategoryProduct
            {
                CategoryId = model.CategoryId,
                ProductId = model.ProductId
            };

            if (addCategory != catProducts)
            {
                _context.CategoryProducts.Add(addCategory);
            }
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void EditProduct(EditProductViewModel model)
        {
            var result = _context.Products
                .Include(b => b.Brand)
                .Include(cp => cp.CategoryProducts)
                .ThenInclude(c => c.Category)
                .FirstOrDefault(p => p.ProductId == model.Product.ProductId);
           
            if (result != null)
            {
                if (model.TitleImg != null)
                {
                    string uploadImage = UploadImage(model.TitleImg);
                    result.ImgUrl = uploadImage;
                }

                if(model.Images != null)
                {
                    foreach (var item in model.Images)
                    {
                        var fileName = UploadImage(item);
                        var productImage = new ProductImage()
                        {
                            ImageUrl = fileName,
                            ProductId = model.Product.ProductId,
                        };
                        _context.ProductImages.Add(productImage);
                    }
                }

                var presenceCheckCategory = _context.CategoryProducts
                    .FirstOrDefault(cp => cp.ProductId == model.Product.ProductId && cp.CategoryId == model.CategoryId);

                if (presenceCheckCategory == null)
                {
                    var categoryProduct = new CategoryProduct
                    {
                        CategoryId = (Guid)model.CategoryId,
                        ProductId = model.Product.ProductId
                    };
                    _context.CategoryProducts.Add(categoryProduct);
                }

                result.BrandId = model.BrandId == result.BrandId ? result.BrandId : model.BrandId;
                result.Name = model.Product.Name;
                result.Description = model.Product.Description;
                result.Price = model.Product.Price;

                _context.Products.Update(result);
                _context.SaveChanges();
            }
        }

        public void Remove(Guid id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public Product ProductDetails(Guid id)
        {
            var product = GetProduct(id);
            return product;
        }


        public IEnumerable<Product> GetProducts()
        {
            return _context.Products
                .Include(b => b.Brand)
                .Include(cp => cp.CategoryProducts)
                .ThenInclude(c => c.Category)
                .ToList();
        }

        public EditProductViewModel GetProductViewModelById(Guid id)
        {
            var product = _context.Products
               .Include(b => b.Brand)
               .Include(cp => cp.CategoryProducts)
               .ThenInclude(c => c.Category)
               .Include(i => i.ProductImages)
               .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return null;

            EditProductViewModel productViewModel = new EditProductViewModel()
            {
                Product = product,
                BrandId = product.BrandId,
            
            };
            var productCategory = _context.CategoryProducts
                    .FirstOrDefault(cp => cp.ProductId == product.ProductId);
            
            if (productCategory != null)
                productViewModel.CategoryId = productCategory.CategoryId;

            return productViewModel;
        }

        public Product GetProduct(Guid? id)
        {
            var product = _context.Products
               .Include(b => b.Brand)
               .Include(cp => cp.CategoryProducts)
               .ThenInclude(c => c.Category)
               .Include(i => i.ProductImages)
               .FirstOrDefault(p => p.ProductId == id);
            
            if (product == null)
                return null;

            return product;
        }

        private string UploadImage(IFormFile file)
        {
            string fileName = null;
            if (file != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
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
