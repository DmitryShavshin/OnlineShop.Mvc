using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Interfaces
{
    public interface IProduct
    {
        public IEnumerable<Product> GetProducts();
        public ProductViewModel GetProductsVM();
        public Product GetProduct(Guid? id);
        public EditProductViewModel GetProductViewModelById(Guid id);
        public ProductViewModel Create(CreateProductViewModel model);
        public ProductViewModel Update(Guid? id);
        public void Update(Guid id, ProductViewModel model);
        public Product Delete(Guid? id);
        public Product Details(Guid? id);
        public void Remove(Guid id);
        public Product ProductDetails(Guid id);
        public void EditProduct(EditProductViewModel model);
    }
}
