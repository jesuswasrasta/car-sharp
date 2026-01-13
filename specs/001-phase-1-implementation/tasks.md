# Task: Fase 1 - Gestione Parco Mezzi Minimale

**Input**: Documenti di progettazione da `/specs/001-phase-1-implementation/`
**Prerequisiti**: plan.md, spec.md, research.md, data-model.md

## Strategia di Implementazione

Seguiamo un approccio TDD narrativo per scopi didattici.
- **Granularità**: Esattamente un commit per ogni passaggio TDD (Red -> Green -> Refactor).
- **Focus sull'Apprendimento**: Ogni file deve includere gli header `ABOUTME`. Ogni test e implementazione deve includere commenti inline che spieghino il *perché*.
- **Contrasto tra Paradigmi**: Le implementazioni funzionali devono includere note specifiche che confrontino l'approccio con la versione OOP.
- **MVP**: Completamento della User Story 1 (Popolamento Base) in entrambi i percorsi.

## Grafo delle Dipendenze

```text
Setup -> Fondamenta -> US1 (Popolamento) -> US2 (Manutenzione) -> Rifinitura
```

## Fase 1: Setup e Preparazione Didattica

- [x] T001 Scrivere le slide introduttive didattiche in `specs/001-phase-1-implementation/slides-intro.md`
- [x] T002 Aggiornare il `README.md` per riflettere i progressi della Fase 1 e gli obiettivi di apprendimento
- [x] T003 [P] Verificare che il pacchetto NuGet `FsCheck.Xunit` sia presente in `src/CarSharp.Functional.Tests/CarSharp.Functional.Tests.csproj`
- [x] T004 [P] Verificare che il pacchetto NuGet `System.Collections.Immutable` sia presente in `src/CarSharp.Functional/CarSharp.Functional.csproj`

## Fase 2: Fondamenta (Entità Comuni)

- [x] T005 [P] Implementare la classe `Auto` con `ABOUTME` e commenti didattici in `src/CarSharp.Oop/Auto.cs`
- [x] T006 [P] Implementare il record `Auto` con `ABOUTME` e commenti didattici in `src/CarSharp.Functional/Auto.cs`

## Fase 3: User Story 1 - Popolamento Base del Parco Mezzi [US1]

**Obiettivo**: Consentire la creazione del parco mezzi e l'aggiunta di auto per tracciare l'inventario totale.
**Test Indipendente**: Verificare che il conteggio del parco mezzi vuoto sia 0; verificare che l'aggiunta di $N$ auto risulti in un conteggio $N$.

- [x] T007 [US1] [OOP] Scrivere un Fact fallimentare per lo stato iniziale del parco mezzi vuoto in `src/CarSharp.Oop.Tests/ParcoMezziTests.cs`
- [x] T008 [US1] [OOP] Implementare la classe `ParcoMezzi` con la proprietà `TotaleAuto` in `src/CarSharp.Oop/ParcoMezzi.cs`
- [x] T009 [US1] [OOP] Scrivere un Fact fallimentare per l'aggiunta di un'auto al parco mezzi in `src/CarSharp.Oop.Tests/ParcoMezziTests.cs`
- [x] T010 [US1] [OOP] Implementare il metodo `AggiungiAuto` con stato mutabile in `src/CarSharp.Oop/ParcoMezzi.cs`
- [x] T011 [US1] [Funzionale] Scrivere una Proprietà fallimentare per lo stato iniziale, l'aggiunta base e il grande volume (10k) in `src/CarSharp.Functional.Tests/ParcoMezziTests.cs`
- [x] T012 [US1] [Funzionale] Implementare il record `ParcoMezzi` e l'estensione `AggiungiAuto` con immutabilità e note di contrasto in `src/CarSharp.Functional/ParcoMezzi.cs`

## Fase 4: User Story 2 - Manutenzione Base del Parco Mezzi [US2]

**Obiettivo**: Consentire la rimozione delle auto dall'inventario del parco mezzi.
**Test Indipendente**: Aggiungere un'auto, rimuoverla e verificare che il conteggio torni allo stato precedente.

- [x] T013 [US2] [OOP] Scrivere un Fact fallimentare per la rimozione di un'istanza di auto esistente in `src/CarSharp.Oop.Tests/ParcoMezziTests.cs`
- [x] T014 [US2] [OOP] Implementare il metodo `RimuoviAuto` con rimozione da lista mutabile in `src/CarSharp.Oop/ParcoMezzi.cs`
- [x] T015 [US2] [Funzionale] Scrivere una Proprietà fallimentare per la rimozione di auto, inclusi i casi inesistenti e null, in `src/CarSharp.Functional.Tests/ParcoMezziTests.cs`
- [x] T016 [US2] [Funzionale] Implementare l'estensione `RimuoviAuto` utilizzando la mutazione non distruttiva e note di contrasto in `src/CarSharp.Functional/ParcoMezzi.cs`

## Fase 5: Rifinitura e Conclusione Didattica

- [x] T017 Scrivere `comparison.md` analizzando le differenze architettoniche tra i due percorsi in `specs/001-phase-1-implementation/comparison.md`
- [x] T018 [P] Verificare che tutti i test passino in entrambe le implementazioni con `dotnet test`
- [x] T019 [P] Revisione finale dei commenti e degli header `ABOUTME` per la coerenza didattica
- [x] T020 [P] Verificare le prestazioni SC-002 (conteggio < 10ms per 10k auto) con commenti che spieghino la complessità O(1) della proprietà TotaleAuto in entrambi i paradigmi
