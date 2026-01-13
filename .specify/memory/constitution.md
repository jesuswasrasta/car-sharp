# CarSharp Constitution

<!-- Sync Impact Report
Version: 2.2.0
Modified Principles:
- II. Narrative TDD: Removed "Artifacts" (slides), added emphasis on in-code comments for presentation.
- IX. Documentation and Recap: New requirement to update README.md after each phase.
-->

## Core Principles

### I. Comparative Implementation Strategy
The primary goal is creating educational material comparing software paradigms within the same language. Every domain phase MUST be implemented twice:
1.  **C# (OOP, Object Oriented)**: Idiomatic Object-Oriented style (Classes, Encapsulation, Mutable State). This acts as the "control group" (familiar territory). **CRITICAL**: Always use skill `csharp-OOP-developer` for OOP implementations.

2.  **C# (FP, Functional)**: C# leveraging modern functional features (Records, Immutability, Pure Functions, LINQ) and functional patterns (Monads, Railway Oriented Programming, First-Class Functions and Higher-Order Functions, Closures, Function Composition and Pipelines, Pattern Matching, The Option/Maybe Pattern, Currying and Partial Application). **CRITICAL**: Always use skill `csharp-FP-developer` for FP implementations.

The objective is to demonstrate how the same language can be used to solve the same problem using radically different mental models.

### II. Narrative Test-Driven Development
Code is documentation. Development MUST follow a strict, granular Red-Green-Refactor cycle intended for presentation.
*   **Granularity**: One commit per TDD step.
*   **Clarity**: Code and tests must include comments explaining the motivation behind, the *why*. These comments are the primary source of explanation during live presentations. Always underline patterns used, design decision, paradigm specific choices.

Example: 
YES:
```csharp
    [Fact]
    public void ParcoMezziVuoto_DovrebbeAvereZeroAuto()
    {
        //Il punto di partenza del dominio è un valore immutabile vuoto.
        Assert.Equal(0, ParcoMezzi.Vuoto.TotaleAuto);
    }
```
NO:
```csharp
    [Fact]
    public void Test1()
    {
    [Fact]
    public void ParcoMezziVuoto_DovrebbeAvereZeroAuto()
    {
        //Perché: Il punto di partenza del dominio è un valore immutabile vuoto.
        Assert.Equal(0, ParcoMezzi.Vuoto.TotaleAuto);
    }
    }
```


### III. Paradigm-Specific Testing
Testing strategies must reflect the strengths of each paradigm:
*   **C# (OOP)**: Use **Example-Based Testing**. Verify specific state transitions after method calls using xUnit `[Fact]`.
*   **C# (Functional)**: Use **Property-Based Testing**. Verify invariants that hold true for any random input using xUnit with FsCheck.
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
*   **Content**: Don't bother the audience with info about using TDD, they know about it (no "//Act, //Arrange, //Assert" comments). Underline the paradigm differences instead.

### VII. Language
The audience is Italian programers. 
Use Italian language for comments, for domain concepts (classes, type records, variables) and documentation in general.
Use italian language for commit messages.
Use english only for technical related code elements (eg. library names, framework constructs, design patterns names, etc).

Examples:
*   **Code**: NO: "class Car" YES: "classe Auto".
*   **Code**: NO: "record Risultato<Valore, Errore>" YES: "record Result<Value, Error>".
*   **Comments**: NO: "This a test for the Car class". YES: "Questo è un test per la classe Auto".
*   **Documentation**: NO: "The Car domain represents vehicles". YES: "Il dominio Auto rappresenta i veicoli".
*   **Commit message**: NO: "feat: add car rental functionality" YES: "feat: aggiunge funzionalità di noleggio auto".


### VIII. Git Strategy
Develop every phase in a dedicated branch as per Spec Driven Development using SpecKit commands.
After closing a phase, ask to merge branch into `main`.
After merging, add a tag with the same name of the branch.

Example:
*   Branch name: `001-fase-1-parco-mezzi`
*   Tag name: `001-fase-1-parco-mezzi`

### IX. Documentation and Recap
At the end of each development phase, the `README.md` MUST be updated with a detailed recap.
The recap should include:
- A summary of the steps taken during the phase.
- Details on the implementations made in both tracks.
- Concepts explored and the specific motivations/trade-offs behind the OOP and Functional choices.


## Governance

This constitution defines the rules for the CarSharp educational project.

*   **Amendments**: Changes require a version bump.
*   **Compliance**: Pull requests must demonstrate the dual implementation and narrative structure.
*   **Versioning**: Semantic Versioning.

**Version**: 2.2.0 | **Ratified**: 2026-01-13 | **Last Amended**: 2026-01-13
