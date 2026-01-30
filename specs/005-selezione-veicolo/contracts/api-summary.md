# Sommario API: Fase 5 - Scelta del mezzo

## OOP Track: `CarSharp.Oop`

### `ParcoMezzi` (Selezione Ottimizzata)

| Metodo | Firma | Algoritmo | Errori |
|--------|-------|-----------|--------|
| `Noleggia` | `Auto Noleggia(RichiestaNoleggio r)` | **Best Fit**: Minima capacità sufficiente + Ordine inserimento. | `InvalidOperationException` se non trovata. |
| `NoleggiaBatch` | `void NoleggiaBatch(IEnumerable<RichiestaNoleggio> b)` | Atomico; ogni riga applica Best Fit sequenzialmente. | `ArgumentException` su ID duplicati. |

---

## FP Track: `CarSharp.Functional`

### `ParcoMezziExtensions` (Pipeline di Selezione)

| Firma | Ritorno | Logica di Selezione |
|-------|---------|---------------------|
| `NoleggiaAuto(this ParcoMezzi p, RichiestaNoleggio r)` | `Result<ParcoMezzi>` | Filtro Disponibili -> OrderBy Capacità -> Replace State. |
| `NoleggiaBatch(this ParcoMezzi p, IEnumerable<RichiestaNoleggio> b)` | `Result<ParcoMezzi>` | Composizione monadica (Aggregate + Bind) con Best Fit per ogni step. |

---

## Logica di Selezione (Pseudo-codice)

1. **Filtro**: `auto.Stato == Disponibile AND auto.Capacita >= richiesta.Posti`
2. **Ordinamento**: `OrderBy(a => a.Capacita)`
3. **Stabilità**: L'ordinamento LINQ preserva l'ordine di inserimento a parità di capacità.
4. **Esecuzione**: Prendi il primo risultato o restituisci errore.