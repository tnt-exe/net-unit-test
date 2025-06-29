using WebApi.Models;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Interfaces;

namespace WebApi.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Product> CreateProductAsync(Product product)
        => await _productRepository.CreateAsync(product);

    public async Task<bool> DeleteProductAsync(int id)
        => await _productRepository.DeleteAsync(id);

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
        => await _productRepository.GetAllAsync();

    public async Task<Product?> GetProductByIdAsync(int id)
        => await _productRepository.GetByIdAsync(id);

    public async Task<Product?> UpdateProductAsync(int id, Product product)
        => await _productRepository.UpdateAsync(id, product);
}
