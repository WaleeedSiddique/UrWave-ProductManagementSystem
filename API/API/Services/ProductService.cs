using API.Contracts;
using API.Entities;
using API.Interfaces;
using API.Repositories.Interfaces;
using API.Response;
using AutoMapper;

namespace API.Services
{
    public class ProductService(IProductRepository repo, IMapper mapper) : IProductService
    {
        private readonly IProductRepository _repo = repo;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<ProductDto>> CreateAsync(CreateProductDto request)
        {
            var result = await _repo.CreateProduct(_mapper.Map<Product>(request));
            await _repo.SaveAsync();
            return new Response<ProductDto>(_mapper.Map<ProductDto>(result), "Created Successfully.");
        }

        public async Task<Response<List<ProductDto>>> GetAllAsync()
        {
            var result = await _repo.GetAll();
            if (!result.Any())
                return new Response<List<ProductDto>>("List is Empty.");
            return new Response<List<ProductDto>>(_mapper.Map<List<ProductDto>>(result), "Returning List.");
        }

        public async Task<Response<ProductDto>> GetById(int id)
        {
            var result = await _repo.GetById(id);
            if (result == null)
                return new Response<ProductDto>("Product Not Found.");
            return new Response<ProductDto>(_mapper.Map<ProductDto>(result), "Returning Item.");
        }

        public async Task<Response<ProductDto>> UpdateAsync(int id, CreateProductDto request)
        {
            var result = await _repo.GetById(id);
            if (result == null)
                return new Response<ProductDto>($"Product With Id {id} Could not be found.");
            _mapper.Map(request, result);
            await _repo.SaveAsync();
            return new Response<ProductDto>(_mapper.Map<ProductDto>(result), "Updated Successfully.");
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var result = await _repo.GetById(id);
            if (result == null)
                return new Response<bool>($"Product With Id {id} Could not be found.");
            result.SetIsActive(false);
            await _repo.SaveAsync();
            return new Response<bool>(true, "Deleted Successfully.");
        }
    }
}
