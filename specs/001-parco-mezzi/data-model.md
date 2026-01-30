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

```text
ParcoMezzi (Aggregato)
   └── 1 : N ──► Auto (Entità/Valore)
```

## Invarianti di Dominio

| ID | Invariante | Descrizione |
|----|------------|-------------|
| INV-001 | Conteggio non negativo | `TotaleAuto` deve essere sempre >= 0. |
| INV-002 | Unicità (OOP) | La stessa istanza di `Auto` non può essere aggiunta due volte (se gestito dalla collezione). |

## Transizioni di Stato

In questa fase, non esiste uno stato operativo interno all'auto (es. Disponibile/Noleggiata), quindi le transizioni riguardano solo l'appartenenza alla collezione del parco.

```text
[ParcoMezzi] ── AggiungiAuto(Auto) ──► [ParcoMezzi + 1]
[ParcoMezzi] ── RimuoviAuto(Auto)  ──► [ParcoMezzi - 1]
```
