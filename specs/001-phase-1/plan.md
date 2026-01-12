# Implementation Plan: Phase 1 - Minimal Fleet

**Branch**: `001-phase-1` | **Date**: 2026-01-12 | **Spec**: [spec.md](spec.md)
**Input**: Educational Comparison Strategy for Phase 1

## Summary

Implement the "Minimal Fleet" requirements in both C# (OOP) and F# (Functional) to demonstrate the paradigm shift from mutable state and example-based testing to immutable state and property-based testing.

## Technical Context

**Languages**: C# 12 (.NET 10), F# 9 (.NET 10)
**Testing**: xUnit (C#), xUnit + FsCheck (F#)
**Phase Goal**: Phase 1 - Minimal Fleet (Add, Remove, Count)

## Constitution Check

*GATE: Must pass before Phase 0 research.*
1. Dual Implementation (C# & F#)? ✅
2. Narrative TDD approach planned? ✅ (Granular steps 1.1 - 1.11)
3. Proper testing strategies (Facts vs Properties) defined? ✅

## Project Structure

### Documentation (this phase)

```text
specs/001-phase-1/
├── plan.md              # This file
├── slides-intro.md      # Phase 1 Intro & Objectives
├── checklists/
│   └── requirements.md
└── comparison.md        # Comparison: Mutable vs Immutable, Facts vs Properties
```

### Source Code

```text
src/
├── CarSharp.CS/        # C# Project: Fleet (Class), Car (Class)
└── CarSharp/           # F# Project: Fleet (Type), Car (Type)

tests/
├── CarSharp.CS.Test/   # C# Tests: xUnit Facts
└── CarSharp.Test/      # F# Tests: xUnit + FsCheck Properties
```

## Detailed Implementation Steps (from NOTES.md)

### Phase 1: Preparation
- **T001**: Prepare `slides-intro.md` with Phase 1 context and learning objectives.

### Phase 2: C# Session (OOP Classic)
- **Step 1.1**: TDD Red - `NewFleet_Should_BeEmpty` (Fact).
- **Step 1.2**: TDD Green/Refactor - Impl `Fleet` class with private `List<Car>` and `Count` property.
- **Step 1.3**: TDD Red - `Add_Should_IncrementCount` (Fact).
- **Step 1.4**: TDD Green - Impl `void Add(Car car)` (Mutable side-effect).
- **Step 1.5**: TDD Red - `Remove_ExistingCar_Should_DecrementCount` (Fact).
- **Step 1.6**: TDD Green - Impl `bool Remove(Car car)`.

### Phase 3: F# Session (Functional & Property Based)
- **Step 1.7**: TDD Red/Green - Define `Fleet` and `Car` types. Fact: `empty fleet has zero count`.
- **Step 1.8**: TDD Red - Property: `adding a car increments count` (FsCheck).
- **Step 1.9**: TDD Green - Impl `add: Car -> Fleet -> Fleet` (Immutable transformation).
- **Step 1.10**: TDD Red - Property: `removing a car previously added returns original count` (Invariance/Reversibility).
- **Step 1.11**: TDD Green - Impl `remove: Car -> Fleet -> Result<Fleet, Error>` (Result pattern).

### Phase 4: Wrap-up
- **T002**: Prepare `comparison.md` side-by-side analysis.

## Complexity Tracking

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| Dual Track | Educational presentation requires contrasting paradigms. | Single implementation lacks comparative value. |
| Result Pattern (Phase 1) | Introduce Railway Oriented Programming early in the narrative. | Using simple option or bool would delay key functional concept. |
