using System;
using EventSourcingDemo.Events;
using EventSourcingDemo.Aggregates;

class Program
{
    static void Main(string[] args)
    {
        var store = new EventStore();

        var accountId = Guid.NewGuid();
        var account = new BankAccount();
        account.Create(accountId, "Matheus");
        account.Deposit(100);
        account.Withdraw(40);

        store.Save(accountId, account.GetUncommittedChanges());
        account.MarkChangesAsCommitted();

        var snapshot = new Snapshot
        {
            Id = accountId,
            Version = account.Version,
            Owner = account.Owner,
            Balance = account.Balance
        };

        store.SaveSnapshot(snapshot);

        Console.WriteLine($"Original Account Balance: {account.Balance}");

        var restored = BankAccountFactory.Restore(store, accountId);
        Console.WriteLine($"Restored from snapshot Balance: {restored.Balance}");

        account.Deposit(100);
        store.Save(accountId, account.GetUncommittedChanges());

        Console.WriteLine($"Original Account Balance: {account.Balance}");

        restored = BankAccountFactory.Restore(store, accountId);
        Console.WriteLine($"Restored from snapshot Balance: {restored.Balance}");
    }
}
