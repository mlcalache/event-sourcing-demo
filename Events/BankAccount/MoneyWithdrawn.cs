using EventSourcingDemo.Events;

namespace EventSourcingDemo.Events.BankAccount;

public class MoneyWithdrawn : Event
{
    public decimal Amount { get; }

    public MoneyWithdrawn(decimal amount)
    {
        Amount = amount;
    }
}
