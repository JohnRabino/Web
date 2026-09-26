using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Domain;
using Web.Models.Dto;
using Web.Repositories;

namespace Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository; // depends on the abstraction

        public ProductService(IProductRepository repository) => _repository = repository;

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.StockQuantity));
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var p = await _repository.GetByIdAsync(id);
            return p is null ? null : new ProductDto(p.Id, p.Name, p.Price, p.StockQuantity) { Description = p.Description };
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
                throw new ArgumentException("Name is required.");
            if (input.Price < 0)
                throw new ArgumentException("Price cannot be negative.");
            if (input.StockQuantity < 0)
                throw new ArgumentException("StockQuantity cannot be negative.");

            var entity = new Product
            {
                Name = input.Name,
                Description = input.Description,
                Price = input.Price,
                StockQuantity = input.StockQuantity
            };

            var saved = await _repository.AddAsync(entity);
            return new ProductDto(saved.Id, saved.Name, saved.Price, saved.StockQuantity) { Description = saved.Description };
        }

        public async Task<bool> UpdateAsync(ProductDto product)
        {
            var entity = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}