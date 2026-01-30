# Modello Dati: Richieste Batch (Fase 3)

**Funzionalità**: Richieste Batch (Fase 3)
**Sorgente**: `spec.md`

## Relazioni

```text
Batch (Comando) ─── 1 : N ───► Richiesta (ID Auto)
                                  │
                                  ▼
ParcoMezzi (Stato) ── 1 : N ──► Auto
```

## Invarianti di Dominio

| ID | Invariante | Descrizione |
|----|------------|-------------|
| INV-001 | Atomicità Batch | O tutte le auto del batch passano a "Noleggiata" o nessuna cambia stato. |
| INV-002 | Unicità del Batch | Un singolo batch non può contenere due volte lo stesso identificativo auto. |
| INV-003 | Esistenza | Tutte le auto richieste nel batch devono esistere nel parco mezzi. |

## Transizioni di Stato Atomiche

A differenza delle fasi precedenti, la transizione avviene sull'intero aggregato.

```text
[ParcoMezzi (Stato A)] ── NoleggiaBatch(List<Id>) ──► [ParcoMezzi (Stato B)]
                               |
                               └─ SE fallisce ──► [ParcoMezzi (Stato A)]
```

In OOP, il rollback è gestito tramite logica di validazione preventiva (Check-Then-Act). In FP, l'atomicità è garantita dalla natura del tipo `Result` e dalla trasformazione di stato (se la catena si rompe, il valore originale non viene mai sostituito).