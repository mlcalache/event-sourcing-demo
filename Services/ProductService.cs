using MediatR;
using EventSourcingDemo.Commands.Product;
using EventSourcingDemo.Aggregates.Product;
using EventSourcingDemo.Queries.Product;

namespace EventSourcingDemo.Services;

public class ProductService : IProductService
{
    private readonly IMediator _mediator;

    public ProductService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task CreateAsync(Guid productId, string model, string brand, decimal price)
    {
        await _mediator.Publish(new CreateProductCommand(productId, model, brand, price));
    }

    public async Task ChangeModelAsync(Guid productId, string model)
    {
        await _mediator.Publish(new ChangeProductModelCommand(productId, model));
    }

    public async Task ChangeBrandAsync(Guid productId, string brand)
    {
        await _mediator.Publish(new ChangeProductBrandCommand(productId, brand));
    }

    public async Task ChangePriceAsync(Guid productId, decimal price)
    {
        await _mediator.Publish(new ChangeProductPriceCommand(productId, price));
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var products = await _mediator.Send(new GetAllProductQuery());
        return products;
    }

    public async Task<Product> GetAsync(Guid productId)
    {
        var product = await _mediator.Send(new GetProductQuery(productId));
        return product;
    }
}