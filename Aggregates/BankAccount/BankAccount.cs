using EventSourcingDemo.Events;
using EventSourcingDemo.Events.BankAccount;

namespace EventSourcingDemo.Aggregates.BankAccount;

public class BankAccount
{
    public Guid Id { get; private set; }
    public string Owner { get; private set; }
    public decimal Balance { get; private set; }
    public int Version { get; private set; } = -1;

    private List<IEvent> _changes = new();

    public IEnumerable<IEvent> GetUncommittedChanges() => _changes;
    public void MarkChangesAsCommitted() => _changes.Clear();

    public void ApplySnapshot(BankAccountSnapshot snapshot)
    {
        Id = snapshot.Id;
        Owner = snapshot.Owner;
        Balance = snapshot.Balance;
        Version = snapshot.Version;
    }

    public void Create(Guid id, string owner)
    {
        ApplyChange(new AccountCreated(id, owner));
    }

    public void Deposit(decimal amount)
    {
        ApplyChange(new MoneyDeposited(amount));
    }

    public void Withdraw(decimal amount)
    {
        ApplyChange(new MoneyWithdrawn(amount));
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
            case AccountCreated e:
                Id = e.AccountId;
                Owner = e.Owner;
                Balance = 0;
                break;
            case MoneyDeposited e:
                Balance += e.Amount;
                break;
            case MoneyWithdrawn e:
                Balance -= e.Amount;
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
