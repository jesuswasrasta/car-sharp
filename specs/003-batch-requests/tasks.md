# Tasks: Batch Requests

**Feature Branch**: `003-batch-requests`
**Spec**: [spec.md](./spec.md)

## Phase 1: Setup & Prerequisites
*Goal: Ensure project structure is ready for Phase 3 implementation.*

- [x] T001 Verify project state and clean build artifacts
- [x] T002 Ensure test runners (xUnit) are operational for both Oop and Functional projects

## Phase 2: User Story 1 - Atomic Processing of Valid Batch (Priority: P1)
*Goal: Implement the happy path where a valid batch of requests results in all vehicles being rented.*
*Independent Test: Submit a batch of IDs for available cars; verify all transition to Rented.*

### OOP Track (Example-Based TDD)
- [x] T003 [US1] Create test `NoleggiaBatch_ConAutoDisponibili_DovrebbeNoleggiarleTutte` in src/CarSharp.Oop.Tests/ParcoMezziTests.cs
- [x] T004 [US1] Implement `NoleggiaBatch` basic loop logic in src/CarSharp.Oop/ParcoMezzi.cs

### Functional Track (Property-Based TDD)
- [x] T005 [P] [US1] Create property test `NoleggiaBatch_ConAutoDisponibili_DovrebbeRitornareNuovoStato` in src/CarSharp.Functional.Tests/ParcoMezziTests.cs
- [x] T006 [US1] Implement `NoleggiaBatch` logic using state transformation in src/CarSharp.Functional/ParcoMezzi.cs

## Phase 3: User Story 2 - Atomic Rejection of Invalid Batch (Priority: P1)
*Goal: Ensure atomicity by rejecting the entire batch if any request is invalid (unavailable, non-existent, duplicate).*
*Independent Test: Submit a batch with one rented car; verify exception/error and NO state change.*

### OOP Track (Example-Based TDD)
- [x] T007 [US2] Create test `NoleggiaBatch_ConAutoNonDisponibile_DovrebbeLanciareEccezioneENonModificareStato` in src/CarSharp.Oop.Tests/ParcoMezziTests.cs
- [x] T008 [US2] Create test `NoleggiaBatch_ConDuplicati_DovrebbeLanciareEccezione` in src/CarSharp.Oop.Tests/ParcoMezziTests.cs
- [x] T009 [US2] Refactor `NoleggiaBatch` to add Check-Then-Act validation in src/CarSharp.Oop/ParcoMezzi.cs

### Functional Track (Property-Based TDD)
- [x] T010 [P] [US2] Create property test `NoleggiaBatch_ConAutoNonDisponibile_DovrebbeRitornareErrore` in src/CarSharp.Functional.Tests/ParcoMezziTests.cs
- [x] T011 [P] [US2] Create property test `NoleggiaBatch_ConDuplicati_DovrebbeRitornareErrore` in src/CarSharp.Functional.Tests/ParcoMezziTests.cs
- [x] T012 [US2] Refactor `NoleggiaBatch` to propagate errors and handle duplicates in src/CarSharp.Functional/ParcoMezzi.cs

## Phase 4: Polish & Documentation
*Goal: Finalize the phase with documentation and comparisons as per Constitution.*

- [x] T013 Update `README.md` with Phase 3 recap and paradigm comparison details
- [x] T014 Create/Update `specs/003-batch-requests/comparison.md` with technical analysis of Check-Then-Act vs State Transformation
- [x] T015 Run full regression suite `dotnet test` to ensure no regressions

## Dependencies
- Phase 3 (US2) depends on Phase 2 (US1) logic being present.
- OOP and Functional tracks can be executed in parallel within each phase.

## Implementation Strategy
- Follow Strict Narrative TDD: Red -> Green -> Refactor.
- Comments in code MUST explain the *why* for the educational audience (Italian).
- Commit often (at least once per task).
