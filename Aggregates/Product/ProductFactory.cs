using EventSourcingDemo.Events;
using EventSourcingDemo.Events.Product;

namespace EventSourcingDemo.Aggregates.Product;

public static class ProductFactory
{
    public static Product Restore(ProductEventStore store, Guid id)
    {
        var product = new Product();

        var snapshot = store.GetLatestSnapshot(id);

        if (snapshot != null)
        {
            product = new Product();
            product.ApplySnapshot(snapshot);
            product.LoadFromHistory(store.GetEvents(id, snapshot.Version));
        }
        else
        {
            var events = store.GetEvents(id);
            product.LoadFromHistory(events);
        }

        return product;
    }
}
