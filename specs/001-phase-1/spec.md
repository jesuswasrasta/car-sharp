# Feature Specification: Minimal Fleet Phase 1

**Feature Branch**: `001-phase-1`  
**Created**: 2026-01-12  
**Status**: Draft  
**Input**: User description (Educational Comparison Strategy for Phase 1)

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Educational Foundation (Priority: P1)

As an instructor, I want to present the context and learning objectives for Phase 1 so that the audience understands the goals before seeing code.

**Why this priority**: Sets the stage for the comparison; essential for the educational narrative.

**Independent Test**: Can be verified by reviewing the `slides-intro.md` content against the learning objectives (Mutable vs Immutable, Example vs Property Testing).

**Acceptance Scenarios**:

1. **Given** the presentation starts, **When** the intro is shown, **Then** it clearly defines the "Minimal Fleet" requirements (Add, Remove, Count).
2. **Given** the intro, **When** objectives are presented, **Then** it contrasts Mutable State (C#) vs Immutable State (F#).

---

### User Story 2 - C# Implementation (Control Group) (Priority: P1)

As an "Enterprise C# Developer", I want to implement the Fleet logic using standard OOP practices and TDD so that we establish a baseline for comparison.

**Why this priority**: Essential for the "before" part of the comparison.

**Independent Test**: Can be verified by running `dotnet test` on the C# project and checking git history for TDD steps.

**Acceptance Scenarios**:

1. **Given** a new `Fleet` instance, **When** checked, **Then** `Count` is 0.
2. **Given** an empty fleet, **When** `Add(car)` is called, **Then** `Count` becomes 1.
3. **Given** a fleet with one car, **When** `Remove(car)` is called, **Then** `Count` becomes 0 and returns true.
4. **Given** a fleet, **When** `Remove(missingCar)` is called, **Then** `Count` is unchanged and returns false.

---

### User Story 3 - F# Implementation (New Paradigm) (Priority: P1)

As an "F# Functional Programmer", I want to implement the Fleet logic using functional patterns and Property-Based Testing to demonstrate the benefits of immutability and stronger invariants.

**Why this priority**: The core educational value proposition.

**Independent Test**: Can be verified by running `dotnet test` on the F# project and checking FsCheck output.

**Acceptance Scenarios**:

1. **Given** any random list of cars, **When** creating a `Fleet`, **Then** the count matches the list length (Invariant).
2. **Given** any fleet `f` and car `c`, **When** `add c f` is called, **Then** the new fleet count is `f.Count + 1`.
3. **Given** any fleet `f` and car `c`, **When** `add c f` then `remove c`, **Then** the resulting fleet has the same count as `f` (Reversibility).
4. **Given** a removal operation, **When** the car is missing, **Then** it returns a `Result.Error` instead of false/exception.

---

### User Story 4 - Comparative Wrap-up (Priority: P2)

As a learner, I want to see a side-by-side comparison of the two implementations so that I can solidify my understanding of the paradigm shift.

**Why this priority**: Consolidates the lesson.

**Independent Test**: Verified by reviewing `comparison.md`.

**Acceptance Scenarios**:

1. **Given** both implementations complete, **When** reviewing the wrap-up, **Then** specific code snippets (Void vs Pure, Exception vs Result) are contrasted.

### Edge Cases

- **Removing a non-existent car**: 
  - C#: Should return `false` (no exception).
  - F#: Should return `Result.Error` (Railway Oriented).
- **Adding the same car twice**:
  - Phase 1 assumes cars are indistinguishable or unique ID handling is not yet enforced (Parco Mezzi Minimale).
  - Assumption: We treat cars as distinct instances or value equality?
  - *Clarification*: For Phase 1, we assume adding the same instance increments count (multiset behavior) or list simply appends. To be clarified in implementation, but safe default is "List.Add allows duplicates".

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST provide a C# `Fleet` class with internal mutable state (`List<Car>`).
- **FR-002**: C# implementation MUST use `void Add(Car)` (side-effect) and `bool Remove(Car)` (return status).
- **FR-003**: C# tests MUST use xUnit `[Fact]` and strict Example-Based Testing.
- **FR-004**: System MUST provide an F# `Fleet` type (immutable wrapper).
- **FR-005**: F# implementation MUST use pure functions: `add: Car -> Fleet -> Fleet` and `remove: Car -> Fleet -> Result<Fleet, Error>`.
- **FR-006**: F# tests MUST use FsCheck `[Property]` to verify invariants over random data.
- **FR-007**: Development MUST follow strict TDD (Red -> Green -> Refactor) with one commit per step.
- **FR-008**: Commits MUST include educational commentary explaining the *why* of each step.

### Key Entities

- **Car**: Initially a dummy/unit type (indistinguishable vehicles in Phase 1).
- **Fleet**: Collection of cars (Mutable Class in C#, Immutable Type in F#).

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: 100% of defined C# Facts pass.
- **SC-002**: 100% of defined F# Properties pass (default FsCheck config, usually 100 runs).
- **SC-003**: Git history contains at least 3 distinct commits per implementation track (Red, Green, Refactor) properly labeled.
- **SC-004**: `comparison.md` identifies at least 3 key differences (State, Signatures, Error Handling).