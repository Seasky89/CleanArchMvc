using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetProductsAsync();
    Task<ProductDTO> GetByIdAsync(int? id);
    //Task<ProductDTO> GetProductCategoryAsync(int? id);

    Task CreateAsync(ProductDTO productDTO);
    Task UpdateAsync(ProductDTO productDTO);
    Task DeleteAsync(int? id);
}
