
using EventSourcingDemo.Events.Product;
using MediatR;

namespace EventSourcingDemo.Commands.Product;

public class CreateProductCommandHandler : INotificationHandler<CreateProductCommand>
{
    private readonly IProductEventStore _store;

    public CreateProductCommandHandler(IProductEventStore store)
    {
        _store = store;
    }

    public Task Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Aggregates.Product.Product();

        product.Create(command.ProductId, command.Model, command.Brand, command.Price);

        _store.Save(command.ProductId, product.GetUncommittedChanges());

        product.MarkChangesAsCommitted();

        Console.WriteLine($"Details of product: model {product.Model}, brand {product.Brand}, price {product.Price}");

        return Task.CompletedTask;
    }
}
