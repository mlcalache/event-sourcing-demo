namespace EventSourcingDemo.Events;

public class MoneyDeposited : Event
{
    public decimal Amount { get; }

    public MoneyDeposited(decimal amount)
    {
        Amount = amount;
    }
}