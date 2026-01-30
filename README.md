# CarSharp
Da un'idea di Arialdo Martini: [CarSharp](https://github.com/arialdomartini/car-sharp). Grazie! 🤗  

## Contesto

Stai sviluppando il core di un **servizio di car renting**.
Il sistema non ha interfaccia grafica: è una libreria che gestisce **stato**, **regole di business** e **decisioni automatiche**.
Il dominio viene introdotto **per passi**, partendo da un modello volutamente povero e arricchendolo progressivamente.

---

## Fase 1 – Parco mezzi minimale (✅ COMPLETATA)

- Il sistema gestisce un parco mezzi.
- Il parco può essere inizialmente vuoto.
- È possibile aggiungere un mezzo al parco.
- È possibile rimuovere un mezzo dal parco.
- È possibile conoscere il numero totale di mezzi nel parco.

In questa fase abbiamo gettato le basi del sistema implementando la gestione del parco mezzi.   
Il confronto tra i paradigmi evidenzia scelte architetturali divergenti: l'approccio OOP sfrutta l'incapsulamento 
e la mutazione dello stato interno basata sull'identità di riferimento (reference equality);
l'approccio Funzionale, invece, si affida a record immutabili, funzioni pure e collezioni persistenti 
che sfruttano l'uguaglianza basata sul valore (value equality).

---

## Fase 2 – Disponibilità (✅ COMPLETATA)

- Un mezzo può essere **disponibile** o **noleggiato**.
- Noleggiare un mezzo disponibile lo rende non disponibile.
- Non è possibile noleggiare un mezzo già noleggiato.
- È possibile restituire un mezzo noleggiato.
- In ogni momento è possibile conoscere il numero di mezzi disponibili.

In questa fase abbiamo introdotto la gestione della disponibilità e dell'identità tecnica.
Il confronto ha evidenziato una divergenza fondamentale nella rappresentazione dello stato:
- **OOP (Orientata agli Oggetti)**: Lo stato è una **proprietà mutabile** (`StatoAuto`) interna all'oggetto. La consistenza è garantita dall'incapsulamento: l'oggetto stesso lancia un'eccezione se si tenta un'operazione non valida (es. noleggiare un'auto già occupata). L'identità è preservata dal riferimento in memoria.
- **FP (Funzionale)**: Lo stato è espresso dai **Tipi** (**Type-Driven Design**). Esistono tipi distinti per `AutoDisponibile` e `AutoNoleggiata`. Le operazioni sono trasformazioni pure da un tipo all'altro. Questo rende gli stati invalidi irrappresentabili a livello di compilazione. L'identità è garantita da un identificativo tecnico stabile (`Guid`) che persiste attraverso le trasformazioni.

---

## Fase 3 – Richieste e batch (✅ COMPLETATA)

- Il sistema può ricevere un batch di richieste.
- Un batch viene soddisfatto solo se **tutte** le richieste possono essere soddisfatte.
- Se anche una sola richiesta fallisce, nessuna viene applicata.

In questa fase abbiamo introdotto il concetto di **atomicità** delle operazioni multiple.
Il confronto ha evidenziato strategie opposte per garantire la consistenza:
- **OOP**: Pattern **Check-Then-Act**. Si convalida l'intero batch (controllando disponibilità e duplicati) *prima* di applicare qualsiasi modifica. La consistenza è garantita impedendo l'accesso allo stato intermedio non valido (spesso tramite lock in scenari concorrenti, qui semplificato).
- **FP**: Pattern **Parse, Don't Validate** e **State Transformation**. L'input viene prima "parsato" in una struttura valida (es. un Set per garantire unicità). Successivamente, si usa una pipeline di trasformazioni (`Aggregate` + `Bind`) dove ogni passo produce un nuovo stato o un errore. Se un passo fallisce, l'intera catena restituisce un errore, lasciando il valore originale (immutabile) intatto senza bisogno di rollback espliciti.

---

## Fase 4 – Tipologie e dimensioni

- I mezzi iniziano a differenziarsi per **dimensione**.
- Ogni mezzo ha un numero di posti (capacità).
- Una richiesta specifica il numero minimo di posti richiesti.
- Un mezzo può soddisfare una richiesta solo se ha posti sufficienti.
- Una richiesta viene sempre assegnata a un solo mezzo.

---

## Fase 5 – Scelta del mezzo

- Se più mezzi possono soddisfare una richiesta, il sistema sceglie quello con il minor numero di posti in eccesso.
- L'assegnazione delle richieste non dipende dall'ordine di arrivo.
- A parità di soluzioni valide, il risultato è deterministico.

---

## Fase 6 – Prezzi base

- Ogni mezzo ha un costo base giornaliero.
- Il costo di una prenotazione dipende dal mezzo.
- Mezzi più grandi non possono costare meno di mezzi più piccoli.
- Il costo totale di un batch è la somma dei costi delle singole prenotazioni.

---

## Fase 7 – Clienti e sconti

- Ogni prenotazione è associata a un cliente.
- Se un cliente prenota più mezzi nello stesso batch, ottiene uno sconto percentuale.
- Lo sconto si applica al totale del cliente, non ai singoli mezzi.
- Il prezzo finale non può mai essere negativo.

---

## Fase 8 – Profitto e ottimizzazione

- Un batch di richieste genera un profitto totale.
- Se esistono più assegnazioni valide, il sistema sceglie quella a profitto massimo.
- Uno sconto può rendere una soluzione valida ma meno profittevole di un’altra.
- Il sistema privilegia sempre il profitto totale rispetto alla soddisfazione di singole richieste.

---

## Fase 9 – Estensioni opzionali

- Una prenotazione può includere servizi aggiuntivi a costo fisso.
- I servizi aggiuntivi non sono soggetti a sconti.
- Il carburante mancante alla restituzione viene addebitato al cliente.
- Nessuna regola di business può violare la consistenza dello stato.

---

## Fase 10 - Requisiti perturbatori

- Alcuni mezzi sono premium e possono essere noleggiati solo pagando un sovrapprezzo fisso.

- I mezzi premium non partecipano agli sconti, anche se prenotati in batch.

- Un cliente può avere al massimo un mezzo attivo alla volta, indipendentemente dal tipo.

- È possibile prenotare più mezzi per un singolo evento, ma devono avere tutti la stessa durata.

- Il sistema può rifiutare un batch profittevole se non rispetta una politica di equità tra clienti.

- Alcuni clienti hanno un budget massimo che non può essere superato.

- Se una prenotazione supera il budget del cliente, l’intero batch fallisce.

- Il prezzo finale viene arrotondato per eccesso all’unità monetaria.

- I mezzi possono essere prenotati in anticipo e diventano indisponibili solo a partire da una certa data.

- Una prenotazione anticipata può essere annullata, ma prevede una penale fissa.
