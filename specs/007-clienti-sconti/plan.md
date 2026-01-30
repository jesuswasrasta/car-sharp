# Implementation Plan: Clienti e Sconti

**Branch**: `007-clienti-sconti` | **Date**: 2026-01-30 | **Spec**: [specs/007-clienti-sconti/spec.md](spec.md)
**Input**: Feature specification from `/specs/007-clienti-sconti/spec.md`

## Summary

Implementazione della gestione clienti e sconti per batch.
Ogni `RichiestaNoleggio` sarà associata a un `ClienteId`.
Il processamento batch (`PrenotaBatch` o equivalente) accetterà una percentuale di sconto.
Se un cliente ha >1 prenotazione nel batch, il totale per quel cliente sarà scontato.
Il sistema garantirà che il prezzo finale non sia mai negativo.

## Technical Context

**Language/Version**: C# (.NET 10.0)
**Primary Dependencies**: xUnit (Testing), FsCheck.Xunit (Property-based Testing)
**Storage**: In-memory (State immutabile per FP, Stato incapsulato per OOP)
**Testing**:
- OOP: xUnit `[Fact]` per scenari specifici (Example-Based).
- FP: FsCheck `[Property]` per invarianti (prezzo >= 0, sconto applicato correttamente).
**Target Platform**: .NET 10.0 Library
**Project Type**: Library (Backend logic comparison)
**Performance Goals**: N/A (Focus educativo)
**Constraints**:
- Dual Implementation (OOP & FP)
- Strict TDD (Red-Green-Refactor)
- Italiano per codice di dominio e commenti
- Prezzo non negativo

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- [x] **I. Comparative Strategy**: OOP and FP implementations planned.
- [x] **II. Phases**: Following Phase 7 requirements.
- [x] **III. TDD**: Will follow strict TDD cycle.
- [x] **IV. Paradigm Testing**: xUnit for OOP, FsCheck for FP.
- [x] **V. Incremental**: Builds on Phase 6.
- [x] **VI. Arch Contrast**: Exceptions/State vs Result/Immutability.
- [x] **VII. Learning**: Comments will explain "Why".
- [x] **VIII. Language**: Italian for domain.
- [x] **IX. Git**: Feature branch `007-clienti-sconti`.
- [x] **X. Documentation**: README update planned.

## Project Structure

### Documentation (this feature)

```text
specs/007-clienti-sconti/
├── plan.md              # This file
├── research.md          # Phase 0 output
├── data-model.md        # Phase 1 output
├── quickstart.md        # Phase 1 output
├── contracts/           # Phase 1 output
│   └── api-summary.md
└── tasks.md             # Phase 2 output
```

### Source Code (repository root)

```text
src/
├── CarSharp.Oop/               # Implementation OOP
│   ├── Auto.cs
│   ├── ParcoMezzi.cs
│   ├── RichiestaNoleggio.cs
│   ├── RisultatoNoleggio.cs
│   └── ...
├── CarSharp.Functional/        # Implementation Functional
│   ├── Auto.cs
│   ├── ParcoMezzi.cs
│   ├── RichiestaNoleggio.cs
│   ├── RisultatoNoleggio.cs
│   └── ...
├── CarSharp.Oop.Tests/         # Tests OOP
└── CarSharp.Functional.Tests/  # Tests Functional
```

**Structure Decision**: Standard Library layout (Option 1).

## Complexity Tracking

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| None | | |