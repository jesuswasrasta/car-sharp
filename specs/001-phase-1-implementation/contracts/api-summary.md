# Sintesi delle API: Fase 1

## Percorso OOP (`CarSharp.Oop`)

### Classe `ParcoMezzi`
- `int TotaleAuto { get; }`
- `void AggiungiAuto(Auto auto)`
- `bool RimuoviAuto(Auto auto)`

---

## Percorso Funzionale (`CarSharp.Functional`)

### Record `ParcoMezzi`
- `static ParcoMezzi Vuoto { get; }`
- `int TotaleAuto { get; }`

### Metodi di Estensione (`ParcoMezziExtensions`)
- `ParcoMezzi AggiungiAuto(this ParcoMezzi parco, Auto auto)`
- `ParcoMezzi RimuoviAuto(this ParcoMezzi parco, Auto auto)`