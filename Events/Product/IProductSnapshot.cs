namespace EventSourcingDemo.Events.Product;

public interface IProductSnapshot
{
    Guid Id { get; set; }
    int Version { get; set; }
    string Model { get; set; }
    string Brand { get; set; }
    decimal Price { get; set; }
}
