# Sommario API: Fase 2 - Disponibilità e Identità

## OOP Track: `CarSharp.Oop`

### `Auto` (Classe)

| Membro | Firma | Descrizione | Errori |
|--------|-------|-------------|--------|
| Costruttore | `Auto(Guid id, string targa)` | Inizializza un'auto in stato "Disponibile". | `ArgumentException` se targa vuota. |
| Metodo | `void Noleggia()` | Passa lo stato a "Noleggiata". | `InvalidOperationException` se già noleggiata. |
| Metodo | `void Restituisci()` | Torna allo stato "Disponibile". | - |

---

## FP Track: `CarSharp.Functional`

### `Auto` (Record / Tipi)

In questo paradigma, lo stato è espresso tramite specializzazioni o proprietà immutabili.

| Tipo | Membro | Descrizione |
|------|--------|-------------|
| `IAuto` | `Guid Id`, `string Targa` | Interfaccia base per l'identità. |
| `AutoDisponibile` | Record | Rappresenta un'auto pronta per il noleggio. |
| `AutoNoleggiata` | Record | Rappresenta un'auto attualmente occupata. |

### `AutoExtensions` (Metodi di Estensione)

| Firma | Ritorno | Descrizione |
|-------|---------|-------------|
| `Noleggia(this Auto auto)` | `Result<Auto>` | Trasforma un'auto in stato noleggiata. |
| `Restituisci(this Auto auto)` | `Result<Auto>` | Trasforma un'auto in stato disponibile. |