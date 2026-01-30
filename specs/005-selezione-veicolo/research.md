# Research: Phase 5 - Scelta del mezzo

## Decision: Strategia di Selezione Ottimizzata

L'obiettivo è selezionare l'auto che minimizza lo "spreco" di posti (capacità residua minima), mantenendo il determinismo.

### Track OOP (Object-Oriented)
- **Logica**: All'interno di `ParcoMezzi.Noleggia`, filtreremo le auto disponibili che soddisfano la capacità richiesta. Ordineremo quindi per `Capacita` (ascendente) e, in caso di parità, per l'ordine naturale di inserimento (che è garantito dalla struttura dati `List<Auto>` o simile già presente).
- **Pattern**: "Check-Then-Act". Il `ParcoMezzi` interroga lo stato delle auto e sceglie la migliore.

### Track Functional (FP)
- **Logica**: Utilizzeremo una pipeline LINQ. Filtro per `Disponibile` e `Capacita >= richiesta`, ordinamento per `Capacita`, quindi selezione della prima (`FirstOrDefault`).
- **Pattern**: "Parse, don't validate". La richiesta viene trasformata in un'operazione di filtro su una collezione immutabile.

## Rationale: Perché questa scelta?
- **Efficienza**: Minimizzare i posti in eccesso permette di servire gruppi più grandi in futuro.
- **Determinismo**: L'ordine di inserimento è il criterio di parità più semplice e naturale da implementare senza aggiungere metadati extra (come timestamp).
- **Consistenza**: Entrambi i paradigmi utilizzeranno la stessa logica di base ma espressa con i propri idiomi.

## Alternatives considered: Cosa altro abbiamo valutato?
- **Selezione Casuale**: Scartata perché non deterministica e difficile da testare.
- **Selezione per ID**: Scartata come default perché l'utente non sempre conosce gli ID (ma supportata come fallback se l'ID è esplicitamente richiesto, come da FR-005).
- **Round-robin**: Scartata perché non tiene conto della capacità, obiettivo primario di questa fase.

## Decision: Gestione Errori per Input Non Validi (Capacità <= 0)
- **Track OOP**: Utilizzeremo un approccio fail-fast lanciando una `ArgumentException` nel momento della validazione della `RichiestaNoleggio` o all'inizio del metodo `Noleggia`.
- **Track FP**: La validazione restituirà un `Result.Failure` con un messaggio descrittivo (es. "La capacità richiesta deve essere maggiore di zero"), mantenendo la pipeline pura e senza eccezioni.

## Unknowns Resolved
- **Batch Processing**: Ogni elemento del batch deve essere valutato sequenzialmente. Se una riga del batch consuma l'auto "ottimale" per la riga successiva, la riga successiva dovrà scegliere la *nuova* ottimale tra quelle rimaste. Questo garantisce l'atomicità e la correttezza del batch.
