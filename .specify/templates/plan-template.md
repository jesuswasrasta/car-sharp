# Implementation Plan: [FEATURE]

**Branch**: `[###-feature-name]` | **Date**: [DATE] | **Spec**: [link]
**Input**: Feature specification from `/specs/[###-feature-name]/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command.

## Summary

[Extract from feature spec: primary requirement + comparison goals]

## Technical Context

**Languages**: C# 12 (.NET 10), F# 8 (.NET 10)
**Testing**: xUnit (C#), xUnit + FsCheck (F#)
**Phase Goal**: [Which Phase (1-10) is being implemented?]

## Constitution Check

*GATE: Must pass before Phase 0 research.*
1. Dual Implementation (C# & F#)?
2. Narrative TDD approach planned?
3. Proper testing strategies (Facts vs Properties) defined?

## Project Structure

### Documentation (this phase)

```text
specs/phase-[N]/
├── plan.md              # This file
├── slides-intro.md      # Educational intro text/slides
└── comparison.md        # Post-implementation comparison notes
```

### Source Code

```text
src/
├── CarSharp.CS/        # C# Implementation (OOP/Mutable)
├── CarSharp.FS/        # F# Implementation (Functional/Immutable)
└── CarSharp.Domain/    # (Optional) Shared contract types if strictly necessary

tests/
├── CarSharp.CS.Tests/  # xUnit Facts
└── CarSharp.FS.Tests/  # xUnit + FsCheck Properties
```

## Complexity Tracking

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| [e.g., Shared Project] | [Reason] | [Reason] |