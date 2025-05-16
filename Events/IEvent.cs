namespace EventSourcingDemo.Events;

public interface IEvent { 
    int Version { get; set; }
    }