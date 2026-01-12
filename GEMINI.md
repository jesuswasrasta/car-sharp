# CarSharp - Car Rental Core Domain

## Project Overview

CarSharp is an F# library that implements the core business logic for a car rental service. It is designed as a backend system without a graphical user interface, focusing strictly on state management, business rules, and decision-making. The project follows an incremental development approach, introducing requirements in "Phases".

### Key Technologies
- **Language**: F# (Functional)
- **Framework**: .NET 10.0
- **Testing**: xUnit with FsCheck (Property-based testing)

## Project Structure

```
/
├── src/
│   ├── CarSharp/           # Core Logic Library
│   │   ├── Library.fs      # Domain types and functions
│   │   └── CarSharp.fsproj
│   └── CarSharp.Test/      # Testing Project
│       ├── Tests.fs        # Unit and Property-based tests
│       ├── Program.fs      # Test runner entry point
│       └── CarSharp.Test.fsproj
├── CarSharp.sln            # Solution file
├── README.md               # Requirements and Phases
└── CLAUDE.md               # Context for AI assistants
```

## Development Workflow

### Building and Running

Since this is a library, "running" typically means executing the tests to verify logic.

```bash
# Build the solution
dotnet build

# Run all tests
dotnet test

# Run tests continuously (Watch mode)
dotnet watch test --project src/CarSharp.Test/CarSharp.Test.fsproj
```

### Development Approach

The project is built in **Phases** (defined in `README.md`). Each phase adds complexity to the domain.
*   **Current State**: Early phases (Basic fleet management, Availability).
*   **Methodology**: Test-Driven Development (TDD) and Property-Based Testing.
    *   Logic is often prototyped in `Tests.fs` (e.g., the `book` function) before being moved to the core library.

### Coding Conventions

1.  **Functional First**: Use pure functions, immutable data structures, and composition.
2.  **Type Safety**: Use Discriminated Unions (DUs) and specific types (e.g., `Available`, `Rent`) to model state and prevent invalid data usage.
    *   Example: `Fleet of Car list` instead of a raw list.
3.  **Error Handling**: Use `Result<_,_>` types for operations that can fail (Railway Oriented Programming). Do not throw exceptions for domain errors.
4.  **Testing**:
    *   Use **FsCheck** properties (`[<Property>]`) to verify invariants (e.g., "Adding a car always increases count by 1").
    *   Use xUnit facts (`[<Fact>]`) for specific edge cases.
    *   Use the `getOk` helper in tests to unwrap `Result` types safely.

## Key Domain Concepts

*   **Car**: Can be in different states (`AvailableCar`, `RentCar`), identified by a GUID.
*   **Fleet**: An immutable collection of cars.
*   **Operations**: `add`, `remove`, `count`, `book` (logic currently in transition from test to lib).

## Future Requirements (Roadmap)

Refer to `README.md` for the full list, but upcoming complexities include:
*   Batch processing (atomic transactions).
*   Vehicle differentiation (types, sizes).
*   Pricing strategies and optimizations.
