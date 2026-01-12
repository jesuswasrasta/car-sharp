# CarSharp - Car Rental Core Domain

## Project Overview

CarSharp is a C# library that implements the core business logic for a car rental service. It serves as a comparative study between **Object-Oriented (OOP)** and **Functional Programming** paradigms using modern C#. The system is a backend library focusing strictly on state management, business rules, and decision-making.

### Key Technologies
- **Language**: C# (.NET 10.0)
- **Testing**: vanilla xUnit for OOP (Example-based testing) and xUnit with FsCheck.Xunit (Property-based testing)

## Project Structure

```
/
├── src/
│   ├── CarSharp.Oop/               # Idiomatic OOP Implementation
│   ├── CarSharp.Functional/        # Functional C# Implementation
│   ├── CarSharp.Oop.Tests/         # Idiomatic OOP Testing Project
│   ├── CarSharp.Functional.Tests/  # Functional C# Implementation Testing Project
├── CarSharp.sln                # Solution file
├── README.md                   # Requirements and Phases (Italian)
└── CLAUDE.md                   # Context for AI assistants
```

## Development Workflow

### Building and Running

```bash
# Build the solution
dotnet build

# Run all tests
dotnet test

# Run tests continuously (Watch mode)
dotnet watch test
```

### Development Approach

The project is built in **Phases** (defined in `README.md`). Every phase is implemented twice to highlight the architectural differences between OOP and Functional styles in C#.

### Coding Conventions

1.  **OOP Track**: Use classes, encapsulation, mutable state, and example-based testing.
2.  **Functional Track**: Use records, immutability, pure functions, and property-based testing.
3.  **Consistency**: Both implementations must satisfy the same business requirements but through different paradigms.

## Key Domain Concepts

*   **Car**: Modeled as an entity in OOP, as a record/value in Functional.
*   **Fleet**: A collection managing vehicles and their states.
*   **Operations**: Add, Remove, Count, Book (Incremental complexity).

## Future Requirements (Roadmap)

Refer to `README.md` for the full list of 10 phases, covering everything from batch processing to profit optimization.