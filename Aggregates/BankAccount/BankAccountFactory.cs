using EventSourcingDemo.Events;
using EventSourcingDemo.Events.BankAccount;

namespace EventSourcingDemo.Aggregates.BankAccount;

public static class BankAccountFactory
{
    public static BankAccount Restore(BankAccountEventStore store, Guid id)
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
