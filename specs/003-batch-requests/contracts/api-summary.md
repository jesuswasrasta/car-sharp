# Contratti API: Richieste Batch (Fase 3)

**Fase**: 3
**Linguaggio**: C# 12 (.NET 10)

## Percorso OOP (`CarSharp.Oop`)

```csharp
namespace CarSharp.Oop;

public class ParcoMezzi
{
    // ... altri membri ...

    /// <summary>
    /// Tenta di noleggiare un insieme di auto in modo atomico.
    /// </summary>
    /// <param name="targhe">Elenco delle targhe da noleggiare.</param>
    /// <exception cref="InvalidOperationException">Lanciata se almeno un'auto non è disponibile o non esiste.</exception>
    /// <exception cref="ArgumentException">Lanciata se ci sono targhe duplicate nel batch.</exception>
    public void NoleggiaBatch(IEnumerable<Guid> targhe)
    {
        // Implementazione Check-Then-Act
    }
}
```

## Percorso Funzionale (`CarSharp.Functional`)

```csharp
namespace CarSharp.Functional;

public record ParcoMezzi
{
    // ... altri membri ...

    /// <summary>
    /// Calcola un nuovo stato del parco dopo il noleggio di un batch di auto.
    /// </summary>
    /// <param name="targhe">Elenco delle targhe da noleggiare.</param>
    /// <returns>
    /// Un Success con il nuovo ParcoMezzi se tutte le richieste sono valide,
    /// altrimenti un Failure con il dettaglio dell'errore.
    /// </returns>
    public Result<ParcoMezzi> NoleggiaBatch(IEnumerable<Guid> targhe)
    {
        // Implementazione tramite composizione (Aggregate/Bind)
    }
}
```
