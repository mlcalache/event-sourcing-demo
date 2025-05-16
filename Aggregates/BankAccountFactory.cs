using EventSourcingDemo.Events;
using EventSourcingDemo.Aggregates;

public static class BankAccountFactory
{
    public static BankAccount Restore(EventStore store, Guid id)
    {
        var account = new BankAccount();

        var snapshot = store.GetLatestSnapshot(id);
        
        if (snapshot != null)
        {
            account = new BankAccount();
            account.ApplySnapshot(snapshot);
            account.LoadFromHistory(store.GetEvents(id, snapshot.Version));
        }
        else
        {
            var events = store.GetEvents(id);
            account.LoadFromHistory(events);
        }

        return account;
    }
}
