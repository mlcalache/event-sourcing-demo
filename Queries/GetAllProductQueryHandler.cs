
using EventSourcingDemo.Events.Product;
using EventSourcingDemo.Repositories;
using MediatR;

namespace EventSourcingDemo.Queries.Product;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<Aggregates.Product.Product>>
{
    private readonly IProductRepository _repository;

    public GetAllProductQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Aggregates.Product.Product>> Handle(GetAllProductQuery query, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
