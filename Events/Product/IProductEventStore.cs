namespace EventSourcingDemo.Events.Product;

public interface IProductEventStore
{
    IEnumerable<IEvent> GetEvents(Guid aggregateId, int afterVersion = -1);
    ProductSnapshot? GetLatestSnapshot(Guid aggregateId);
    void Save(Guid aggregateId, IEnumerable<IEvent> events);
    void SaveSnapshot(ProductSnapshot snapshot);
}
