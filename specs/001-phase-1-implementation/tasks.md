# Tasks: Phase 1 - Minimal Fleet Management

**Input**: Design documents from `/specs/001-phase-1-implementation/`
**Prerequisites**: plan.md, spec.md, research.md, data-model.md

## Implementation Strategy

We follow a narrative TDD approach for educational purposes.
- **Granularity**: Exactly one commit per TDD step (Red -> Green -> Refactor).
- **Learning Focus**: Every file must include `ABOUTME` headers. Every test and implementation must include inline comments explaining the *why*.
- **Paradigm Contrast**: Functional implementations must include specific notes comparing the approach to the OOP version.
- **MVP**: Completion of User Story 1 (Basic Population) in both tracks.

## Dependency Graph

```text
Setup -> Foundational -> US1 (Population) -> US2 (Maintenance) -> Polish
```

## Phase 1: Setup & Educational Prep

- [ ] T001 Write educational introductory slides in `specs/001-phase-1-implementation/slides-intro.md`
- [ ] T002 Update `README.md` to reflect Phase 1 progress and learning goals
- [ ] T003 [P] Verify NuGet package `FsCheck.Xunit` is present in `src/CarSharp.Functional.Tests/CarSharp.Functional.Tests.csproj`
- [ ] T004 [P] Verify NuGet package `System.Collections.Immutable` is present in `src/CarSharp.Functional/CarSharp.Functional.csproj`

## Phase 2: Foundational (Common Entities)

- [ ] T005 [P] Implement `Car` class with `ABOUTME` and educational comments in `src/CarSharp.Oop/Car.cs`
- [ ] T006 [P] Implement `Car` record with `ABOUTME` and educational comments in `src/CarSharp.Functional/Car.cs`

## Phase 3: User Story 1 - Basic Fleet Population [US1]

**Goal**: Enable fleet creation and adding cars to track total inventory.
**Independent Test**: Verify empty fleet count is 0; verify adding $N$ cars results in count $N$.

- [ ] T007 [US1] [OOP] Write failing Fact for initial empty fleet state in `src/CarSharp.Oop.Tests/FleetTests.cs`
- [ ] T008 [US1] [OOP] Implement `Fleet` class with `TotalCars` property in `src/CarSharp.Oop/Fleet.cs`
- [ ] T009 [US1] [OOP] Write failing Fact for adding a car to the fleet in `src/CarSharp.Oop.Tests/FleetTests.cs`
- [ ] T010 [US1] [OOP] Implement `AddCar` method with mutable state in `src/CarSharp.Oop/Fleet.cs`
- [ ] T011 [US1] [Functional] Write failing Property for initial state, basic addition, and large volume (10k) in `src/CarSharp.Functional.Tests/FleetTests.cs`
- [ ] T012 [US1] [Functional] Implement `Fleet` record and `AddCar` extension with immutability and contrast notes in `src/CarSharp.Functional/Fleet.cs`

## Phase 4: User Story 2 - Basic Fleet Maintenance [US2]

**Goal**: Enable removing cars from the fleet inventory.
**Independent Test**: Add a car, remove it, and verify the count returns to its previous state.

- [ ] T013 [US2] [OOP] Write failing Fact for removing an existing car instance in `src/CarSharp.Oop.Tests/FleetTests.cs`
- [ ] T014 [US2] [OOP] Implement `RemoveCar` method with mutable list removal in `src/CarSharp.Oop/Fleet.cs`
- [ ] T015 [US2] [Functional] Write failing Property for removing cars, including non-existent and null cases, in `src/CarSharp.Functional.Tests/FleetTests.cs`
- [ ] T016 [US2] [Functional] Implement `RemoveCar` extension using non-destructive mutation and contrast notes in `src/CarSharp.Functional/Fleet.cs`

## Phase 5: Polish & Educational Wrap-up

- [ ] T017 Write `comparison.md` analyzing architectural differences between the two tracks in `specs/001-phase-1-implementation/comparison.md`
- [ ] T018 [P] Verify all tests pass across both implementations with `dotnet test`
- [ ] T019 [P] Final review of comments and `ABOUTME` headers for educational consistency
- [ ] T020 [P] Verify SC-002 performance (count < 10ms for 10k cars) with comments explaining the O(1) complexity of the Count property in both paradigms
