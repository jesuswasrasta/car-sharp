# Modello Dati: Richieste Batch (Fase 3)

**Funzionalità**: Richieste Batch (Fase 3)
**Sorgente**: `spec.md`

## Entità

### Batch
- **Rappresentazione**: `IEnumerable<Guid>` (Lista di Targhe).
- **Validazione**: 
    - Non deve contenere duplicati.
    - Tutte le targhe devono corrispondere ad auto esistenti.

### Request
- **Tipo**: `Guid` (Identificativo Tecnico/Targa).

## Transizioni di Stato

### OOP (CarSharp.Oop)
- **Metodo**: `void Noleggia(IEnumerable<Guid> targhe)` sulla classe `ParcoMezzi`.
- **Comportamento**: 
    1. Validazione (Check): verifica che tutte le auto siano disponibili.
    2. Esecuzione (Act): itera e chiama `.Noleggia()` su ogni istanza di `Auto`.
    3. Eccezione se la validazione fallisce (nessuna mutazione avviene).

### FP (CarSharp.Functional)
- **Funzione**: `Result<ParcoMezzi, Error> NoleggiaBatch(IEnumerable<Guid> targhe)` come extension method o metodo statico.
- **Comportamento**:
    1. `targhe.Aggregate(...)` partendo da `Result.Success(this)`.
    2. Ogni step applica `Bind(parco => parco.Noleggia(targa))`.
    3. Se un'auto non è disponibile, il `Result` diventa `Failure` e viene propagato fino alla fine.
    4. Il parco originale non viene mai toccato.