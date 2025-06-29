using WebApi.Models;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories;

public class ProductRepository : IProductRepository
{
    public Task<Product> CreateAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> UpdateAsync(int id, Product product)
    {
        throw new NotImplementedException();
    }
}
