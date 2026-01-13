# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository Overview

This is a **C# library project** implementing the core business logic for a **car rental service**. The project focuses on comparing two paradigms within C#: **Object-Oriented (OOP)** and **Functional Programming**. The domain is introduced **incrementally through phases**, starting from a minimal model and progressively enriching it with new requirements.

### Technology Stack

- **Language**: C# 13.0
- **Target Framework**: .NET 10.0
- **Test Framework**: xUnit with FsCheck.Xunit (or CsCheck) for property-based testing
- **Solution Structure**:
  - `src/CarSharp.Oop/` - Idiomatic C# OOP implementation
  - `src/CarSharp.Functional/` - Functional C# implementation
  - `src/CarSharp.Tests/` - Unified or separate test projects for both paradigms

## Common Commands

### Build and Test

```bash
# Build the entire solution
dotnet build

# Run all tests
dotnet test

# Watch mode for continuous testing during development
dotnet watch test
```

### Clean and Restore

```bash
# Clean build artifacts
dotnet clean

# Restore NuGet packages
dotnet restore
```

## Architecture and Design Approach

### Comparative Development

Every phase MUST be implemented in both styles:

1.  **OOP Style**:
    - Use classes with private fields and public methods.
    - Encapsulate logic within the object.
    - Use state mutation where appropriate.
    - Handle errors via exceptions or boolean returns.
2.  **Functional Style**:
    - Use `record` for immutable data structures.
    - Use static pure functions.
    - Leverage `OneOf` or custom `Result<T>` types for error handling.
    - No side effects in the domain logic.

### Domain-Driven Incremental Development

The project follows a **phase-based approach** defined in `README.md`. Each phase adds new concepts and constraints, which must be addressed in both C# tracks.

## Development Guidelines

### Working with the Phased Requirements

1. Ensure earlier phase requirements still pass in both implementations.
2. Add new types/functionality to both tracks.
3. Use **Example-Based Testing** for the OOP track.
4. Use **Property-Based Testing** for the Functional track.

### Testing Strategy

- **OOP Tests**: Focus on state changes and specific scenarios.
- **Functional Tests**: Focus on invariants and mathematical properties of the functions.

## Project-Specific Notes

### Code Organization

```
CarSharp.sln
├── src/
│   ├── CarSharp.Oop/         (C# OOP Project)
│   ├── CarSharp.Functional/  (C# Functional Project)
│   └── CarSharp.Tests/       (Shared or separate tests)
```


# Italian Language Usage
All comments, documentation, and domain concepts MUST be expressed in Italian to cater to the target audience of Italian programmers.