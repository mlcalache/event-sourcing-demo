
using EventSourcingDemo.Events.Product;
using MediatR;

namespace EventSourcingDemo.Commands.Product;

public class ChangeProductBrandCommandHandler : INotificationHandler<ChangeProductBrandCommand>
{
    private readonly IProductEventStore _store;

    public ChangeProductBrandCommandHandler(IProductEventStore store)
    {
        _store = store;
    }

    public Task Handle(ChangeProductBrandCommand command, CancellationToken cancellationToken)
    {
        var product = new Aggregates.Product.Product();

        product.ChangeBrand(command.Brand);

        _store.Save(command.ProductId, product.GetUncommittedChanges());

        product.MarkChangesAsCommitted();

        Console.WriteLine($"Details of product: Brand {product.Brand}");

        return Task.CompletedTask;
    }
}
