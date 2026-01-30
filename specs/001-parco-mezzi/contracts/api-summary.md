# Sommario API: Fase 1 - Gestione Parco Mezzi Minimale

## OOP Track: `CarSharp.Oop`

### `ParcoMezzi` (Classe)

| Membro | Firma | Descrizione |
|--------|-------|-------------|
| Proprietà | `int TotaleAuto { get; }` | Restituisce il conteggio corrente degli oggetti nella lista interna. |
| Metodo | `void AggiungiAuto(Auto auto)` | Aggiunge il riferimento dell'auto alla collezione. |
| Metodo | `bool RimuoviAuto(Auto auto)` | Tenta di rimuovere l'oggetto tramite riferimento. Ritorna `false` se non trovato. |

---

## FP Track: `CarSharp.Functional`

### `ParcoMezzi` (Record)

| Membro | Firma | Descrizione |
|--------|-------|-------------|
| Proprietà | `static ParcoMezzi Vuoto { get; }` | Il valore costante di partenza. |
| Proprietà | `int TotaleAuto { get; }` | Conteggio degli elementi nella lista immutabile. |

### `ParcoMezziExtensions` (Metodi di Estensione)

| Firma | Ritorno | Descrizione |
|-------|---------|-------------|
| `AggiungiAuto(this ParcoMezzi parco, Auto auto)` | `ParcoMezzi` | Restituisce un NUOVO valore con l'auto aggiunta. |
| `RimuoviAuto(this ParcoMezzi parco, Auto auto)` | `ParcoMezzi` | Restituisce un NUOVO valore con l'auto rimossa. |
