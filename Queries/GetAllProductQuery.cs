using EventSourcingDemo.Aggregates.Product;
using MediatR;

namespace EventSourcingDemo.Queries.Product;

public class GetAllProductQuery : IRequest<List<Aggregates.Product.Product>>
{
    public GetAllProductQuery()
    {
    }
}