namespace EventSourcingDemo.Events.Product;

public class ProductSnapshot
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public decimal Price { get; set; }
}
