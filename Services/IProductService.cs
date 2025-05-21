using EventSourcingDemo.Aggregates.Product;

public interface IProductService
{
    Task CreateAsync(Guid productId, string model, string brand, decimal price);

    Task<List<Product>> GetAllAsync();

    Task ChangeModelAsync(Guid productId, string model);

    Task ChangeBrandAsync(Guid productId, string brand);

    Task ChangePriceAsync(Guid productId, decimal price);

    Task<Product> GetAsync(Guid productId);
}