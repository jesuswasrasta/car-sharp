# Piano di Implementazione: Fase 1 - Gestione Parco Mezzi Minimale

**Branch**: `001-parco-mezzi` | **Data**: 12-01-2026 | **Spec**: [/home/nando/Source/Github/jesuswasrasta/car-sharp/specs/001-parco-mezzi/spec.md]
**Input**: Specifica della funzionalità da `/specs/001-parco-mezzi/spec.md`

## Sintesi

Implementare un sistema di gestione del parco mezzi minimale confrontando gli approcci OOP (stato mutabile) e Funzionale (stato immutabile) in C# 12. Focus sulla chiarezza educativa e sul contrasto tra paradigmi.

## Contesto Tecnico

**Linguaggi**: C# 12 (.NET 10.0)
**Testing**: xUnit (OOP Facts), xUnit + FsCheck (Proprietà Funzionali)
**Obiettivo della Fase**: Fase 1 - Gestione Parco Mezzi Minimale

## Controllo Costituzione

*GATE: Deve passare prima della ricerca della Fase 0.*
1. Doppia implementazione (OOP vs Funzionale in C#)? **SÌ**
2. Approccio TDD narrativo pianificato? **SÌ**
3. Strategie di testing appropriate (Facts vs Properties) definite? **SÌ**
4. Scopo didattico (commenti e note di contrasto) incluso? **SÌ**

## Struttura del Progetto

### Documentazione (questa fase)

```text
specs/001-parco-mezzi/
├── plan.md              # Questo file
├── spec.md              # Specifica della funzionalità
├── research.md          # Decisioni tecniche e ricerca
├── data-model.md        # Definizioni delle entità
├── contracts/           # Sintesi delle API
├── quickstart.md        # Esempi di utilizzo
├── slides-intro.md      # Testo/slide introduttive didattiche
└── comparison.md        # Note di confronto post-implementazione (TODO)
```

### Codice Sorgente

```text
src/
├── CarSharp.Oop/           # Implementazione OOP
├── CarSharp.Functional/    # Implementazione Funzionale
├── CarSharp.Oop.Tests/     # Test OOP (Facts)
└── CarSharp.Functional.Tests/ # Test Funzionali (Proprietà FsCheck)
```

## Tracciamento della Complessità

| Violazione | Perché è necessaria | Alternativa più semplice rifiutata perché |
|-----------|------------|-------------------------------------|
| Nessuna | N/A | N/A |