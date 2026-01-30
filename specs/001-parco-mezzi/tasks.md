# Task: Fase 1 - Gestione Parco Mezzi Minimale

**Input**: Documenti di progettazione da `/specs/001-parco-mezzi/`
**Prerequisiti**: plan.md, spec.md, research.md, data-model.md

**Skill (Costituzione Principio I)**: 
- OOP Tasks: `csharp-OOP-developer`
- FP Tasks: `csharp-FP-developer`

**Commenti (Costituzione Principio VII)**: 
- Ogni test e implementazione DEVE includere commenti discorsivi in italiano (no prefix `//Perché:`).

## Path Conventions
- OOP: `src/CarSharp.Oop/`, `src/CarSharp.Oop.Tests/`
- FP: `src/CarSharp.Functional/`, `src/CarSharp.Functional.Tests/`

---

## Fase 1: Setup e Preparazione Didattica

- [x] T001 Scrivere le slide introduttive didattiche in `specs/001-parco-mezzi/slides-intro.md`
- [x] T002 Aggiornare il `README.md` per riflettere i progressi della Fase 1
- [x] T003 [P] Verificare pacchetto `FsCheck.Xunit` in `src/CarSharp.Functional.Tests/`
- [x] T004 [P] Verificare pacchetto `System.Collections.Immutable` in `src/CarSharp.Functional/`

## Fase 2: Fondamenta (Entità Comuni)

- [x] T005 [P] [US1] Implementare la classe `Auto` in `src/CarSharp.Oop/Auto.cs`
- [x] T006 [P] [US1] Implementare il record `Auto` in `src/CarSharp.Functional/Auto.cs`

## Fase 3: User Story 1 - Popolamento Base [US1] 🎯 MVP

- [x] T007 [US1] [OOP] RED: Fact stato iniziale vuoto in `src/CarSharp.Oop.Tests/ParcoMezziTests.cs`
- [x] T008 [US1] [OOP] GREEN: Classe `ParcoMezzi` con `TotaleAuto`
- [x] T009 [US1] [OOP] RED: Fact aggiunta auto
- [x] T010 [US1] [OOP] GREEN: Metodo `AggiungiAuto` (mutazione)
- [x] T011 [US1] [FP] RED: Property stato iniziale e aggiunta in `src/CarSharp.Functional.Tests/ParcoMezziTests.cs`
- [x] T012 [US1] [FP] GREEN: Record `ParcoMezzi` e estensione `AggiungiAuto` (immutabilità)

## Fase 4: User Story 2 - Manutenzione Base [US2]

- [x] T013 [US2] [OOP] RED: Fact rimozione auto esistente
- [x] T014 [US2] [OOP] GREEN: Metodo `RimuoviAuto` (rimozione da lista mutabile)
- [x] T015 [US2] [FP] RED: Property rimozione auto
- [x] T016 [US2] [FP] GREEN: Estensione `RimuoviAuto` (non-destructive mutation)

## Fase 5: Rifinitura e Conclusione

- [ ] T017 Creare `comparison.md` con analisi architetturale
- [ ] T018 Eseguire regressione `dotnet test`
- [ ] T019 Revisione narrativa dei commenti (Costituzione v2.2.0)
- [ ] T020 Validazione performance SC-002

**Checkpoint**: ✅ Fase 1 completata e allineata agli standard qualitativi della Fase 7.