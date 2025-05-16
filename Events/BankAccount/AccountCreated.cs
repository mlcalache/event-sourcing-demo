using EventSourcingDemo.Events;

namespace EventSourcingDemo.Events.BankAccount;

public class AccountCreated : Event
{
    public Guid AccountId { get; }
    public string Owner { get; }

    public AccountCreated(Guid accountId, string owner)
    {
        AccountId = accountId;
        Owner = owner;
    }
}