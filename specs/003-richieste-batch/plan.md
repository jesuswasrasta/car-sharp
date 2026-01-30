# Piano di Implementazione: Richieste Batch (Fase 3)

**Branch**: `003-richieste-batch` | **Data**: 2026-01-13 | **Spec**: [spec.md](./spec.md)
**Input**: Requisiti della Fase 3 - Gestione atomica di richieste multiple.

## Sintesi
L'obiettivo di questa fase è implementare l'elaborazione di batch di noleggio. Il sistema deve garantire l'atomicità: o tutte le auto del batch vengono noleggiate, o nessuna. Questo permette di confrontare la gestione delle transazioni in-memory tra OOP (mutazione controllata) e FP (trasformazione di stato tramite Result).

## Contesto Tecnico
**Linguaggio**: C# 12 (.NET 10)
**Librerie**: xUnit (Test), FsCheck.Xunit (Property-Based Testing per FP)
**Obiettivo Fase**: Fase 3 - Richieste e Batch

## Constitution Check
*GATING: Deve passare prima di procedere.*
1. **Doppia Implementazione (C# OOP & C# FP)?** Sì, entrambe in C# come da Costituzione v2.2.0.
2. **Approccio Narrative TDD pianificato?** Sì, un commit per ogni step Rosso/Verde.
3. **Strategie di test differenziate?** Sì, Example-Based (OOP) vs Property-Based (FP).

## Struttura Progetto

### Documentazione
- `plan.md`: Questo documento.
- `research.md`: Decisioni architetturali sull'atomicità.
- `data-model.md`: Definizione delle entità e transizioni.
- `contracts/api-summary.md`: Firme dei metodi per entrambi i paradigmi.

### Codice Sorgente
- `src/CarSharp.Oop/`: Implementazione con classi e stato mutabile.
- `src/CarSharp.Functional/`: Implementazione con record, immutabilità e Result.

## Complexity Tracking

| Violazione | Perché Necessaria | Alternativa Rifiutata |
|-----------|------------------|----------------------|
| Nessuna | | |
