# Confronto Architetturale - Fase 4: Tipologie e dimensioni

## Obiettivo
Introdurre la capacità (posti) come attributo del mezzo e vincolo per il noleggio.

## Approccio OOP: Entità con Attributi e Validazione Imperativa

### Caratteristiche
1.  **Stato**: L'entità `Auto` acquisisce una proprietà immutabile `Capacita`. La consistenza è garantita dal costruttore (fail-fast).
2.  **Assegnazione**: Il `ParcoMezzi` agisce come coordinatore, cercando nella lista interna il primo oggetto che soddisfa i criteri (`Stato == Disponibile && Capacita >= postiMinimi`).
3.  **Vincoli Incrociati**: Se una richiesta specifica sia l'ID che la capacità, il sistema esegue una validazione esplicita lanciando `InvalidOperationException` se il mezzo non è idoneo.

### Codice
```csharp
var autoIdonea = _auto.FirstOrDefault(a => 
    a.Stato == StatoAuto.Disponibile && 
    a.Capacita >= richiesta.PostiMinimi);
```

## Approccio FP: Type-Driven Constraints e Property-Based Verification

### Caratteristiche
1.  **Dati**: La capacità è parte del record `IAuto`. Le estensioni assicurano che la capacità persista durante le transizioni di stato (proiezione dei dati).
2.  **Richiesta come Dato**: Abbiamo introdotto il record `RichiestaNoleggio` per uniformare l'input delle pipeline.
3.  **Pipeline Filtrata**: Il matching avviene tramite LINQ e Pattern Matching. Il risultato è un `Result<ParcoMezzi>` che incapsula il successo o il motivo del fallimento (es. capacità insufficiente).
4.  **Verifica**: Grazie a FsCheck, abbiamo dimostrato che il sistema non può mai assegnare un'auto con meno posti di quelli richiesti, indipendentemente dai parametri generati casualmente.

### Codice
```csharp
autoTrovata switch
{
    AutoDisponibile d when d.Capacita >= richiesta.PostiMinimi => Success(...),
    AutoDisponibile => Failure("Capacità insufficiente"),
    _ => Failure("Stato non valido")
};
```

## Analisi Comparativa

| Dimensione | OOP | FP |
|------------|-----|----|
| **Estendibilità** | Semplice aggiunta di proprietà all'oggetto. | Richiede l'aggiornamento dei record e delle funzioni di proiezione. |
| **Sicurezza** | Basata su controlli `if` e eccezioni a runtime. | Basata su tipi e filtri nella pipeline (Railway Oriented Programming). |
| **Performance** | Accesso diretto alla lista mutabile, molto veloce. | Creazione di nuovi record e collezioni immutabili ad ogni noleggio. |
| **Testabilità** | Richiede scenari di esempio specifici (Example-based). | Si presta naturalmente alla verifica di proprietà invarianti (Property-based). |