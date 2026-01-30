# Sommario API: Fase 3 - Richieste Batch

## OOP Track: `CarSharp.Oop`

### `ParcoMezzi` (Metodi Batch)

| Membro | Firma | Descrizione | Errori |
|--------|-------|-------------|--------|
| Metodo | `void NoleggiaBatch(IEnumerable<Guid> ids)` | Tenta di noleggiare un batch di auto atomicamente. | `ArgumentException` su duplicati; `InvalidOperationException` su indisponibilità. |

---

## FP Track: `CarSharp.Functional`

### `ParcoMezziExtensions` (Metodi di Estensione)

| Firma | Ritorno | Descrizione |
|-------|---------|-------------|
| `NoleggiaBatch(this ParcoMezzi parco, IEnumerable<Guid> ids)` | `Result<ParcoMezzi>` | Trasforma il parco applicando il batch. Atomicità garantita dalla composizione monadica. |

---

## Dettagli di Implementazione

- **OOP**: Utilizza il pattern **Check-Then-Act**. Un primo ciclo valida tutti gli ID, un secondo ciclo applica `Noleggia()` alle istanze.
- **FP**: Utilizza il pattern **Aggregate/Fold**. La lista degli ID viene ridotta partendo dal parco iniziale, applicando `Bind(p => p.Noleggia(id))` a ogni passo.