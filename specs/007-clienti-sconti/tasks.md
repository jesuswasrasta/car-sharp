# Tasks: Clienti e Sconti

**Feature**: `007-clienti-sconti`
**Status**: Todo

## Phase 1: Setup
*Goal: Ensure environment is ready for Phase 7 implementation.*

- [ ] T001 Verify .NET 10 SDK availability and C# 14 compatibility check

## Phase 2: Foundational (Data Model)
*Goal: Update core data structures to support ClientId and Batch Results. These changes are prerequisites for all User Stories.*

- [ ] T002 [P] [OOP] Update `RichiestaNoleggio` class to include `ClienteId` property and update constructor in `src/CarSharp.Oop/RichiestaNoleggio.cs`
- [ ] T003 [P] [OOP] Update `RisultatoNoleggio` class to include `ClienteId` property in `src/CarSharp.Oop/RisultatoNoleggio.cs`
- [ ] T004 [P] [FP] Update `RichiestaNoleggio` record to include `ClienteId` property in `src/CarSharp.Functional/RichiestaNoleggio.cs`
- [ ] T005 [P] [FP] Update `RisultatoNoleggio` record to include `ClienteId` property in `src/CarSharp.Functional/RisultatoNoleggio.cs`
- [ ] T006 [P] [OOP] Create `DettaglioCostiCliente` class/struct in `src/CarSharp.Oop/RisultatoBatch.cs` (or new file)
- [ ] T007 [P] [FP] Create `DettaglioCliente` record in `src/CarSharp.Functional/RisultatoBatch.cs` (or new file)
- [ ] T008 [P] [OOP] Update `RisultatoBatch` class to include `RiepilogoClienti` and `TotaleGenerale` in `src/CarSharp.Oop/RisultatoBatch.cs`
- [ ] T009 [P] [FP] Update `RisultatoBatch` record to include `RiepilogoClienti` and `TotaleGenerale` in `src/CarSharp.Functional/RisultatoBatch.cs`

## Phase 3: User Story 1 - Associazione prenotazione a cliente (P1)
*Goal: Ensure every booking is linked to a client ID.*

- [ ] T010 [OOP] [US1] Update `ParcoMezzi.Prenota` logic to propagate `ClienteId` from request to result in `src/CarSharp.Oop/ParcoMezzi.cs`
- [ ] T011 [FP] [US1] Update `ParcoMezzi.Prenota` logic to propagate `ClienteId` from request to result in `src/CarSharp.Functional/ParcoMezzi.cs`
- [ ] T012 [OOP] [US1] Add test for `ClienteId` propagation in `src/CarSharp.Oop.Tests/ParcoMezziTests.cs`
- [ ] T013 [FP] [US1] Add property test for `ClienteId` preservation in `src/CarSharp.Functional.Tests/ParcoMezziTests.cs`

## Phase 4: User Story 2 - Sconto percentuale per prenotazioni multiple (P2)
*Goal: Apply percentage discount for clients with multiple bookings in a batch.*

- [ ] T014 [OOP] [US2] Update `ParcoMezzi.PrenotaBatch` signature to accept `scontoPercentuale` in `src/CarSharp.Oop/ParcoMezzi.cs`
- [ ] T015 [FP] [US2] Update `ParcoMezzi.PrenotaBatch` function signature to accept `scontoPercentuale` in `src/CarSharp.Functional/ParcoMezzi.cs`
- [ ] T016 [OOP] [US2] Implement logic to group by client, count bookings, and apply discount in `src/CarSharp.Oop/ParcoMezzi.cs`
- [ ] T017 [FP] [US2] Implement logic to group by client, count bookings, and apply discount in `src/CarSharp.Functional/ParcoMezzi.cs`
- [ ] T018 [OOP] [US2] Add tests for multi-booking discount and single-booking exclusion in `src/CarSharp.Oop.Tests/ParcoMezziTests.cs`
- [ ] T019 [FP] [US2] Add property test verifying discount condition (count > 1) in `src/CarSharp.Functional.Tests/ParcoMezziTests.cs`

## Phase 5: User Story 3 - Prezzo finale non negativo (P3)
*Goal: Ensure final price is never negative (floor at 0).*

- [ ] T020 [OOP] [US3] Implement clamp `Math.Max(0, total)` in cost calculation logic in `src/CarSharp.Oop/ParcoMezzi.cs`
- [ ] T021 [FP] [US3] Implement clamp `Math.Max(0, total)` in cost calculation logic in `src/CarSharp.Functional/ParcoMezzi.cs`
- [ ] T022 [OOP] [US3] Add test case with >100% discount to verify non-negative price in `src/CarSharp.Oop.Tests/ParcoMezziTests.cs`
- [ ] T023 [FP] [US3] Add property test verifying price invariant (>= 0) in `src/CarSharp.Functional.Tests/ParcoMezziTests.cs`
- [ ] T026 [OOP/FP] Verify discount range validation (0-100%) in tests.

## Phase 6: Polish & Cross-Cutting
*Goal: Finalize documentation and verify quality.*

- [ ] T024 Update `README.md` with Phase 7 recap and examples
- [ ] T025 Run full test suite and verify no regressions

## Implementation Strategy
- **MVP**: Complete Phase 2 and Phase 3 (US1) first. This enables client tracking without logic complexity.
- **Incremental**: Add US2 (Discounts) logic on top of the established data model.
- **Safety**: US3 is a safety guard that can be added last.
- **Parallelism**: OOP and FP tracks can be implemented in parallel for each task if multiple developers are available.

## Dependencies
- US2 depends on US1 (needs ClientId to group).
- US3 depends on US2 (needs discount calculation to potentially go negative).
