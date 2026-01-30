# Implementation Plan: Prezzi Base (Fase 6)

**Branch**: `006-prezzi-base` | **Date**: 2026-01-30 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/006-prezzi-base/spec.md`

## Summary

Questa fase introduce il concetto di **costo giornaliero** per i mezzi nel sistema di car rental. Ogni auto avrà un prezzo base, e il sistema dovrà garantire l'invariante commerciale: mezzi con maggiore capacità non possono costare meno di mezzi più piccoli. L'implementazione estende sia il track OOP che quello Functional, mantenendo il contrasto paradigmatico.

## Technical Context

**Language/Version**: C# 13.0 / .NET 10.0
**Primary Dependencies**: xUnit, FsCheck.Xunit (property-based testing)
**Storage**: N/A (in-memory domain library)
**Testing**: xUnit con FsCheck per FP, Example-Based per OOP
**Target Platform**: Cross-platform .NET library
**Project Type**: Dual-paradigm educational library
**Performance Goals**: N/A (progetto educativo)
**Constraints**: Nessuno specifico; focus su chiarezza e contrasto paradigmatico
**Scale/Scope**: Single-feature phase (Fase 6 di 10)

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

| Principio | Status | Note |
|-----------|--------|------|
| I. Comparative Implementation | ✅ PASS | Implementazione parallela OOP e FP prevista |
| II. Phased Narrative Structure | ✅ PASS | Fase 6 segue logicamente Fase 5 |
| III. Test-Driven Development | ✅ PASS | TDD con commit granulari previsto |
| IV. Paradigm-Specific Testing | ✅ PASS | Example-Based (OOP) + Property-Based (FP) |
| V. Incremental Phased Delivery | ✅ PASS | Fase 5 completata, Fase 6 pronta |
| VI. Architectural Contrast | ✅ PASS | Exceptions (OOP) vs Result types (FP) |
| VII. Learning Purpose | ✅ PASS | Commenti esplicativi previsti |
| VIII. Language (Italian) | ✅ PASS | Dominio e commenti in italiano |
| IX. Git Strategy | ✅ PASS | Branch dedicato, merge + tag a fine fase |
| X. Documentation | ✅ PASS | README.md aggiornato a fine fase |

## Project Structure

### Documentation (this feature)

```text
specs/006-prezzi-base/
├── spec.md              # Feature specification
├── plan.md              # This file
├── research.md          # Phase 0 output
├── data-model.md        # Phase 1 output
├── quickstart.md        # Phase 1 output
├── contracts/           # Phase 1 output (API summary)
│   └── api-summary.md
├── checklists/
│   └── requirements.md
└── tasks.md             # Phase 2 output (/speckit.tasks)
```

### Source Code (repository root)

```text
src/
├── CarSharp.Oop/
│   ├── Auto.cs              # + CostoGiornaliero property
│   ├── ParcoMezzi.cs        # + validazione invariante + return costi
│   ├── RichiestaNoleggio.cs # (esistente)
│   ├── RisultatoNoleggio.cs # NUOVO: contiene auto + costo
│   └── StatoAuto.cs         # (esistente)
├── CarSharp.Oop.Tests/
│   └── ParcoMezziTests.cs   # + test prezzi e invariante
├── CarSharp.Functional/
│   ├── Auto.cs              # + CostoGiornaliero in IAuto/records
│   ├── ParcoMezzi.cs        # + return Result con costo
│   ├── RichiestaNoleggio.cs # (esistente)
│   ├── RisultatoNoleggio.cs # NUOVO: record con auto + costo
│   └── Result.cs            # (esistente)
└── CarSharp.Functional.Tests/
    └── ParcoMezziTests.cs   # + property tests su invariante
```

**Structure Decision**: Struttura esistente preservata. Nuovi tipi `RisultatoNoleggio` aggiunti in entrambi i track per incapsulare auto + costo.

## Complexity Tracking

> Nessuna violazione della constitution da giustificare.
