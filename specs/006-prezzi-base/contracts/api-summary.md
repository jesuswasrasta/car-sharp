# Sommario API: Fase 6 - Prezzi Base

## OOP Track: `CarSharp.Oop`

### `Auto` (Inizializzazione con Prezzo)

| Membro | Firma | Descrizione | Errori |
|--------|-------|-------------|--------|
| Costruttore | `Auto(..., decimal costo)` | Aggiunge il costo giornaliero fisso. | `ArgumentException` se costo <= 0. |

### `ParcoMezzi` (Calcolo Costi)

| Firma | Ritorno | Descrizione |
|-------|---------|-------------|
| `NoleggiaConCosto(...)` | `RisultatoNoleggio` | Restituisce auto e costo associato. |
| `PrenotaBatch(...)` | `RisultatoBatch` | Elabora richieste restituendo riepilogo costi. |

---

## FP Track: `CarSharp.Functional`

### `IAuto` (Proprietà Prezzo)

| Tipo | Proprietà | Descrizione |
|------|-----------|-------------|
| `IAuto` | `decimal CostoGiornaliero` | Requisito per tutti i record auto. |

### `ParcoMezziExtensions` (Pipeline Economica)

| Firma | Ritorno | Descrizione |
|-------|---------|-------------|
| `NoleggiaConCosto(this ParcoMezzi p, RichiestaNoleggio r)` | `Result<RisultatoNoleggio>` | Pipeline che emette il costo nel Result. |
| `NoleggiaBatch(this ParcoMezzi p, IEnumerable<RichiestaNoleggio> b)` | `Result<RisultatoBatch>` | Aggregazione atomica con somma dei costi totali. |

---

## Modelli di Risultato (DTO/Record)

| Nome | Paradigma | Contenuto |
|------|-----------|-----------|
| `RisultatoNoleggio` | OOP | `Auto Auto`, `decimal Costo` |
| `RisultatoNoleggio` | FP | `IAuto Auto`, `decimal Costo`, `ParcoMezzi ParcoAggiornato` |
| `RisultatoBatch` | Entrambi | Elenco noleggi riusciti e `decimal TotaleGenerale`. |