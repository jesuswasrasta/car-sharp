# Feature Specification: Clienti e Sconti

**Feature Branch**: `007-clienti-sconti`
**Created**: 2026-01-30
**Status**: Draft
**Input**: User description: "Fase 7 – Clienti e sconti: ogni prenotazione è associata a un cliente. Se un cliente prenota più mezzi nello stesso batch, ottiene uno sconto percentuale. Lo sconto si applica al totale del cliente, non ai singoli mezzi. Il prezzo finale non può mai essere negativo."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Associazione prenotazione a cliente (Priority: P1)

Il sistema deve permettere di associare ogni prenotazione a un cliente identificato. Quando viene effettuata una prenotazione, il cliente viene registrato come titolare del noleggio. Questa è la base fondamentale per tutte le funzionalità successive (sconti, storico, fatturazione).

**Why this priority**: Senza l'associazione cliente-prenotazione, non è possibile calcolare sconti per cliente né gestire prenotazioni multiple dello stesso cliente in un batch.

**Independent Test**: Può essere testato verificando che ogni prenotazione completata contenga l'identificativo del cliente e che il cliente sia recuperabile dal risultato del noleggio.

**Acceptance Scenarios**:

1. **Given** un parco mezzi con almeno un'auto disponibile, **When** un cliente effettua una prenotazione specificando il proprio identificativo, **Then** il risultato del noleggio include l'identificativo del cliente.
2. **Given** un batch di prenotazioni, **When** ogni prenotazione specifica un cliente, **Then** ogni risultato di noleggio nel batch è associato al rispettivo cliente.

---

### User Story 2 - Sconto percentuale per prenotazioni multiple (Priority: P2)

Se un cliente prenota più di un mezzo nello stesso batch, ottiene uno sconto percentuale sul totale dei suoi noleggi. Lo sconto incentiva i clienti a effettuare prenotazioni aggregate anziché singole.

**Why this priority**: Lo sconto è il valore aggiunto principale di questa fase, ma richiede che l'associazione cliente-prenotazione (US1) sia già implementata.

**Independent Test**: Può essere testato creando un batch con più prenotazioni dello stesso cliente e verificando che il costo totale sia ridotto della percentuale di sconto applicata.

**Acceptance Scenarios**:

1. **Given** un batch con 2 prenotazioni dello stesso cliente e uno sconto del 10%, **When** il batch viene processato, **Then** il costo totale del cliente è ridotto del 10%.
2. **Given** un batch con 3 prenotazioni di cui 2 dello stesso cliente, **When** il batch viene processato, **Then** solo il cliente con 2 prenotazioni riceve lo sconto, l'altro paga il prezzo pieno.
3. **Given** un batch con 1 sola prenotazione per cliente, **When** il batch viene processato, **Then** nessuno sconto viene applicato.

---

### User Story 3 - Prezzo finale non negativo (Priority: P3)

Il sistema deve garantire che il prezzo finale di una prenotazione non possa mai essere negativo, anche in caso di sconti elevati o errori di configurazione. Questo protegge l'integrità economica del sistema.

**Why this priority**: È un invariante di protezione che interviene solo in casi limite; la maggior parte delle operazioni normali non lo attiva.

**Independent Test**: Può essere testato applicando sconti estremi (100% o superiori) e verificando che il prezzo finale sia sempre >= 0.

**Acceptance Scenarios**:

1. **Given** un cliente con sconto del 100%, **When** prenota un mezzo, **Then** il prezzo finale è 0 (non negativo).
2. **Given** una percentuale di sconto negativa o > 100%, **When** si tenta di processare il batch, **Then** il sistema genera un errore di validazione.

---

### Edge Cases

- Cosa succede se un cliente prenota lo stesso mezzo due volte nello stesso batch? (Errore: duplicato già gestito dalla Fase 3)
- Cosa succede se lo sconto percentuale non è definito? (Default: 0%, nessuno sconto)
- Cosa succede con percentuale sconto < 0% o > 100%? (Errore di validazione)
- Cosa succede se il cliente non è specificato in una richiesta di noleggio? (Errore: il cliente è obbligatorio per questa fase)
- Cosa succede con un batch misto di clienti diversi? (Ogni cliente riceve lo sconto in base al proprio numero di prenotazioni)

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Ogni RichiestaNoleggio DEVE includere un identificativo cliente.
- **FR-002**: Ogni RisultatoNoleggio DEVE includere l'identificativo del cliente associato.
- **FR-003**: Il sistema DEVE calcolare il numero di prenotazioni per ogni cliente in un batch.
- **FR-004**: Se un cliente ha più di una prenotazione nel batch, il sistema DEVE applicare uno sconto percentuale al totale del cliente.
- **FR-005**: Lo sconto DEVE essere applicato al totale del cliente, non ai singoli mezzi.
- **FR-006**: Il prezzo finale (dopo sconto) NON PUÒ essere negativo; il floor è 0.
- **FR-007**: Il RisultatoBatch DEVE riportare il costo totale per cliente (pre e post sconto) e il costo totale complessivo.
- **FR-008**: Clienti con una sola prenotazione nel batch NON ricevono sconto.
- **FR-009**: La percentuale di sconto DEVE essere compresa tra 0% e 100% inclusi; valori fuori range generano errore.

### Key Entities

- **Cliente**: Identificato univocamente da una stringa (`ClienteId: string`). Può effettuare una o più prenotazioni.
- **RichiestaNoleggio** (estesa): Aggiunge il campo `ClienteId` (string) per associare la richiesta a un cliente.
- **RisultatoNoleggio** (esteso): Aggiunge il campo `ClienteId` (string) per tracciare il titolare del noleggio.
- **RisultatoBatch** (esteso): Raggruppa i costi per cliente, mostrando il costo lordo, lo sconto applicato e il costo netto.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Ogni prenotazione completata riporta correttamente l'identificativo del cliente.
- **SC-002**: Il costo totale di un cliente con N prenotazioni (N > 1) è pari a (somma costi singoli) × (1 - percentuale sconto).
- **SC-003**: Il prezzo finale non è mai negativo, anche con sconti del 100% o superiori.
- **SC-004**: Il costo totale del batch è la somma dei costi netti di tutti i clienti.
- **SC-005**: L'atomicità del batch è preservata: se una prenotazione fallisce, nessuna viene applicata.

## Clarifications

### Session 2026-01-30

- Q: Tipo di identificativo cliente (string vs Guid)? → A: `string`
- Q: Range valido per percentuale sconto? → A: 0-100% (valori fuori range generano errore)

## Assumptions

- La percentuale di sconto sarà un parametro configurabile passato al metodo di noleggio batch, non hardcoded nel sistema.
- Il cliente è identificato da una stringa (`ClienteId: string`); non è necessaria un'entità Cliente complessa con attributi aggiuntivi per questa fase.
- Lo sconto si applica solo quando un cliente ha **più di una** prenotazione nel batch (non con una sola).
- Le regole di sconto sono uniformi per tutti i clienti (stessa percentuale).
