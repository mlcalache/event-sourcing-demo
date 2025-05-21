using MediatR;

namespace EventSourcingDemo.Commands.Product;

public class ChangeProductModelCommand : INotification
{
    public Guid ProductId { get; }
    public string Model { get; }

    public ChangeProductModelCommand(Guid productId, string model)
    {
        ProductId = productId;
        Model = model;
    }
}