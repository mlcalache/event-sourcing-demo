using EventSourcingDemo.Events;

namespace EventSourcingDemo.Events.Product;

public class ProductPriceChanged : Event
{
    public decimal Price { get; }

    public ProductPriceChanged(decimal price)
    {
        Price = price;
    }
}