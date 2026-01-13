# Modello Dati: Fase 2 – Identità e Stato

## Tipi Comuni
```csharp
public enum StatoAuto
{
    Disponibile,
    Noleggiata
}
```

## Percorso OOP (CarSharp.Oop)
### Classe `Auto`
- **Identità**: `public Guid Id { get; }` (Assegnato nel costruttore)
- **Dominio**: `public string Targa { get; private set; }`
- **Stato**: `public StatoAuto Stato { get; private set; }`
- **Metodi**:
    - `void Noleggia()`: Se `Stato == Noleggiata` lancia `InvalidOperationException`. Altrimenti `Stato = Noleggiata`.
    - `void Restituisci()`: `Stato = Disponibile`.

## Percorso Funzionale (CarSharp.Functional)
### Record `Auto`
- **Proprietà**:
    - `Guid id`
    - `string targa`
    - `StatoAuto stato`
- **Funzioni**:
    - `Result<Auto> Noleggia(this Auto auto)`
    - `Result<Auto> Restituisci(this Auto auto)`

### ParcoMezzi
- `int ConteggioDisponibili(this ParcoMezzi parco)`: Query LINQ `p.auto.Count(a => a.stato == StatoAuto.Disponibile)`.