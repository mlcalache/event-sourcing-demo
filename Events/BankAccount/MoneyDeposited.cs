using EventSourcingDemo.Events;

namespace EventSourcingDemo.Events.BankAccount;

public class MoneyDeposited : Event
{
    public decimal Amount { get; }

    public MoneyDeposited(decimal amount)
    {
        Amount = amount;
    }
}