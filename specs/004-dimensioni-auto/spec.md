# Feature Specification: Tipologie e dimensioni dei mezzi

**Feature Branch**: `004-dimensioni-auto`  
**Created**: 2026-01-13  
**Status**: Draft  
**Input**: User description: "Implementazione Fase 4: Tipologie e dimensioni dei mezzi"

## Clarifications

### Session 2026-01-13
- Q: Se più mezzi soddisfano il requisito di capacità, quale logica di selezione si applica? → A: Il sistema assegna il primo mezzo disponibile (in ordine di inserimento) che soddisfa il requisito.
- Q: Come gestire un batch con richieste miste (ID e capacità)? → A: Ordine sequenziale: le richieste sono processate nell'ordine del batch; i conflitti (mezzo già preso) causano il fallimento del batch.
- Q: Se una richiesta ha sia ID che Capacità, si verificano entrambi? → A: Sì, il vincolo di capacità è assoluto; se il mezzo per ID non è capiente, la richiesta fallisce.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Gestione flotta con capacità differenziata (Priority: P1)

Il gestore del parco mezzi vuole poter aggiungere veicoli specificando il numero di posti disponibili per ciascuno, in modo da offrire una gamma di opzioni ai clienti.

**Why this priority**: È fondamentale per poter differenziare i mezzi; senza questa base, non è possibile applicare filtri di capacità.

**Independent Test**: Può essere testato aggiungendo mezzi con diversi numeri di posti e verificando che il sistema mantenga correttamente l'informazione della capacità per ogni veicolo.

**Acceptance Scenarios**:

1. **Given** un parco mezzi vuoto, **When** aggiungo un'auto con 5 posti, **Then** il sistema deve confermare l'aggiunta e mostrare un mezzo con capacità 5.
2. **Given** un parco mezzi con un'auto da 2 posti, **When** aggiungo un'auto da 7 posti, **Then** il sistema deve mostrare correttamente entrambi i mezzi con le rispettive capacità.

---

### User Story 2 - Noleggio basato sulla capacità (Priority: P2)

Un cliente vuole noleggiare un mezzo specificando il numero minimo di posti di cui ha bisogno. Il sistema deve assegnare un mezzo che soddisfi tale requisito.

**Why this priority**: È il nucleo della logica di business di questa fase: l'assegnazione guidata dal requisito di capacità.

**Independent Test**: Può essere testato effettuando una richiesta di noleggio per N posti e verificando che l'auto assegnata abbia almeno N posti.

**Acceptance Scenarios**:

1. **Given** un parco con un'auto da 5 posti disponibile, **When** richiedo un noleggio per 4 posti, **Then** il noleggio deve avere successo e l'auto da 5 posti deve risultare occupata.
2. **Given** un parco con un'auto da 2 posti disponibile, **When** richiedo un noleggio per 5 posti, **Then** il noleggio deve fallire perché non ci sono mezzi sufficientemente capienti.

---

### User Story 3 - Batch di richieste con vincoli di capacità (Priority: P3)

Un cliente invia un batch di richieste, ognuna con il proprio requisito di posti. Il sistema deve soddisfare il batch solo se tutti i vincoli di capacità possono essere rispettati contemporaneamente.

**Why this priority**: Estende l'atomicità introdotta nella Fase 3 con i nuovi vincoli della Fase 4.

**Independent Test**: Può essere testato inviando un batch dove una richiesta è soddisfabile e l'altra no (per mancanza di posti), verificando che nessuna delle due venga applicata.

**Acceptance Scenarios**:

1. **Given** un parco con due auto da 5 posti disponibili, **When** invio un batch di due richieste da 4 posti ciascuna, **Then** entrambe le auto devono risultare noleggiate.
2. **Given** un parco con un'auto da 2 posti e una da 5 posti disponibili, **When** invio un batch con una richiesta da 2 posti e una da 6 posti, **Then** il batch deve fallire e tutti i mezzi devono restare disponibili.

### Edge Cases

- **Richiesta con capacità zero o negativa**: Come si comporta il sistema? (Assunzione: le richieste devono avere almeno 1 posto).
- **Esaurimento mezzi per capacità specifica**: Il sistema ha mezzi disponibili ma nessuno ha la capacità richiesta (es. tutte auto da 2 posti e richiesta da 4).
- **Richieste multiple nel batch per lo stesso mezzo**: (Gestito dall'unicità della targa se specificata, ma qui le richieste sono per capacità - Assunzione: una richiesta senza targa viene assegnata dal sistema al primo mezzo disponibile che soddisfa il requisito).

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Il sistema DEVE permettere di associare a ogni mezzo un numero intero positivo di posti (capacità).
- **FR-002**: Il sistema DEVE permettere di specificare un requisito di posti minimi per ogni richiesta di noleggio.
- **FR-003**: Il sistema DEVE assegnare una richiesta solo a un mezzo che ha una capacità maggiore o uguale ai posti richiesti. In presenza di più opzioni, viene scelto il primo mezzo disponibile in ordine di inserimento nel parco.
- **FR-004**: Una richiesta DEVE essere soddisfatta utilizzando un unico mezzo (non è possibile dividere una richiesta su più mezzi).
- **FR-005**: In caso di batch, il sistema DEVE garantire l'atomicità considerando sia la disponibilità dello stato che il soddisfacimento dei requisiti di capacità per ogni singola voce del batch, processandole in ordine sequenziale.
- **FR-006**: Se una richiesta specifica sia un identificativo mezzo (Targa) che un requisito di capacità, il sistema DEVE verificare che il mezzo specificato soddisfi il requisito di capacità. In caso contrario, la richiesta fallisce.

### Key Entities *(include if feature involves data)*

- **Mezzo**: Rappresenta il veicolo nel parco. Attributi chiave: Identificativo (Targa), Stato (Disponibile/Noleggiato), Capacità (Numero di posti).
- **Richiesta di Noleggio**: Rappresenta l'intenzione di noleggiare un mezzo. Attributi chiave: Posti minimi richiesti, [Identificativo mezzo opzionale].

## Success Criteria *(obbligatorio)*

### Risultati Misurabili

- **SC-001**: Il 100% dei noleggi effettuati deve essere assegnato a un mezzo con capacità maggiore o uguale ai posti richiesti.
- **SC-002**: Il sistema deve rifiutare il 100% dei batch che contengono almeno una richiesta non soddisfacibile per capacità o disponibilità.
- **SC-003**: La logica di assegnazione deve poter gestire correttamente richieste per mezzi con capacità fino a 50 posti (es. autobus) senza degradazione delle performance.
- **SC-004**: L'invariante di capacità deve essere preservato durante tutta la transizione di stato dell'auto (Disponibile -> Noleggiata).

## Assunzioni

- La capacità è espressa come numero intero di posti passeggeri (incluso il conducente).
- In questa fase, se più auto soddisfano il requisito di capacità, viene scelta la prima disponibile (ordine di inserimento).
- Non è possibile modificare la capacità di un'auto dopo che è stata inserita nel parco mezzi.
- Una richiesta con 0 posti o posti negativi è considerata invalida e deve generare un errore di validazione.