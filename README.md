# Event Sourcing Demo - C# Console Application

This is a simple C# console application demonstrating **Event Sourcing** principles. Instead of storing only the current state, all changes to application state are stored as a sequence of immutable events. This allows for full traceability, easy debugging, and rebuilding the state by replaying events.

The project includes two domains: `BankAccount` and `Product`, each implemented with event sourcing concepts including versioning and snapshot support.

## Features

- Event-sourced aggregates
- In-memory event store
- Version tracking for event consistency
- Snapshot mechanism to optimize replay
- Separation of concerns between state-changing logic and storage

## Domains

### BankAccount

A simple account domain supporting:

- Account creation
- Money deposit
- Money withdrawal
- Event versioning
- Snapshot saving and restoration

### Product

A parallel domain showcasing similar principles as `BankAccount`, allowing:

- Product creation
- Price updates
- Stock changes
- Versioned event handling
- Snapshot-based restoration

## Architecture

- **Events**: Represent changes in state. Each event carries a version for concurrency and replay ordering.
- **Aggregates**: Core domain entities (`BankAccount`, `Product`) that apply events to evolve their state.
- **Event Store**: In-memory storage that records events by aggregate ID and supports snapshot persistence.
- **Snapshots**: Serialized state captures used to speed up aggregate rehydration by avoiding full event replay.
- **Factory Methods**: Used to safely create aggregates from snapshots without exposing internal state setters.

## Project Structure

/EventSourcingDemo
|__ Program.cs

/Aggregates
|__ /BankAccount
    |__ BankAccount.cs
    |__ BankAccountFactory.cs
|__ /Products
    |__ Product.cs
    |__ ProductFactory.cs

/Events
|__ Event.cs
|__ /BankAccount
    |__ AccountCreated.cs
    |__ BankAccountEventStore.cs
    |__ BankAccountSnapshot.cs
    |__ MoneyDeposited.cs
    |__ MoneyWithdrawn.cs
|__ /Product
    |__ ProductCreated.cs
    |__ ProductEventStore.cs
    |__ ProductSnapshot.cs
    |__ ModelChanged.cs
    |__ BrandChanged.cs
    |__ PriceChanged.cs

README.md


## How to Run

1. Clone the repository or create the project using the .NET CLI:

```bash
dotnet new console -n EventSourcingDemo
cd EventSourcingDemo
```

2. Add the `.cs` files under appropriate folders (`Domain` and `Infrastructure`).
3. Build and run:

```bash
dotnet run
```

The console will output results demonstrating creation, transactions, and state rehydration using events and snapshots.

## Extending the Project

- Replace in-memory store with a persistent database (e.g., SQL, MongoDB, or file-based).
- Integrate with CQRS to separate query and command responsibilities.
- Use MediatR or similar libraries to dispatch events asynchronously.
- Add unit tests to verify event sourcing logic and business invariants.