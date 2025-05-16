using EventSourcingDemo.Events;

namespace EventSourcingDemo.Events.Product;

public class ProductBrandChanged : Event
{
    public string Brand { get; }

    public ProductBrandChanged(string brand)
    {
        Brand = brand;
    }
}