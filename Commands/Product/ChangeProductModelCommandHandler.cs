
using EventSourcingDemo.Events.Product;
using MediatR;

namespace EventSourcingDemo.Commands.Product;

public class ChangeProductModelCommandHandler : INotificationHandler<ChangeProductModelCommand>
{
    private readonly IProductEventStore _store;

    public ChangeProductModelCommandHandler(IProductEventStore store)
    {
        _store = store;
    }

    public Task Handle(ChangeProductModelCommand command, CancellationToken cancellationToken)
    {
        var product = new Aggregates.Product.Product();

        product.ChangeModel(command.Model);

        _store.Save(command.ProductId, product.GetUncommittedChanges());

        product.MarkChangesAsCommitted();

        Console.WriteLine($"Details of product: model {product.Model}");

        return Task.CompletedTask;
    }
}
