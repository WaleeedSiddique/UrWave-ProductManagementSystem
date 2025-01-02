using API.Contracts;
using API.Response;

namespace API.Interfaces
{
    public interface IProductService
    {
        Task<Response<ProductDto>> CreateAsync(CreateProductDto request);
        Task<Response<ProductDto>> UpdateAsync(int id,CreateProductDto request);
        Task<Response<ProductDto>> GetById(int id);
        Task<Response<List<ProductDto>>> GetAllAsync();
        Task<Response<bool>> DeleteAsync(int id);
    }
}
