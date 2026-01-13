# CarSharp Constitution

<!-- Sync Impact Report
Version: 2.1.0
Modified Principles:
- "Comparative Implementation Strategy": Focused strictly on C# (OOP vs Functional).
- "Paradigm-Specific Testing": Updated to contrast Example-Based vs Property-Based in C#.
- "Architectural Contrast": Focused on C# implementation differences.
Removed: All F# references.
-->

## Core Principles

### I. Comparative Implementation Strategy
The primary goal is creating educational material comparing software paradigms within the same language. Every domain phase MUST be implemented twice:
1.  **C# (OOP)**: Idiomatic Object-Oriented style (Classes, Encapsulation, Mutable State). This acts as the "control group" (familiar territory).
2.  **C# (Functional)**: C# leveraging modern functional features (Records, Immutability, Pure Functions, LINQ).

The objective is to demonstrate how the same language can be used to solve the same problem using radically different mental models.

### II. Narrative Test-Driven Development
Code is documentation. Development MUST follow a strict, granular Red-Green-Refactor cycle intended for presentation.
*   **Granularity**: One commit per TDD step.
*   **Clarity**: Code and tests must include comments explaining the *why* for an audience.
*   **Artifacts**: Each phase includes introductory text/slides explaining the concepts before the code.

### III. Paradigm-Specific Testing
Testing strategies must reflect the strengths of each paradigm:
*   **C# (OOP)**: Use **Example-Based Testing**. Verify specific state transitions after method calls using xUnit `[Fact]`.
*   **C# (Functional)**: Use **Property-Based Testing**. Verify invariants that hold true for any random input using xUnit with FsCheck (or similar C# property testing library like CsCheck/Hedgehog).
*  Always use xUnit as the base test runner to maintain consistency.

### IV. Incremental Phased Delivery
Development follows the strictly defined "Phases" (1-10). Both OOP and Functional implementations for Phase N must be complete and compared before moving to Phase N+1.

### V. Architectural Contrast
Implementations must highlight the differences in error handling and state management:
*   **C# (OOP)**: Use Exceptions or boolean returns for failure; methods that mutate internal state.
*   **C# (Functional)**: Use Result types for failure handling; pure functions that return new states.

### VI. Learning Purpose
The purpose of this project is educational. The focus is on clarity, contrast, and understanding over performance or production readiness.
*   **Comments**: Every test and every implementation must include inline comments explaining the *why* for an audience.
*   **Functional VS OOP**: Each functional implementation must include notes on how it contrasts with the OOP version.
*   **Content**: Don't bother the audience with info about using TDD, they know about it. Underline the paradigm differences instead.

### VII. Language
The audience is Italian programers. Use Italian language for comments, for domain concepts (classes, type records, variables) and documentation in general.
*   **Code**: NO: "class Car" YES: "classe Auto".
*   **Comments**: NO: "This a test for the Car class". YES: "Questo è un test per la classe Auto".
*   **Documentation**: NO: "The Car domain represents vehicles". YES: "Il dominio Auto rappresenta i veicoli".


## Governance

This constitution defines the rules for the CarSharp educational project.

*   **Amendments**: Changes require a version bump.
*   **Compliance**: Pull requests must demonstrate the dual implementation and narrative structure.
*   **Versioning**: Semantic Versioning.

**Version**: 2.1.0 | **Ratified**: 2026-01-12 | **Last Amended**: 2026-01-12