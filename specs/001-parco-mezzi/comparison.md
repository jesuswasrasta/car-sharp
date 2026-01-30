# Confronto: Fase 1 - Gestione Parco Mezzi Minimale

Questa fase evidenzia le differenze fondamentali tra i paradigmi Object-Oriented e Funzionale in C#.

## 1. Identità e Uguaglianza
- **OOP (classe `Auto`)**: Utilizza l'**uguaglianza per riferimento**. Ogni `new Auto()` è un'entità unica in memoria. Per rimuovere un'auto è necessario passare esattamente lo stesso riferimento all'oggetto che è stato aggiunto.
- **Funzionale (record `Auto`)**: Utilizza l'**uguaglianza per valore**. Poiché la nostra `Auto` è vuota, tutte le istanze `new Auto()` sono tecnicamente uguali. Tuttavia, i record forniscono un modo stabile per gestire i dati come valori piuttosto che come identità.

## 2. Gestione dello Stato
- **OOP (`ParcoMezzi.AggiungiAuto`)**: Utilizza la **mutazione sul posto**. La lista interna dell'oggetto `ParcoMezzi` viene modificata. L'identità dell'oggetto `ParcoMezzi` rimane la stessa, ma il suo contenuto cambia. Questo è efficiente ma può portare a effetti collaterali se il parco mezzi è condiviso.
- **Funzionale (`ParcoMezzi.AggiungiAuto`)**: Utilizza l'**immutabilità**. Il `ParcoMezzi` originale non viene mai modificato. Invece, un'espressione `with` crea una nuova copia con la lista aggiornata. Questo è più sicuro per la concorrenza e lo stato condiviso, ma richiede più allocazioni (mitigate dai record C# e dalle Collezioni Immutabili).

## 3. Structural Sharing
- **Funzionale (`ParcoMezzi.RimuoviAuto`)**: Abbiamo implementato un controllo: se `Remove` non trova l'auto, restituisce la *stessa* istanza della lista. Abbiamo sfruttato questo per restituire la *stessa* istanza di `ParcoMezzi`. Questo "Structural Sharing" è un'ottimizzazione chiave nella programmazione funzionale, riducendo al minimo le copie non necessarie.

## 4. Progettazione delle API
- **OOP**: I metodi sono membri della classe (`parco.AggiungiAuto(auto)`) e restituiscono `void` (affidandosi agli effetti collaterali) o `bool` (indicando il successo).
- **Funzionale**: Abbiamo utilizzato i **metodi di estensione** (`parco.AggiungiAuto(auto)`) per separare i dati (`record ParcoMezzi`) dalla logica. Ogni operazione restituisce il nuovo stato, consentendo il **concatenamento fluido (fluent chaining)**.

## 5. Testing
- **OOP**: **Basato su esempi (Facts)**. Verifichiamo scenari specifici "Dato-Quando-Allora".
- **Funzionale**: **Basato su proprietà (FsCheck)**. Verifichiamo che "L'aggiunta di $N$ auto risulti SEMPRE in $N$ auto totali", indipendentemente da quanto sia $N$. Questo permette di scoprire i casi limite in modo più efficace.

## 6. Gestione degli Errori: Bool vs Result
- **OOP**: Spesso ci si affida a valori di ritorno booleani o eccezioni. Ad esempio, `RimuoviAuto` restituisce `true` o `false`. Spetta al chiamante decidere se e come gestire il fallimento.
- **Funzionale**: Anche se in questa fase iniziale siamo minimali, l'approccio funzionale tende verso il **Railway Oriented Programming (ROP)**. Le operazioni che possono fallire restituiscono tipi come `Result<T, E>`, costringendo il chiamante a gestire esplicitamente sia il caso di successo che quello di errore, permettendo di concatenare le operazioni come "binari" di una ferrovia.