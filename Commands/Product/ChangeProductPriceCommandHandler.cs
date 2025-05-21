
using EventSourcingDemo.Events.Product;
using MediatR;

namespace EventSourcingDemo.Commands.Product;

public class ChangeProductPriceCommandHandler : INotificationHandler<ChangeProductPriceCommand>
{
    private readonly IProductEventStore _store;

    public ChangeProductPriceCommandHandler(IProductEventStore store)
    {
        _store = store;
    }

    public Task Handle(ChangeProductPriceCommand command, CancellationToken cancellationToken)
    {
        var product = new Aggregates.Product.Product();

        product.ChangePrice(command.Price);

        _store.Save(command.ProductId, product.GetUncommittedChanges());

        product.MarkChangesAsCommitted();

        Console.WriteLine($"Details of product: price {product.Price}");

        return Task.CompletedTask;
    }
}
