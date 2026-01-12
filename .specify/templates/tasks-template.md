---
description: "Task list template for feature implementation"
---

# Tasks: [PHASE NAME]

**Input**: Design documents from `/specs/phase-[N]/`
**Prerequisites**: plan.md, spec.md

## Phase 1: Educational Prep

- [ ] T001 Write `slides-intro.md`: Context and Learning Objectives
- [ ] T002 Update `README.md` with current phase status

## Phase 2: C# Implementation (OOP/Mutable)

**Goal**: Implement Phase [N] logic using idiomatic C# and xUnit Facts.

- [ ] T010 [C#] Setup/Refactor classes for Phase [N]
- [ ] T011 [C#] TDD Step 1: Red (Write failing Fact) -> [Description]
- [ ] T012 [C#] TDD Step 2: Green (Implement minimal logic)
- [ ] T013 [C#] TDD Step 3: Refactor (Clean up)
- [ ] T014 [C#] TDD Step 4: Red (Next scenario...)
- [ ] ... [Repeat for all requirements]

## Phase 3: F# Implementation (Functional/Immutable)

**Goal**: Implement Phase [N] logic using idiomatic F# and FsCheck Properties.

- [ ] T020 [F#] Setup/Refactor types for Phase [N]
- [ ] T021 [F#] TDD Step 1: Red (Write failing Property) -> [Description]
- [ ] T022 [F#] TDD Step 2: Green (Implement pure function)
- [ ] T023 [F#] TDD Step 3: Refactor (Clean up)
- [ ] T024 [F#] TDD Step 4: Red (Next invariant...)
- [ ] ... [Repeat for all requirements]

## Phase 4: Educational Wrap-up

- [ ] T030 Write `comparison.md`: Analyze code differences, pros/cons
- [ ] T031 Final Review: Verify narrative flow and commit history