# Tasks: Fase 4 - Tipologie e dimensioni dei mezzi

## Phase 1: Common Infrastructure & Data Model
*Goal: Update domain entities to support capacity.*

- [ ] T001 [OOP] Add `Capacita` property to `Auto.cs` and update constructor in src/CarSharp.Oop/Auto.cs
- [ ] T002 [FP] Add `Capacita` property to `Auto` record and specialized types in src/CarSharp.Functional/Auto.cs
- [ ] T003 [OOP] Update `ParcoMezzi.AggiungiAuto` to accept capacity in src/CarSharp.Oop/ParcoMezzi.cs
- [ ] T004 [FP] Update `ParcoMezzi.AggiungiAuto` to accept capacity in src/CarSharp.Functional/ParcoMezzi.cs

## Phase 2: Single Rental with Capacity (US1 & US2)
*Goal: Implement selection logic based on seats.*

### OOP Track
- [x] T005 [OOP] Create test `NoleggiaPerCapacita_DovrebbeAssegnareAutoIdonea` in src/CarSharp.Oop.Tests/AutoTests.cs
- [x] T006 [OOP] Implement `Noleggia(int postiMinimi)` logic in src/CarSharp.Oop/ParcoMezzi.cs
- [x] T007 [OOP] Create test for capacity failure scenario in src/CarSharp.Oop.Tests/AutoTests.cs

### Functional Track
- [x] T008 [FP] Create property test `NoleggioPerCapacita_DovrebbeSempreRispettareIlMinimo` in src/CarSharp.Functional.Tests/AutoTests.cs
- [x] T009 [FP] Implement `NoleggiaPerCapacita(int postiMinimi)` in src/CarSharp.Functional/ParcoMezzi.cs
- [x] T010 [FP] Refactor state transformation to use capacity-based lookup

## Phase 3: Batch Requests with Capacity (US3)
*Goal: Atomic batch processing with capacity constraints.*

- [x] T011 [OOP] Update `NoleggiaBatch` to handle capacity requirements in src/CarSharp.Oop/ParcoMezzi.cs
- [x] T012 [OOP] Add tests for atomic rejection due to capacity in src/CarSharp.Oop.Tests/ParcoMezziTests.cs
- [x] T013 [FP] Update `NoleggiaBatch` pipeline to support capacity requirements in src/CarSharp.Functional/ParcoMezzi.cs
- [x] T014 [FP] Add property tests for batch capacity constraints in src/CarSharp.Functional.Tests/ParcoMezziTests.cs

## Phase 4: Polish & Documentation
- [x] T015 Update `README.md` with Phase 4 completion
- [x] T016 Create `specs/004-car-dimensions/comparison.md` for paradigm analysis
- [x] T017 Final regression run `dotnet test`
