namespace EventSourcingDemo.Events;

public abstract class Event : IEvent
{
    public int Version { get; set; }
}