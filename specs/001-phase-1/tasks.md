# Tasks: Phase 1 - Minimal Fleet

**Input**: Design documents from `/specs/001-phase-1/`
**Prerequisites**: plan.md, spec.md

## Phase 1: Educational Prep

- [x] T001 Write `slides-intro.md`: Phase 1 Context and Learning Objectives
- [x] T002 Update `README.md` with Phase 1 status

## Phase 2: C# Implementation (OOP/Mutable)

**Goal**: Implement Phase 1 logic using idiomatic C# and xUnit Facts.

- [x] T010 [C#] Setup `CarSharp.CS` project and `CarSharp.CS.Test`
- [x] T011 [C#] Step 1.1: TDD Red - `NewFleet_Should_BeEmpty` Fact
- [x] T012 [C#] Step 1.2: TDD Green/Refactor - Impl `Fleet` class with mutable `List<Car>`
- [x] T013 [C#] Step 1.3: TDD Red - `Add_Should_IncrementCount` Fact
- [x] T014 [C#] Step 1.4: TDD Green - Impl `void Add(Car car)` (State mutation)
- [x] T015 [C#] Step 1.5: TDD Red - `Remove_ExistingCar_Should_DecrementCount` Fact
- [x] T016 [C#] Step 1.6: TDD Green - Impl `bool Remove(Car car)`

## Phase 3: F# Implementation (Functional/Immutable)

**Goal**: Implement Phase 1 logic using idiomatic F# and FsCheck Properties.

- [x] T020 [F#] Step 1.7: TDD Red/Green - Define `Fleet` and `Car` types; Fact for zero count
- [x] T021 [F#] Step 1.8: TDD Red - Property: `adding a car increments count`
- [x] T022 [F#] Step 1.9: TDD Green - Impl `add: Car -> Fleet -> Fleet` (Immutable transformation)
- [x] T023 [F#] Step 1.10: TDD Red - Property: `removing a car previously added returns original count`
- [x] T024 [F#] Step 1.11: TDD Green - Impl `remove: Car -> Fleet -> Result<Fleet, Error>` (Railway Oriented)

## Phase 4: Educational Wrap-up

- [x] T030 Write `comparison.md`: Side-by-side analysis (Mutable vs Immutable, etc.)
- [x] T031 Final Review: Verify TDD commit narrative and comments
