# Feature Specification: Prezzi Base

**Feature Branch**: `006-prezzi-base`
**Created**: 2026-01-30
**Status**: Draft
**Input**: User description: "Fase 6 – Prezzi base: Ogni mezzo ha un costo base giornaliero. Il costo di una prenotazione dipende dal mezzo. Mezzi più grandi non possono costare meno di mezzi più piccoli. Il costo totale di un batch è la somma dei costi delle singole prenotazioni."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Calcolo costo prenotazione singola (Priority: P1)

Il sistema calcola automaticamente il costo di una prenotazione in base al mezzo assegnato. Quando un utente noleggia un'auto, il sistema deve restituire il costo giornaliero associato a quel mezzo.

**Why this priority**: È il requisito fondamentale della fase: senza questa funzionalità non è possibile calcolare alcun costo.

**Independent Test**: Può essere testato creando un parco mezzi con auto a diversi prezzi e verificando che il noleggio restituisca il costo corretto.

**Acceptance Scenarios**:

1. **Given** un parco mezzi con una Fiat 500 (4 posti, 30€/giorno) disponibile, **When** noleggio la Fiat 500, **Then** il sistema restituisce un costo di 30€.
2. **Given** un parco mezzi con un Ford Transit (9 posti, 80€/giorno) disponibile, **When** noleggio il Ford Transit, **Then** il sistema restituisce un costo di 80€.
3. **Given** un parco mezzi con più auto disponibili, **When** noleggio per 4 posti (Best Fit seleziona la Fiat 500 a 30€/giorno), **Then** il costo restituito è 30€.

---

### User Story 2 - Vincolo di coerenza prezzo-capacità (Priority: P1)

Il sistema garantisce che mezzi con capacità maggiore non possano avere un costo inferiore a mezzi con capacità minore. Questo vincolo assicura la coerenza commerciale del listino prezzi.

**Why this priority**: È un invariante di business che deve essere garantito fin dall'inizio per evitare configurazioni incoerenti.

**Independent Test**: Può essere testato tentando di creare auto con prezzi incoerenti rispetto alla capacità.

**Acceptance Scenarios**:

1. **Given** una configurazione di mezzo con 5 posti e 25€/giorno, **When** tento di aggiungere un mezzo con 4 posti e 30€/giorno, **Then** l'operazione è valida (il mezzo più piccolo può costare meno).
2. **Given** una configurazione di mezzo con 5 posti e 40€/giorno, **When** tento di aggiungere un mezzo con 7 posti e 35€/giorno, **Then** l'operazione viene rifiutata (mezzo più grande non può costare meno).
3. **Given** un parco vuoto, **When** aggiungo un mezzo con 4 posti e 30€/giorno, **Then** l'operazione è valida (primo mezzo, nessun confronto necessario).

---

### User Story 3 - Calcolo costo batch (Priority: P2)

Quando il sistema elabora un batch di richieste, il costo totale è la somma dei costi delle singole prenotazioni. Ogni richiesta nel batch viene valutata indipendentemente e il costo complessivo viene calcolato.

**Why this priority**: Estende la funzionalità base al contesto batch già implementato nelle fasi precedenti.

**Independent Test**: Può essere testato creando un batch di più richieste e verificando che il totale sia la somma dei singoli costi.

**Acceptance Scenarios**:

1. **Given** un parco con Fiat 500 (30€) e VW Golf (40€), **When** invio un batch con richieste per 2 e 4 posti, **Then** il costo totale è 70€ (30€ + 40€).
2. **Given** un parco con tre auto disponibili a 30€, 40€ e 80€, **When** invio un batch con due richieste, **Then** il costo totale è la somma dei due mezzi assegnati (Best Fit).
3. **Given** un batch che non può essere soddisfatto (atomicità), **When** il batch fallisce, **Then** il costo restituito è zero o errore (nessuna prenotazione effettuata).

---

### Edge Cases

- Cosa succede quando si tenta di noleggiare da un parco vuoto? Il costo non viene calcolato e viene restituito un errore.
- Cosa succede se il prezzo è zero o negativo? Il sistema deve rifiutare configurazioni con prezzi non validi (prezzo deve essere maggiore di zero).
- Come si comporta il vincolo capacità-prezzo con capacità uguali? Auto con la stessa capacità possono avere prezzi diversi.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Ogni mezzo DEVE avere un attributo `CostoGiornaliero` (prezzo base per giorno di noleggio).
- **FR-002**: Il `CostoGiornaliero` DEVE essere un valore positivo maggiore di zero.
- **FR-003**: Il sistema DEVE restituire il costo del mezzo assegnato quando viene effettuato un noleggio singolo.
- **FR-004**: Il sistema DEVE garantire l'invariante: per ogni coppia di mezzi A e B, se `Capacita(A) > Capacita(B)`, allora `CostoGiornaliero(A) >= CostoGiornaliero(B)`.
- **FR-005**: Il sistema DEVE calcolare il costo totale di un batch come somma dei costi delle singole prenotazioni.
- **FR-006**: Se un batch fallisce (per atomicità), il sistema DEVE restituire un errore senza costi parziali.
- **FR-007**: L'algoritmo Best Fit (già implementato) DEVE continuare a funzionare: la selezione avviene per capacità minima sufficiente, il costo è quello del mezzo selezionato.

### Key Entities

- **Auto**: Estensione con attributo `CostoGiornaliero` (decimal > 0). Rappresenta il prezzo base giornaliero del mezzo.
- **RisultatoNoleggio** (nuovo): Contiene riferimento al mezzo noleggiato e il costo calcolato.
- **RisultatoBatch** (estensione): Contiene la lista dei noleggi effettuati e il costo totale.

## Success Criteria *(obbligatorio)*

### Risultati Misurabili

- **SC-001**: Il sistema restituisce il costo corretto per ogni noleggio singolo nel 100% dei casi (Costo = CostoGiornaliero del mezzo).
- **SC-002**: Il sistema rifiuta il 100% delle configurazioni che violano l'invariante capacità-prezzo all'aggiunta al parco.
- **SC-003**: Il costo totale di un batch è matematicamente uguale alla somma dei costi individuali (precisione decimale assoluta).
- **SC-004**: L'atomicità è preservata: un batch fallito non deve produrre alcun calcolo di costo o variazione di stato.

## Assunzioni

- Il costo giornaliero è un valore fisso per mezzo e non varia in base alla durata (la durata sarà introdotta in fasi successive).
- Non esistono sconti o maggiorazioni in questa fase; il prezzo è puramente additivo.
- L'unità monetaria è l'euro (€) rappresentata tramite precisione `decimal`.
- Il vincolo capacità-prezzo è assoluto: un mezzo più grande NON può costare meno di uno più piccolo, indipendentemente da altri fattori (segmento, lusso, etc.) non ancora modellati.
