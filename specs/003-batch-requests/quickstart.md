# Quickstart: Richieste Batch (Fase 3)

Esempi di utilizzo per i due paradigmi in C#.

## OOP (Imperativo e Mutabile)

In questo approccio, il batch viene elaborato validando prima lo stato e poi applicando le modifiche all'oggetto esistente.

```csharp
var parco = new ParcoMezzi();
// ... aggiunta auto ...

var batch = new List<Guid> { targa1, targa2 };

try 
{
    // Il metodo modifica internamente il parco o lancia eccezione
    parco.NoleggiaBatch(batch);
    Console.WriteLine("Batch processato con successo.");
}
catch (Exception ex)
{
    // Se fallisce, nessuna auto del batch è stata noleggiata (Atomicità)
    Console.WriteLine($"Errore nel batch: {ex.Message}");
}
```

## Funzionale (Dichiarativo e Immutabile)

In questo approccio, il batch è una trasformazione che produce un nuovo valore del parco. Se fallisce, otteniamo un errore e il parco originale rimane disponibile.

```csharp
ParcoMezzi parcoIniziale = ParcoMezzi.Vuoto;
// ... aggiunta auto ...

var batch = new List<Guid> { targa1, targa2 };

// La trasformazione restituisce un Result
var risultato = parcoIniziale.NoleggiaBatch(batch);

risultato.Match(
    success: nuovoParco => {
        Console.WriteLine("Nuovo stato del parco creato.");
        // nuovoParco contiene le auto noleggiate
    },
    failure: errore => {
        Console.WriteLine($"Batch fallito: {errore}");
        // parcoIniziale è ancora nel suo stato precedente
    }
);
```
