using EventSourcingDemo.Events;

namespace EventSourcingDemo.Events.BankAccount;

public class BankAccountEventStore
{
    private readonly Dictionary<Guid, List<IEvent>> _storage = new();
    private readonly Dictionary<Guid, BankAccountSnapshot> _snapshots = new();

    public void Save(Guid aggregateId, IEnumerable<IEvent> events)
    {
        if (!_storage.ContainsKey(aggregateId))
        {
            _storage[aggregateId] = new List<IEvent>();
        }
        _storage[aggregateId].AddRange(events);
    }

    public IEnumerable<IEvent> GetEvents(Guid aggregateId, int afterVersion = -1)
    {
        return _storage.ContainsKey(aggregateId)
            ? _storage[aggregateId].Where(e => e.Version > afterVersion)
            : new List<IEvent>();
    }

    public void SaveSnapshot(BankAccountSnapshot snapshot)
    {
        _snapshots[snapshot.Id] = snapshot;
    }

    public BankAccountSnapshot? GetLatestSnapshot(Guid aggregateId)
    {
        return _snapshots.TryGetValue(aggregateId, out var snapshot) ? snapshot : null;
    }
}
