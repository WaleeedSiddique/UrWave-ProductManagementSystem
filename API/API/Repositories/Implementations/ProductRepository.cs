using API.Context;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class ProductRepository(ApplicationDbContext _context) : IProductRepository
    {
        private readonly ApplicationDbContext _context = _context;

        public async Task<Product> CreateProduct(Product product)
        {
            var result = await _context.Products.AddAsync(product);
            return result.Entity;
        }

        public async Task<List<Product>> GetAll()
        {
            var productList = await _context.Products.Where(x => x.IsActive == true)
                .AsNoTracking()
                .ToListAsync();
            return productList;
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
