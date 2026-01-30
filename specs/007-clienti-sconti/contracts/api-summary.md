# API Summary: Clienti e Sconti

Questa sezione descrive le firme pubbliche aggiornate per supportare la gestione clienti e sconti.

## C# OOP (`CarSharp.Oop`)

### `ParcoMezzi`

```csharp
public class ParcoMezzi
{
    // ... metodi esistenti ...

    /// <summary>
    /// Processa un batch di richieste di noleggio applicando sconti per clienti multipli.
    /// </summary>
    /// <param name="richieste">Lista delle richieste di noleggio.</param>
    /// <param name="scontoPercentuale">Percentuale di sconto (0.0 - 1.0) per clienti con >1 prenotazione.</param>
    /// <returns>RisultatoBatch contenente i dettagli e i totali.</returns>
    /// <exception cref="ArgumentException">Se scontoPercentuale < 0 o > 1.</exception>
    public RisultatoBatch PrenotaBatch(IEnumerable<RichiestaNoleggio> richieste, decimal scontoPercentuale);
}
```

### `RichiestaNoleggio`

```csharp
public class RichiestaNoleggio
{
    public RichiestaNoleggio(string targa, int giorni, string clienteId);
    // ...
}
```

---

## C# Functional (`CarSharp.Functional`)

### `ParcoMezzi` (Modulo/Static Class)

```csharp
public static class ParcoMezzi
{
    // ... funzioni esistenti ...

    /// <summary>
    /// Funzione pura per processare un batch con sconti.
    /// </summary>
    public static Func<
        ParcoMezzi,                 // Stato Iniziale
        IEnumerable<RichiestaNoleggio>, // Richieste
        decimal,                    // Sconto Percentuale
        Result<RisultatoBatch>      // Risultato (Nuovo Stato + Dettagli o Errore)
    > PrenotaBatch = ...;
}
```

### `RichiestaNoleggio`

```csharp
public record RichiestaNoleggio(string Targa, int Giorni, string ClienteId);
```

### `Result`

Nessuna modifica strutturale al tipo `Result`, ma verrà utilizzato per gestire errori di validazione dello sconto (es. percentuale invalida).
