# Sommario API: Fase 4 - Tipologie e dimensioni

## OOP Track: `CarSharp.Oop`

### `Auto` (Proprietà Capacità)

| Membro | Firma | Descrizione |
|--------|-------|-------------|
| Proprietà | `int Capacita { get; }` | Posti totali del mezzo. |

### `ParcoMezzi` (Metodi di Selezione)

| Firma | Ritorno | Descrizione | Errori |
|-------|---------|-------------|--------|
| `Noleggia(int postiMinimi)` | `Auto` | Trova e noleggia la prima auto idonea. | `InvalidOperationException` se non trovata. |

---

## FP Track: `CarSharp.Functional`

### `RichiestaNoleggio` (Nuovo Record)

| Proprietà | Tipo | Descrizione |
|-----------|------|-------------|
| `PostiMinimi` | `int` | Requisito di capacità. |
| `IdAuto` | `Guid?` | Identificativo specifico opzionale. |

### `ParcoMezziExtensions`

| Firma | Ritorno | Descrizione |
|-------|---------|-------------|
| `NoleggiaPerCapacita(this ParcoMezzi p, int posti)` | `Result<ParcoMezzi>` | Trasforma il parco cercando per capacità. |
| `NoleggiaAuto(this ParcoMezzi p, RichiestaNoleggio r)` | `Result<ParcoMezzi>` | Trasforma il parco in base alla richiesta complessa. |
