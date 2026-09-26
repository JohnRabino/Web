using Web.Models.Data;
using Web.Models.Domain;
using Web.Repositories;
using Web.Models.Data;
using Web.Models.Domain;
using Web.Repositories;

namespace Chico91226.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<Product> AddAsync(Product product)
        {
            var result = _context.Product.AddAsync(product);
            return result.AsTask().ContinueWith(t => t.Result.Entity);

        }

        public Task<object> AddAsync(object product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            var product = _context.Product.Find(id);
            if (product == null)
            {
                return Task.FromResult(false);
            }
            _context.Product.Remove(product);
            return Task.FromResult(true);

        }
        public Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = _context.Product.ToList();
            return Task.FromResult(products.AsEnumerable());
        }

        public Task GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Product product)
        {
            var existingProduct = _context.Product.Find(product.Id);
            if (existingProduct == null)
            {
                return Task.FromResult(false);
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;
            return Task.FromResult(true);
        }

        public Task<bool> UpdateAsync(object product)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<object>> IProductRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Product> IProductRepository.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}