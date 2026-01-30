# Feature Specification: Fase 2 – Disponibilità dei Mezzi

**Feature Branch**: `002-disponibilita`  
**Created**: 2026-01-13  
**Status**: Draft  
**Input**: User description: "Implementiamo la fase 2"

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Noleggio di un'auto disponibile (Priority: P1)

Come addetto al parco mezzi, voglio poter noleggiare un'auto che risulta attualmente disponibile, in modo da poterla consegnare al cliente.

**Why this priority**: È la funzionalità principale di questa fase. Senza la possibilità di cambiare lo stato da "Disponibile" a "Noleggiata", il sistema non ha valore di business.

**Independent Test**: Creando un'auto disponibile e invocando l'azione di noleggio, l'auto deve risultare in stato "Noleggiata".

**Acceptance Scenarios**:

1. **Given** un'auto disponibile, **When** viene noleggiata, **Then** il suo stato diventa "Noleggiata".
2. **Given** un'auto già noleggiata, **When** si tenta di noleggiarla nuovamente, **Then** il sistema restituisce un errore e lo stato rimane "Noleggiata".

---

### User Story 2 - Restituzione di un'auto noleggiata (Priority: P2)

Come addetto al parco mezzi, voglio poter registrare la restituzione di un'auto precedentemente noleggiata, in modo che torni ad essere disponibile per altri clienti.

**Why this priority**: Chiude il ciclo di vita del noleggio. È fondamentale per mantenere aggiornato l'inventario dei mezzi pronti all'uso.

**Independent Test**: Prendendo un'auto in stato "Noleggiata" e invocando la restituzione, lo stato deve tornare a "Disponibile".

**Acceptance Scenarios**:

1. **Given** un'auto noleggiata, **When** viene restituita, **Then** lo stato torna "Disponibile".
2. **Given** un'auto già disponibile, **When** si tenta di restituirla, **Then** il sistema restituisce un errore (o ignora l'operazione a seconda dell'implementazione specifica).

---

### User Story 3 - Monitoraggio della flotta disponibile (Priority: P3)

Come gestore del parco, voglio conoscere in tempo reale quante auto sono pronte per il noleggio, per ottimizzare la gestione della flotta.

**Why this priority**: Fornisce una vista d'insieme utile per la pianificazione commerciale, sebbene l'operatività singola (noleggio/restituzione) sia più critica.

**Independent Test**: Aggiungendo 3 auto al parco e noleggiandone una, il conteggio delle disponibili deve essere esattamente 2.

**Acceptance Scenarios**:

1. **Given** un parco con diverse auto in stati misti, **When** viene richiesto il totale delle disponibili, **Then** il numero restituito corrisponde solo alle auto in stato "Disponibile".

### Edge Cases

- **Noleggio multiplo contemporaneo**: Cosa succede se due tentativi di noleggio avvengono sulla stessa istanza (rilevante per la concurrency, ma qui gestito tramite logica di stato).
- **Parco vuoto**: Il conteggio delle disponibili deve restituire 0 senza errori.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Il sistema DEVE tracciare lo stato di ogni auto (Disponibile o Noleggiata).
- **FR-002**: Il sistema DEVE permettere il noleggio solo di auto in stato Disponibile.
- **FR-003**: Il sistema DEVE impedire il noleggio di auto già occupate, segnalando l'errore (tramite Eccezione in OOP o Result in FP).
- **FR-004**: Il sistema DEVE permettere la restituzione di un'auto rendendola nuovamente Disponibile.
- **FR-005**: Il sistema DEVE fornire il conteggio totale delle sole auto Disponibili nel parco mezzi.
- **FR-006**: Il sistema DEVE assegnare un identificativo univoco tecnico (GUID) ad ogni auto per garantirne l'identità stabile.
- **FR-007**: Il sistema DEVE gestire la Targa come attributo di dominio distinto dall'identità tecnica.

### Key Entities *(include if feature involves data)*

- **Auto**: Rappresenta il veicolo individuale, ora arricchito con l'informazione del suo stato operativo.
- **ParcoMezzi**: La collezione di auto, responsabile dell'aggregazione dei dati (es. conteggio dei disponibili).

## Success Criteria *(obbligatorio)*

### Risultati Misurabili

- **SC-001**: Il 100% delle operazioni di noleggio su auto disponibili deve andare a buon fine, portando allo stato "Noleggiata".
- **SC-002**: Il 100% dei tentativi di noleggio su auto già occupate deve essere bloccato con un errore esplicito.
- **SC-003**: Il conteggio delle auto disponibili deve riflettere istantaneamente i cambiamenti di stato (consistenza immediata).
- **SC-004**: L'identità tecnica (GUID) deve rimanere invariata per tutta la durata delle transizioni di stato.

## Assunzioni

- Un'auto appena inserita nel parco mezzi è considerata "Disponibile" per impostazione predefinita.
- La Targa è considerata univoca a livello di business, sebbene l'identità tecnica sia garantita dal GUID.
- Non è possibile eliminare un'auto dal parco mentre è in stato "Noleggiata" (vincolo di integrità).
- Lo stato "Disponibile" implica che il mezzo sia pronto all'uso (senza necessità di manutenzione in questa fase).