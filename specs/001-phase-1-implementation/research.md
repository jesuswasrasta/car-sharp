# Research: Phase 1 - Minimal Fleet Management

## Decision: Equality-based Identity
- **Rationale**: In Phase 1, we avoid explicit identifiers (ID, Plate) to keep the domain minimal. Identity is handled via language-native equality: reference equality for OOP classes and value equality for Functional records. This highlights the fundamental difference in how objects vs. values are perceived in different paradigms.
- **Alternatives considered**: 
  - Adding a GUID: Rejected to avoid premature domain complexity.
  - Position-based removal: Rejected as it doesn't represent "removing a specific car".

## Decision: FsCheck.Xunit for Functional Testing
- **Rationale**: Property-based testing is a core pillar of the functional paradigm. FsCheck is the most mature .NET tool for this.
- **Alternatives considered**: 
  -CsCheck: A more C#-native alternative, but FsCheck has better integration with the project's educational goals and xUnit.

## Decision: System.Collections.Immutable
- **Rationale**: To strictly adhere to the functional paradigm, we use immutable collections. This ensures that any "change" to the fleet results in a new instance, preventing side effects.
- **Alternatives considered**:
  - `IEnumerable` with LINQ: Functional in spirit but doesn't provide the structured "non-destructive mutation" patterns (like `Add`, `Remove`) that `ImmutableList` offers.