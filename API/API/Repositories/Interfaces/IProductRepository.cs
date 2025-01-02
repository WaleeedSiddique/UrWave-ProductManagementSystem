using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateProduct(Product product);
        Task<Product> GetById(int id);
        Task<List<Product>> GetAll();
        Task SaveAsync();
    }
}
