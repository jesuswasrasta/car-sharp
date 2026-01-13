# Ricerca e Decisioni: Richieste Batch (Fase 3)

**Funzionalità**: Richieste Batch (Fase 3)
**Data**: 2026-01-13

## Decisione 1: Strategia di Atomicità

### Problema
Dobbiamo elaborare un batch di richieste di noleggio in modo atomico. O tutto il batch ha successo, o non cambia nulla.

### Opzioni Considerate (Entrambe in C#)

1.  **Check-Then-Act (OOP)**
    *   Sfrutta un ciclo di validazione preliminare.
    *   Se tutte le auto sono disponibili, procede con un secondo ciclo di mutazione dello stato.
    *   *Pro*: Semplice da implementare e comprendere in un modello a oggetti mutabile.
    *   *Contro*: Richiede attenzione per garantire che lo stato non cambi tra la validazione e l'esecuzione (in contesti concorrenti).

2.  **State Transformation con Result (FP)**
    *   Usa un approccio di "folding" (aggregazione) sulla lista delle richieste.
    *   Ogni trasformazione produce un nuovo `Result<ParcoMezzi, Error>`.
    *   Se una trasformazione fallisce, l'intera catena si interrompe restituendo l'errore e preservando il riferimento al parco originale.
    *   *Pro*: Estremamente sicuro e dichiarativo. L'atomicità è garantita dal fatto che il nuovo stato viene "emesso" solo se tutto il processo ha successo.
    *   *Contro*: Richiede l'uso di tipi `Result` e operatori di binding (es. `Bind`, `Map`).

### Decisione
- **Track OOP**: Useremo il pattern **Check-Then-Act**. Un metodo `Noleggia(IEnumerable<Guid> ids)` verificherà prima l'intera lista e poi applicherà le mutazioni.
- **Track FP**: Useremo la **Trasformazione dello Stato** tramite `Result`. Useremo l'estensione `Aggregate` (o un fold personalizzato) per accumulare i cambiamenti in modo immutabile.

### Motivazione
Questa scelta evidenzia perfettamente la differenza tra "modificare un oggetto esistente" (OOP) e "calcolare una nuova versione della realtà" (FP), rimanendo fedeli alla Costituzione di CarSharp.

## Decisione 2: Rappresentazione della Richiesta

### Decisione
Una richiesta è rappresentata semplicemente dal `Guid` (Targa) dell'auto, come definito nel requisito FR-002 della specifica. Non aggiungeremo complessità (clienti, date) che appartengono alle fasi successive.