using MediatR;

namespace EventSourcingDemo.Commands.Product;

public class ChangeProductPriceCommand : INotification
{
    public Guid ProductId { get; }

    public decimal Price { get; }

    public ChangeProductPriceCommand(Guid productId, decimal price)
    {
        ProductId = productId;
        Price = price;
    }
}