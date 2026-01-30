# Data Model: Phase 5 - Scelta del mezzo

## Entities

### Auto
Rappresenta un veicolo nel parco mezzi.
- **Fields**:
  - `Id`: Identificativo univoco (string).
  - `Capacita`: Numero di posti (int, > 0).
  - `Stato`: (OOP) `Disponibile` o `Noleggiata`.
- **Validation**:
  - `Capacita` deve essere maggiore di zero.

### RichiestaNoleggio
Rappresenta la richiesta di un utente.
- **Fields**:
  - `IdAuto`: (Opzionale) ID specifico richiesto.
  - `PostiRichiesti`: Numero di persone (int, > 0).
- **Validation**:
  - Almeno uno tra `IdAuto` e `PostiRichiesti` deve essere presente (o entrambi).
  - `PostiRichiesti` deve essere maggiore di zero.

### ParcoMezzi
Aggregato che gestisce le auto e le prenotazioni.
- **Relationships**:
  - Contiene una collezione di `Auto`.
- **Logic**:
  - `Noleggia(RichiestaNoleggio)`: Trova l'auto ottimale.
  - `NoleggiaBatch(IEnumerable<RichiestaNoleggio>)`: Esegue noleggi multipli in modo atomico.

## Relazioni

```text
RichiestaNoleggio ───► ParcoMezzi (Algoritmo Best Fit)
                          │
                          ▼
                       Auto (Selezionata per capacità minima residua)
```

## Invarianti di Dominio

| ID | Invariante | Descrizione |
|----|------------|-------------|
| INV-001 | Ottimizzazione Risorse | Non deve esistere un'auto disponibile con capacità `C` tale che `Richiesta <= C < AutoAssegnata`. |
| INV-002 | Determinismo Stabile | A parità di capacità `C`, viene scelta l'auto con l'indice di inserimento minore. |
| INV-003 | Integrità Batch | Ogni riga del batch deve applicare indipendentemente l'ottimizzazione sulle auto rimanenti. |

## Pipeline di Selezione

L'assegnazione non è più una semplice ricerca del primo elemento, ma una proiezione ordinata.

```text
[Auto Disponibili] ── Filter (Capacita >= Richiesta) ──► OrderBy (Capacita ASC) ──► First()
```

In OOP, questo avviene tramite LINQ sulla lista mutabile all'interno del metodo `Noleggia`. In FP, la pipeline è una composizione di funzioni pure che proietta il nuovo stato del parco mezzi.
