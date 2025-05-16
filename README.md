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

```text
/EventSourcingDemo
│
├── Program.cs # Entry point
│
├── /Aggregates # Domain logic (entities, behavior)
│ ├── /BankAccount
│ │ ├── BankAccount.cs
│ │ └── BankAccountFactory.cs
│ │
│ └── /Product
│ ├── Product.cs
│ └── ProductFactory.cs
│
├── /Events # Domain events
│ ├── Event.cs # Base event interface/class
│ │
│ ├── /BankAccount
│ │ ├── AccountCreated.cs
│ │ ├── MoneyDeposited.cs
│ │ ├── MoneyWithdrawn.cs
│ │ └── BankAccountSnapshot.cs
│ │
│ └── /Product
│ ├── ProductCreated.cs
│ ├── ModelChanged.cs
│ ├── BrandChanged.cs
│ ├── PriceChanged.cs
│ └── ProductSnapshot.cs
│
├── /Infrastructure # In-memory storage & utilities
│ ├── BankAccountEventStore.cs
│ └── ProductEventStore.cs
│
└── README.md # Project documentation
```

## How to Run

1. Clone the repository
2. Build and run:

```bash
dotnet run
```

The console will output results demonstrating creation, transactions, and state rehydration using events and snapshots.

## Extending the Project

- Replace in-memory store with a persistent database (e.g., SQL, MongoDB, or file-based).
- Integrate with CQRS to separate query and command responsibilities.
- Use MediatR or similar libraries to dispatch events asynchronously.
- Add unit tests to verify event sourcing logic and business invariants.