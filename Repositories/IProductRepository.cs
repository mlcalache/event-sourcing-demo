using EventSourcingDemo.Aggregates.Product;

namespace EventSourcingDemo.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    // Task SaveAllAsync(List<Product> products);
}
