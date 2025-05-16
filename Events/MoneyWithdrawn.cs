namespace EventSourcingDemo.Events;

public class MoneyWithdrawn : Event
{
    public decimal Amount { get; }

    public MoneyWithdrawn(decimal amount)
    {
        Amount = amount;
    }
}
