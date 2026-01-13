# Piano Tecnico: Tipologie e dimensioni dei mezzi (Fase 4)

## Architettura e Design

### OOP Track
L'obiettivo è estendere l'entità `Auto` e la logica di `ParcoMezzi` per supportare la capacità.
- **Modifiche a `Auto`**: Aggiunta proprietà `Capacita` (read-only dopo la costruzione).
- **Modifiche a `ParcoMezzi`**: 
    - Nuovo metodo `Noleggia(int postiMinimi)` che trova la prima auto disponibile con capacità sufficiente.
    - Aggiornamento `NoleggiaBatch` per supportare richieste basate sulla capacità.

### Functional Track
Applicheremo il Type-Driven Design per rendere i vincoli di capacità espliciti.
- **Modifiche ai Tipi**: 
    - Il record `Auto` (e le sue specializzazioni) includerà `Capacita`.
    - Definizione di `RichiestaNoleggio` come record per incapsulare i requisiti.
- **Logica**:
    - Funzione `TrovaAutoIdonea` che opera sul `ParcoMezzi` e restituisce un `Option<Auto>`.
    - La pipeline di `NoleggiaBatch` userà questa logica per trasformare lo stato.

## Strategia di Testing

### OOP (Example-Based)
- Test per aggiunta auto con diverse capacità.
- Test per noleggio singolo: successo con capacità esatta, successo con capacità superiore, fallimento con capacità insufficiente.
- Test per batch: atomicità con vincoli di capacità violati.

### FP (Property-Based)
- **Proprietà**: "Per ogni noleggio effettuato con successo, l'auto assegnata deve avere una capacità >= ai posti richiesti".
- **Proprietà**: "Se un batch fallisce per capacità, lo stato del parco mezzi deve restare identico all'originale".

## Considerazioni sulla Consistenza
- **OOP**: La validazione nel `NoleggiaBatch` (Check-Then-Act) deve ora includere la simulazione dell'assegnazione per garantire che non vengano promessi più posti di quelli disponibili.
- **FP**: La natura immutabile dello stato e il binding monadico gestiscono naturalmente l'atomicità senza logica di validazione preventiva complessa (oltre al parsing dell'input).
