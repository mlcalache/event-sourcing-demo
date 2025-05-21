using MediatR;

namespace EventSourcingDemo.Commands.Product;

public class CreateProductCommand : INotification
{
    public Guid ProductId { get; }
    public string Model { get; }
    public string Brand { get; }
    public decimal Price { get; }

    public CreateProductCommand(Guid productId, string model, string brand, decimal price)
    {
        ProductId = productId;
        Model = model;
        Brand = brand;
        Price = price;
    }
}