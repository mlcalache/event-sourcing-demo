using EventSourcingDemo.Repositories;
using MediatR;

namespace EventSourcingDemo.Queries.Product;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Aggregates.Product.Product>
{
    private readonly IProductRepository _repository;

    public GetProductQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Aggregates.Product.Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var p = await _repository.GetByIdAsync(request.ProductId);
        return p;
    }
}
