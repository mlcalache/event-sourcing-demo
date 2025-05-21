using MediatR;

namespace EventSourcingDemo.Queries.Product;

public class GetProductQuery : IRequest<Aggregates.Product.Product>
{
    public Guid ProductId { get; }

    public GetProductQuery(Guid productId)
    {
        ProductId = productId;
    }
}