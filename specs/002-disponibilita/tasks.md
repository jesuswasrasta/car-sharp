# Task Granulari: Fase 2 – Disponibilità e Identità

**Input**: Documenti di progettazione da `/specs/002-disponibilita/`
**Prerequisiti**: plan.md, spec.md, research.md, data-model.md

**Skill (Costituzione Principio I)**: 
- OOP Tasks: `csharp-OOP-developer`
- FP Tasks: `csharp-FP-developer`

**Commenti (Costituzione Principio VII)**: 
- Ogni test e implementazione DEVE includere commenti discorsivi in italiano che spieghino il *perché* del paradigma.

## Path Conventions
- OOP: `src/CarSharp.Oop/`, `src/CarSharp.Oop.Tests/`
- FP: `src/CarSharp.Functional/`, `src/CarSharp.Functional.Tests/`

---

## Fase 1: Setup & Definizioni

- [ ] T001 **OOP**: Definizione enum `StatoAuto` (Disponibile, Noleggiata)
- [ ] T002 **Functional**: Definizione dei tipi `AutoDisponibile` e `AutoNoleggiata`

## Fase 2: Percorso OOP (C# Classico)

- [ ] T003 [OOP] RED: Test costruttore `Auto` richiede Id e Targa
- [ ] T004 [OOP] GREEN: Implementazione proprietà `Id`, `Targa` e `Stato`
- [ ] T005 [OOP] RED: Test `Noleggia` cambia stato in `Noleggiata`
- [ ] T006 [OOP] GREEN: Implementazione metodo `Noleggia` con validazione (Exception)
- [ ] T007 [OOP] RED: Test `Restituisci` riporta lo stato a `Disponibile`
- [ ] T008 [OOP] GREEN: Implementazione metodo `Restituisci`
- [ ] T009 [OOP] RED: Test `TotaleDisponibili` nel parco mezzi
- [ ] T010 [OOP] GREEN: Implementazione logica di filtraggio in `ParcoMezzi.cs`

## Fase 3: Percorso Funzionale (C# Moderno) 🎯 MVP

- [ ] T011 [FP] RED: Property `Noleggia` trasforma `AutoDisponibile` in `AutoNoleggiata`
- [ ] T012 [FP] GREEN: Implementazione funzione `Noleggia` in `AutoExtensions.cs`
- [ ] T013 [FP] RED: Property `Restituisci` trasforma `AutoNoleggiata` in `AutoDisponibile`
- [ ] T014 [FP] GREEN: Implementazione funzione `Restituisci`
- [ ] T015 [FP] RED: Test `ConteggioDisponibili` conta solo istanze di `AutoDisponibile`
- [ ] T016 [FP] GREEN: Implementazione query LINQ con pattern matching

## Fase 4: Conclusione Didattica

- [ ] T017 Creare `comparison.md` analizzando Mutazione vs Transizione
- [ ] T018 Revisione narrativa di tutti i commenti per l'audience
- [ ] T019 Chiusura branch e creazione tag `002-disponibilita`

**Checkpoint**: ✅ Fase 2 completata e documentata secondo gli standard correnti.
