# Specifica della Funzionalità: Fase 1 - Gestione Parco Mezzi Minimale

**Branch della Funzionalità**: `001-parco-mezzi`  
**Creato**: 12-01-2026  
**Stato**: Draft  
**Input**: Descrizione utente: "Implementare la Fase 1 (Gestione Parco Mezzi Minimale) sia con approccio OOP che Funzionale"

## Chiarimenti

### Sessione 12-01-2026
- D: Come dovrebbe il sistema identificare l'auto da rimuovere in questa fase? → R: **Uguaglianza**: Utilizzare l'uguaglianza a livello di linguaggio (Riferimento per OOP, Valore per Funzionale).

## Scenari Utente e Test *(obbligatorio)*

### User Story 1 - Popolamento Base del Parco Mezzi (Priorità: P1)

Come gestore del parco mezzi, voglio poter aggiungere auto al mio parco inizialmente vuoto in modo da poter iniziare a gestire i miei asset.

**Perché questa priorità**: Questo è il blocco fondamentale del sistema. Senza la possibilità di aggiungere auto e vederne il conteggio, il sistema non ha alcuna utilità.

**Test Indipendente**: Può essere testato completamente creando un parco mezzi vuoto, aggiungendo un numero noto di auto e verificando che il conteggio totale corrisponda al numero di auto aggiunte.

**Scenari di Accettazione**:

1. **Dato** un nuovo sistema di gestione del parco mezzi, **Quando** controllo il numero totale di auto, **Allora** dovrebbe essere 0.
2. **Dato** un parco mezzi vuoto, **Quando** aggiungo una singola auto, **Allora** il numero totale di auto dovrebbe essere 1.
3. **Dato** un parco mezzi con 1 auto, **Quando** aggiungo un'altra auto, **Allora** il numero totale di auto dovrebbe essere 2.

---

### User Story 2 - Manutenzione Base del Parco Mezzi (Priorità: P2)

Come gestore del parco mezzi, voglio poter rimuovere auto dal mio parco in modo da poter mantenere aggiornato il mio inventario quando le auto vengono vendute o dismesse.

**Perché questa priorità**: La manutenzione del parco mezzi è essenziale per la precisione.

**Test Indipendente**: Può essere testato aggiungendo un'auto, rimuovendola e verificando che il conteggio torni allo stato precedente.

**Scenari di Accettazione**:

1. **Dato** un parco mezzi contenente 1 auto, **Quando** rimuovo quell'auto, **Allora** il numero totale di auto dovrebbe essere 0.
2. **Dato** un parco mezzi contenente più auto, **Quando** rimuovo un'auto specifica, **Allora** il numero totale di auto dovrebbe diminuire di esattamente 1.
3. **Dato** un parco mezzi vuoto, **Quando** tento di rimuovere un'auto, **Allora** l'operazione dovrebbe indicare che l'auto non è stata trovata e il conteggio dovrebbe rimanere 0.

---

### Casi Limite

- **Rimozione da Parco Vuoto**: Il sistema deve gestire con grazia le richieste di rimozione di un'auto quando il parco mezzi è vuoto.
- **Grande Volume**: L'aggiunta sequenziale di 10.000 auto dovrebbe risultare in un conteggio accurato di 10.000 (soddisfacendo SC-002).
- **Input Null**: L'aggiunta di un riferimento "null" o di un'auto non valida (se applicabile all'approccio) dovrebbe essere gestita secondo le migliori pratiche specifiche del paradigma (es. ignorata o con conseguente errore).

## Requisiti *(obbligatorio)*

### Requisiti Funzionali

- **FR-001**: Il sistema DEVE supportare uno stato inizialmente vuoto per il parco mezzi.
- **FR-002**: Il sistema DEVE consentire l'aggiunta di una singola auto al parco mezzi.
- **FR-003**: Il sistema DEVE consentire la rimozione di una singola auto dal parco mezzi utilizzando l'identificazione basata sull'uguaglianza (uguaglianza per riferimento per OOP, uguaglianza per valore per Funzionale).
- **FR-004**: Il sistema DEVE fornire un modo per recuperare il conteggio totale corrente delle auto nel parco mezzi.

### Entità Chiave *(includere se la funzionalità coinvolge dati)*

- **Auto**: Rappresenta un singolo veicolo nel parco mezzi. L'identità è gestita tramite l'uguaglianza nativa del linguaggio.
- **ParcoMezzi**: Rappresenta la collezione di auto e fornisce le operazioni di gestione.

## Success Criteria *(obbligatorio)*

### Risultati Misurabili

- **SC-001**: Il 100% delle operazioni di "Aggiunta" e "Rimozione" risulta in un aggiornamento accurato del conteggio del parco mezzi.
- **SC-002**: L'operazione di conteggio del parco mezzi deve essere istantanea (inferiore a 10ms) indipendentemente dal numero di auto (fino a 10.000).
- **SC-003**: Il sistema fornisce due implementazioni distinte (OOP e Funzionale) che soddisfano lo stesso insieme di requisiti logici.
- **SC-004**: I test raggiungono un tasso di superamento del 100% (Facts per OOP, Properties per FP).

## Assunzioni

- In questa fase, le auto sono considerate entità/valori atomici senza attributi descrittivi (modello, targa, etc.).
- L'uguaglianza tra auto è puramente tecnica (riferimento per OOP, valore per FP).
- Non ci sono limiti fisici alla dimensione del parco mezzi oltre alla memoria disponibile del sistema.
- Il sistema è inteso per uso single-user in questa fase; la concorrenza non è un requisito primario.
