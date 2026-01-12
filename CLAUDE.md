# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository Overview

This is an **F# library project** implementing the core business logic for a **car rental service**. The system is designed as a state management and decision-making library without any UI. The domain is intentionally introduced **incrementally through phases**, starting from a minimal model and progressively enriching it with new requirements.

### Technology Stack

- **Language**: F# (functional programming)
- **Target Framework**: .NET 10.0
- **Test Framework**: xUnit with FsCheck for property-based testing
- **Solution Structure**:
  - `src/CarSharp/` - Core library
  - `src/CarSharp.Test/` - Test project with property-based tests

## Common Commands

### Build and Test

```bash
# Build the entire solution
dotnet build

# Build specific project
dotnet build src/CarSharp/CarSharp.fsproj
dotnet build src/CarSharp.Test/CarSharp.Test.fsproj

# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Run tests for a specific project
dotnet test src/CarSharp.Test/CarSharp.Test.fsproj

# Watch mode for continuous testing during development
dotnet watch test --project src/CarSharp.Test/CarSharp.Test.fsproj
```

### Clean and Restore

```bash
# Clean build artifacts
dotnet clean

# Restore NuGet packages
dotnet restore
```

## Architecture and Design Approach

### Domain-Driven Incremental Development

The project follows a **phase-based approach** where the domain model is intentionally kept minimal and enriched progressively. Each phase adds new concepts and constraints:

**Phase 1-2**: Basic fleet management with availability states
**Phase 3**: Batch request processing with transactional semantics
**Phase 4-5**: Vehicle differentiation by capacity and optimal assignment
**Phase 6-7**: Pricing, customers, and discount logic
**Phase 8**: Profit optimization for assignment strategies
**Phase 9**: Optional services and fuel charges
**Phase 10**: Advanced constraints (premium vehicles, budgets, fairness policies, advance bookings)

### Core Domain Model (src/CarSharp/Library.fs)

The domain is modeled using F# discriminated unions and type-driven design:

- **Car States**: Cars can be `AvailableCar` or `RentCar`, each wrapping a GUID identifier
- **Fleet**: Represented as `Fleet of Car list` - an immutable collection
- **Error Handling**: Uses F#'s `Result<Fleet, Error>` type for operations that can fail
- **Pure Functions**: All operations return new fleet states without mutating existing ones

Key domain operations:
- `add`: Adds a car to the fleet (returns new Fleet)
- `remove`: Removes a car from the fleet (returns Result)
- `count`: Returns the total number of vehicles
- `find`/`findIndex`: Locates cars in the fleet

### Testing Strategy (src/CarSharp.Test/)

The project uses **property-based testing** with FsCheck, which generates random test data to verify properties hold for all inputs:

- **Unit tests** with `[<Fact>]` for specific scenarios
- **Property tests** with `[<Property>]` for general invariants
- Test helper `getOk`: Unwraps Result types in tests (fails if Error)
- Business logic like `book` is tested in the test file, indicating evolving design

Example property: `Add cars to Fleet` verifies that adding any car increases count by exactly 1.

## Development Guidelines

### Functional Programming Principles

- **Immutability**: All data structures are immutable; operations return new instances
- **Type Safety**: Leverage F#'s type system to make illegal states unrepresentable
- **Railway-Oriented Programming**: Use `Result` types to handle success/failure paths
- **Pure Functions**: Keep functions side-effect free for testability and composability

### Working with the Phased Requirements

When implementing new features, refer to the README.md phases. Each phase builds on the previous:

1. Ensure earlier phase requirements still pass
2. Add new types/functions without breaking existing invariants
3. Use property-based tests to verify general behavior
4. Consider transactional semantics (e.g., Phase 3's all-or-nothing batch processing)

### Type-Driven Development

The codebase uses **wrapper types** to prevent primitive obsession:
- `Available of Guid` and `Rent of Guid` instead of raw GUIDs
- `Fleet of Car list` instead of raw lists
- `FleetCount = uint` for semantic clarity

Continue this pattern when adding new domain concepts (e.g., pricing, customer identifiers).

## Project-Specific Notes

### Current Implementation Status

The implementation covers the early phases (fleet management and basic availability). The `book` function for renting cars is currently implemented in the test file (Tests.fs:57-69), suggesting ongoing development of Phase 2.

### Test-First Approach

Notice that domain logic like car booking exists in tests before being moved to the library. This indicates an exploratory, test-driven workflow appropriate for the incremental phase-based design.

### Transactional Constraints

Phase 3 and beyond require **atomic batch operations**: if any request in a batch fails, none should be applied. This will require careful design of transaction boundaries in F#.

### Future Complexity

Later phases (8-10) introduce:
- **Optimization problems**: Choosing vehicle assignments to maximize profit
- **Complex constraints**: Premium vehicles, customer budgets, fairness policies, advance bookings
- **Non-functional requirements**: Deterministic results, rounding rules, cancellation penalties

These will likely require more sophisticated algorithms and potentially external libraries for constraint solving or optimization.

## Code Organization

```
CarSharp.sln
├── src/
│   ├── CarSharp/
│   │   ├── CarSharp.fsproj    (Library project, .NET 10.0)
│   │   └── Library.fs          (Core domain model)
│   └── CarSharp.Test/
│       ├── CarSharp.Test.fsproj (Test project with xUnit + FsCheck)
│       ├── Tests.fs             (Property-based and unit tests)
│       └── Program.fs           (Test runner entry point)
```

All core domain logic should live in `src/CarSharp/Library.fs` (or additional .fs files added to CarSharp.fsproj). Tests remain in `src/CarSharp.Test/Tests.fs`.
