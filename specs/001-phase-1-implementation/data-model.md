# Modello dei Dati: Fase 1 - Gestione Parco Mezzi Minimale

**Stato**: Fase 1 (Entità di Base)

## Panoramica
In questa fase, il modello dei dati è ridotto all'essenziale per dimostrare la gestione della collezione.

## Entità

### Auto
Rappresenta un singolo veicolo.
- **Identità (OOP)**: Identità basata sull'istanza (riferimento).
- **Identità (Funzionale)**: Uguaglianza basata sul valore.
- **Proprietà**: Nessuna (verranno aggiunte nelle fasi successive).

### ParcoMezzi
Una collezione di Auto.
- **OOP**: Una classe che incapsula una `List<Auto>`.
- **Funzionale**: Un record che contiene una `ImmutableList<Auto>`.

## Relazioni
- Un `ParcoMezzi` contiene 0 o più `Auto`.
