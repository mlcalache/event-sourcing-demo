using EventSourcingDemo.Events;

namespace EventSourcingDemo.Events.Product;

public class ProductCreated : Event
{
    public Guid ProductId { get; }
    public string Model { get; }
    public string Brand { get; }

    public ProductCreated(Guid productId, string model, string brand)
    {
        ProductId = productId;
        Model = model;
        Brand = brand;
    }
}