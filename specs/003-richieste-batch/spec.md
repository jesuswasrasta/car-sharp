# Feature Specification: Batch Requests

**Feature Branch**: `003-richieste-batch`  
**Created**: 2026-01-13  
**Status**: Draft  
**Input**: User description: "Implementa la fase 3"

## Clarifications

### Session 2026-01-13
- Q: Error Detail Granularity → A: Return detailed error identifying the specific vehicle ID(s) that caused failure.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Atomic Processing of Valid Batch (Priority: P1)

As an API consumer, I want to submit a list of rental requests for multiple vehicles so that they are processed as a single unit.

**Why this priority**: This is the core functionality of Phase 3, enabling bulk operations with transactional integrity.

**Independent Test**: Can be tested by submitting a batch of requests for available vehicles and verifying all are rented.

**Acceptance Scenarios**:

1. **Given** a fleet with available vehicles A, B, and C, **When** I submit a batch request to rent A and B, **Then** the operation succeeds AND vehicles A and B become "Rented".
2. **Given** a fleet with vehicles, **When** I submit an empty batch, **Then** the operation succeeds (or fails gracefully) without changing fleet state.

---

### User Story 2 - Atomic Rejection of Invalid Batch (Priority: P1)

As an API consumer, I want the system to reject the entire batch if even one request cannot be fulfilled, ensuring the fleet state remains consistent.

**Why this priority**: Ensures data integrity and prevents partial updates which could leave the system in an inconsistent or difficult-to-rollback state.

**Independent Test**: Can be tested by submitting a batch containing at least one request for an already rented (or non-existent) vehicle.

**Acceptance Scenarios**:

1. **Given** a fleet with available vehicle A and rented vehicle B, **When** I submit a batch request to rent A and B, **Then** the entire operation fails AND vehicle A remains "Available" AND vehicle B remains "Rented".
2. **Given** a fleet, **When** I submit a batch request for a non-existent vehicle ID, **Then** the entire operation fails.

---

### Edge Cases

- **Duplicate Requests**: If a batch contains multiple requests for the same vehicle ID, the system treats it as a conflict (the vehicle cannot be rented twice). The entire batch fails.
- **Batch Size**: For this phase, we assume no hard limit, but the operation must complete within standard timeout limits.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST accept a batch (collection) of rental requests.
- **FR-002**: A "Request" in this phase is defined as an intent to rent a specific vehicle identified by its unique ID.
- **FR-003**: System MUST validate that *all* vehicles in the batch are currently in "Available" state.
- **FR-004**: System MUST apply state changes (Available -> Rented) atomically: either all vehicles in the batch are updated, or none are.
- **FR-005**: System MUST return a success indicator if the batch is processed.
- **FR-006**: System MUST return a failure indicator if any request in the batch is invalid (vehicle not found or not available). The indicator MUST include the specific vehicle ID(s) that caused the failure.
- **FR-007**: System MUST NOT apply partial updates; if a batch fails, the fleet state must be identical to the state before the operation.

### Key Entities *(include if feature involves data)*

- **Batch**: A collection of individual rental requests treated as a single unit of work.
- **Request**: A command to rent a specific vehicle ID.

## Success Criteria *(obbligatorio)*

### Risultati Misurabili

- **SC-001**: Il 100% delle operazioni batch valide deve risultare nel noleggio di tutte le auto richieste.
- **SC-002**: Se una sola richiesta nel batch fallisce, lo stato del parco deve rimanere identico al 100% (nessuna mutazione parziale).
- **SC-003**: Il sistema deve identificare correttamente duplicati di ID all'interno dello stesso batch, rifiutando l'operazione.
- **SC-004**: Il tempo di processamento di un batch deve crescere linearmente con il numero di richieste (O(N)).

## Assunzioni

- Un batch vuoto è considerato un'operazione valida che non produce cambiamenti di stato.
- L'ordine delle richieste all'interno del batch è preservato durante l'elaborazione.
- In questa fase, non ci sono limiti al numero di auto che un singolo cliente può richiedere in un batch.
- L'atomicità è garantita all'interno della singola istanza del parco mezzi (in-memory transaction).