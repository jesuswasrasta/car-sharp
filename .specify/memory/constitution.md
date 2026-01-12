# CarSharp Constitution

<!-- Sync Impact Report
Version: 1.0.0 -> 2.0.0
Modified Principles:
- "Functional Domain Modeling" -> "Comparative Implementation Strategy" (Major scope change)
- "Railway Oriented Programming" -> "Architectural Contrast" (Broadened)
- "Property-Based Testing" -> "Paradigm-Specific Testing" (Split strategies)
- "Library-First Architecture" -> "Narrative Test-Driven Development" (Process focus)
Added Sections: None.
Templates:
- .specify/templates/plan-template.md: ⚠ Pending update for multi-language structure
- .specify/templates/tasks-template.md: ⚠ Pending update for dual-track tasks
- .specify/templates/spec-template.md: ✅
Follow-up: Update project structure to include C# project; Create slide/intro content folder.
-->

## Core Principles

### I. Comparative Implementation Strategy
The primary goal is creating educational material comparing paradigms. Every domain phase MUST be implemented twice:
1.  **C# (OOP)**: Idiomatic Object-Oriented style (Classes, Encapsulation, Mutable State).
2.  **F# (Functional)**: Idiomatic Functional style (Types, Immutability, Pure Functions).
The C# implementation acts as the "control group" (familiar territory) to contrast against the F# approach.

### II. Narrative Test-Driven Development
Code is documentation. Development MUST follow a strict, granular Red-Green-Refactor cycle intended for presentation.
*   **Granularity**: One commit per TDD step.
*   **Clarity**: Code and tests must include comments explaining the *why* for an audience.
*   **Artifacts**: Each phase includes introductory text/slides explaining the concepts before the code.

### III. Paradigm-Specific Testing
Testing strategies must reflect the strengths of each paradigm:
*   **C#**: Use **Example-Based Testing** (xUnit `[Fact]`). Verify specific state transitions after method calls.
*   **F#**: Use **Property-Based Testing** (FsCheck `[Property]`). Verify invariants that hold true for any random input.

### IV. Incremental Phased Delivery
Development follows the strictly defined "Phases" (1-10). Both C# and F# implementations for Phase N must be complete and compared before moving to Phase N+1.

### V. Architectural Contrast
Implementations must highlight the differences in error handling and state management:
*   **C#**: Use Exceptions or boolean returns for failure; `void` methods for state mutation.
*   **F#**: Use Railway Oriented Programming (`Result<T,E>`) for failure; `State -> State` transformations for logic.

## Governance

This constitution defines the rules for the CarSharp educational project.

*   **Amendments**: Changes require a version bump.
*   **Compliance**: Pull requests must demonstrate the dual implementation and narrative structure.
*   **Versioning**: Semantic Versioning.
    *   **MAJOR 2.0.0**: Shift from pure F# library to Comparative Educational Project.

**Version**: 2.0.0 | **Ratified**: 2026-01-12 | **Last Amended**: 2026-01-12
