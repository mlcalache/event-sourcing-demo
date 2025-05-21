namespace EventSourcingDemo.Events.Product;

public class ProductCreated : Event
{
    public Guid ProductId { get; }
    public string Model { get; }
    public string Brand { get; }
    public decimal Price { get; }

    public ProductCreated(Guid productId, string model, string brand, decimal price)
    {
        ProductId = productId;
        Model = model;
        Brand = brand;
        Price = price;
    }
}