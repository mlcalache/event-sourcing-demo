namespace EventSourcingDemo.Events.BankAccount;

public class BankAccountSnapshot
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public string Owner { get; set; }
    public decimal Balance { get; set; }
}
