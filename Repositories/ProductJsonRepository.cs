using System.Text.Json;
using EventSourcingDemo.Aggregates.Product;
using EventSourcingDemo.Events.Product;

namespace EventSourcingDemo.Repositories;

public class ProductJsonRepository : IProductRepository
{
    private readonly string _filePath;
    private readonly IProductEventStore _eventStore;

    public ProductJsonRepository(IProductEventStore eventStore)
    {
        _filePath = "data/products.json";
        _eventStore = eventStore;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        List<Product> products = new List<Product>();

        if (!File.Exists(_filePath))
            return products;

        try {
            var json = await File.ReadAllTextAsync(_filePath);
            products = JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }
        catch (Exception ex) {
            Console.Write(ex.ToString());
        }
        
        return products;
    }

    public async Task<Product?> GetByIdAsync(Guid productId)
    {
        var p = ProductFactory.Restore(_eventStore, productId);
        return p;
    }

    public async Task SaveAsync(Product product)
    {
        var changes = product.GetUncommittedChanges().ToList();

        if (!changes.Any())
            return;

        _eventStore.Save(product.Id, changes);

        // Optional: Save snapshot every N events
        if (product.Version % 5 == 0) // Example snapshot strategy
        {
            var snapshot = new ProductSnapshot
            {
                Id = product.Id,
                Brand = product.Brand,
                Model = product.Model,
                Price = product.Price,
                Version = product.Version
            };
            _eventStore.SaveSnapshot(snapshot);
        }

        product.MarkChangesAsCommitted();

        await Task.CompletedTask; // for interface consistency
    }

    // public async Task SaveAllAsync(List<Product> products)
    // {
    //     var json = JsonSerializer.Serialize(products, new JsonSerializerOptions
    //     {
    //         WriteIndented = true
    //     });

    //     await File.WriteAllTextAsync(_filePath, json);
    // }
}
