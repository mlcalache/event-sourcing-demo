using EventSourcingDemo.Events;

namespace EventSourcingDemo.Events.Product;

public class ProductModelChanged : Event
{
    public string Model { get; }

    public ProductModelChanged(string model)
    {
        Model = model;
    }
}