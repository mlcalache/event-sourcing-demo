using MediatR;

namespace EventSourcingDemo.Commands.Product;

public class ChangeProductBrandCommand : INotification
{
    public Guid ProductId { get; }

    public string Brand { get; }

    public ChangeProductBrandCommand(Guid productId, string brand)
    {
        ProductId = productId;
        Brand = brand;
    }
}