# Confronto Architetturale - Fase 3: Richieste Batch

## Obiettivo
Implementare l'esecuzione atomica di un batch di noleggi: o tutti hanno successo, o nessuno viene applicato.

## Approccio OOP: Check-Then-Act

In C# OOP, abbiamo utilizzato un approccio imperativo basato sulla validazione preventiva.

### Caratteristiche
1.  **Validazione Preliminare**: Iteriamo sull'input per verificare tutte le precondizioni (esistenza, disponibilità, unicità).
2.  **Esecuzione**: Solo se la validazione passa, eseguiamo un secondo ciclo che applica le mutazioni (`auto.Noleggia()`).
3.  **Gestione Errori**: Eccezioni (`InvalidOperationException`, `ArgumentException`) interrompono il flusso.

### Codice
```csharp
public void NoleggiaBatch(IEnumerable<Guid> targhe)
{
    // Validazione (Check)
    if (duplicati) throw new ArgumentException(...);
    foreach (var targa in targhe) 
    {
        if (!Disponibile(targa)) throw new InvalidOperationException(...);
    }

    // Esecuzione (Act)
    foreach (var targa in targhe) 
    {
        auto.Noleggia(); // Mutazione
    }
}
```

### Pro e Contro
*   ✅ Facile da leggere per chi viene da background imperativo.
*   ✅ Efficiente in termini di allocazioni (lavora in-place).
*   ❌ Richiede attenzione per mantenere la sincronizzazione tra Check e Act (race conditions in multithreading).
*   ❌ Logica di validazione e business spesso intrecciate.

## Approccio FP: Parse, Don't Validate & State Transformation

In C# FP, abbiamo utilizzato un approccio dichiarativo basato sulla trasformazione di stato e sulla tipizzazione forte.

### Caratteristiche
1.  **Parse, Don't Validate**: Trasformiamo l'input "grezzo" (`IEnumerable`) in un input "valido" (verificando duplicati all'ingresso).
2.  **Monadic Binding**: Usiamo `Result<T>` e `Bind` per concatenare le operazioni.
3.  **Immutabilità**: Ogni passo produce un *nuovo* stato del parco. Se la catena si rompe, restituiamo un errore e il vecchio stato rimane valido (atomicità implicita).

### Codice
```csharp
public static Result<ParcoMezzi> NoleggiaBatch(this ParcoMezzi parco, IEnumerable<Guid> targhe)
{
    // Parsing
    var setTarghe = targhe.ToList();
    if (duplicati) return Result.Fail("Duplicati");

    // State Transformation (Aggregate)
    return setTarghe.Aggregate(
        Result.From(parco), 
        (res, id) => res.Bind(p => p.NoleggiaAuto(id))
    );
}
```

### Pro e Contro
*   ✅ Atomicità garantita "by design": impossibile lasciare il sistema in stato inconsistente.
*   ✅ Codice estremamente conciso e componibile.
*   ✅ "Parse don't validate" elimina intere classi di bug logici.
*   ❌ Curva di apprendimento più ripida (Concetti: Monadi, Fold, Immutabilità).
*   ❌ Maggior pressione sul Garbage Collector (creazione di oggetti intermedi).