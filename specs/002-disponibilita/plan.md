# Implementation Plan: Fase 2 – Disponibilità e Identità

**Branch**: `002-disponibilita` | **Date**: 2026-01-13 | **Spec**: [spec.md](./spec.md)
**Input**: Introduzione dello stato dell'auto (Disponibile/Noleggiata) e dell'identità tecnica (GUID) distinta dalla Targa.

## Summary
L'obiettivo è evolvere l'entità `Auto` aggiungendo un'identità tecnica stabile (`Guid`) e una proprietà di dominio (`Targa`), gestendo al contempo lo stato operativo. In OOP useremo l'incapsulamento e le eccezioni; in Funzionale useremo record e ROP.

## Technical Context
**Languages**: C# 12 (.NET 10.0)
**Testing**: xUnit (OOP - Facts), xUnit + FsCheck.Xunit (Functional - Properties)
**Phase Goal**: Fase 2 - Gestione Stato e Identità.

## Constitution Check
*GATE: Must pass before Phase 0 research.*
1. Dual Implementation (C# OOP vs C# Functional)? **SÌ**
2. Narrative TDD approach planned? **SÌ**
3. Proper testing strategies (Facts vs Properties) defined? **SÌ**

## Project Structure

### Documentation (this phase)
```text
specs/002-disponibilita/
├── plan.md              # Questo file
├── spec.md              # Specifica funzionale (GUID/Targa inclusi)
├── research.md          # Decisioni tecniche sull'identità
├── data-model.md        # Modello dati dettagliato
├── tasks.md             # Elenco task TDD
└── contracts/
    └── api-summary.md   # Firme dei metodi
```

### Source Code
- `CarSharp.Oop`: Aggiornamento classe `Auto` (GUID, Targa, Stato mutabile).
- `CarSharp.Functional`: Aggiornamento record `Auto` (GUID, Targa, Stato immutabile).

## Complexity Tracking
| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| Doppio ID (GUID + Targa) | Architettura corretta | Usare solo la targa causa instabilità referenziale |
| Pattern ROP | Requisito FP | Coerenza con l'approccio funzionale moderno |
