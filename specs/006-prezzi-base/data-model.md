# Data Model: Prezzi Base (Fase 6)

**Branch**: `006-prezzi-base`
**Spec**: [spec.md](./spec.md)

## Panoramica

Questo documento descrive le estensioni al modello dati per supportare il costo giornaliero dei mezzi. Le modifiche sono minimali e preservano la compatibilità con le fasi precedenti.

---

## Entità Modificate

### Auto (OOP)

```
Auto
├── Id: Guid                    [esistente] Identificativo tecnico univoco
├── Targa: string               [esistente] Identificativo di dominio
├── Stato: StatoAuto            [esistente] Disponibile | Noleggiata
├── Capacita: int               [esistente] Numero di posti (> 0)
└── CostoGiornaliero: decimal   [NUOVO] Prezzo base giornaliero (> 0)
```

**Validazione**:
- `CostoGiornaliero` DEVE essere > 0 (costruttore)
- Invariante capacità-prezzo validato al momento dell'aggiunta al parco

---

### IAuto / AutoDisponibile / AutoNoleggiata (FP)

```
IAuto (interface)
├── Id: Guid                    [esistente]
├── Targa: string               [esistente]
├── Capacita: int               [esistente]
└── CostoGiornaliero: decimal   [NUOVO]

AutoDisponibile : IAuto (record)
└── [eredita tutti i campi]

AutoNoleggiata : IAuto (record)
└── [eredita tutti i campi]
```

**Validazione**:
- Record con init-only properties; validazione al factory o all'aggiunta al parco

---

## Nuove Entità

### RisultatoNoleggio (OOP)

```
RisultatoNoleggio
├── Auto: Auto          Riferimento all'auto noleggiata
└── Costo: decimal      Costo del noleggio (= Auto.CostoGiornaliero)
```

**Note**: Incapsula il risultato di un'operazione di noleggio singola.

---

### RisultatoNoleggio (FP)

```
RisultatoNoleggio (record)
├── Auto: IAuto         Riferimento all'auto noleggiata
└── Costo: decimal      Costo del noleggio
```

---

### RisultatoBatch (FP)

```
RisultatoBatch (record)
├── Noleggi: ImmutableList<RisultatoNoleggio>   Lista dei noleggi effettuati
└── CostoTotale: decimal                        Somma dei costi individuali
```

**Invariante**: `CostoTotale = Noleggi.Sum(n => n.Costo)`

---

## Relazioni

```text
RichiestaNoleggio ───► ParcoMezzi ───► RisultatoNoleggio
                          │               ├── Auto (Noleggiata)
                          │               └── Costo (Decimal)
                          ▼
                       Batch ────────► RisultatoBatch
                                          ├── Noleggi (List)
                                          └── CostoTotale (Σ Costi)
```

## Invarianti di Dominio

| ID | Invariante | Descrizione |
|----|------------|-------------|
| INV-001 | Prezzo Positivo | `CostoGiornaliero` deve essere strettamente maggiore di zero. |
| INV-002 | Coerenza Prezzo-Capacità | `Capacita(A) > Capacita(B)` implica `CostoGiornaliero(A) >= CostoGiornaliero(B)`. |
| INV-003 | Correttezza Sommativa | `RisultatoBatch.CostoTotale` deve essere esattamente la somma dei singoli `RisultatoNoleggio.Costo`. |

## Transizioni di Stato Economiche

La transizione di noleggio ora emette un valore economico oltre a cambiare lo stato dell'auto.

```text
[Auto (Disponibile)] ── Noleggia() ──► [Auto (Noleggiata) + Valore(CostoGiornaliero)]
```

In OOP, il metodo `NoleggiaConCosto` restituisce un DTO `RisultatoNoleggio`. In FP, il record `RisultatoNoleggio` include il nuovo stato del parco per permettere la composizione fluida (folding).
