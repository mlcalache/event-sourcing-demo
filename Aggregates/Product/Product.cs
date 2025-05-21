using EventSourcingDemo.Events;
using EventSourcingDemo.Events.Product;

namespace EventSourcingDemo.Aggregates.Product;

public class Product
{
    public Guid Id { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public decimal Price { get; set; }
    public int Version { get; set; } = -1;
    private List<IEvent> _changes = new();
    public IEnumerable<IEvent> GetUncommittedChanges() => _changes;
    public void MarkChangesAsCommitted() => _changes.Clear();

    public void ApplySnapshot(ProductSnapshot snapshot)
    {
        Id = snapshot.Id;
        Model = snapshot.Model;
        Brand = snapshot.Brand;
        Price = snapshot.Price;
        Version = snapshot.Version;
    }
    
    public void Create(Guid id, string model, string brand, decimal price)
    {
        ApplyChange(new ProductCreated(id, model, brand, price));
    }

    public void ChangePrice(decimal price)
    {
        ApplyChange(new ProductPriceChanged(price));
    }

    public void ChangeModel(string model)
    {
        ApplyChange(new ProductModelChanged(model));
    }

    public void ChangeBrand(string brand)
    {
        ApplyChange(new ProductBrandChanged(brand));
    }

    private void ApplyChange(IEvent @event)
    {
        @event.Version = ++Version;
        Apply(@event);
        _changes.Add(@event);
    }

    private void Apply(IEvent @event)
    {
        switch (@event)
        {
            case ProductCreated e:
                Id = e.ProductId;
                Model = e.Model;
                Brand = e.Brand;
                Price = e.Price;
                break;
            case ProductModelChanged e:
                Model = e.Model;
                break;
            case ProductBrandChanged e:
                Brand = e.Brand;
                break;
            case ProductPriceChanged e:
                Price = e.Price;
                break;
        }

        Version = @event.Version;
    }

    public void LoadFromHistory(IEnumerable<IEvent> history)
    {
        foreach (var e in history)
            Apply(e);
    }
}
