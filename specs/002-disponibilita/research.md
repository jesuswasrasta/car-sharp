# Ricerca e Decisioni Tecniche: Fase 2 – Disponibilità e Identità

## Decisione 1: Identità Tecnica vs Identità di Dominio
- **Decisione**: Ogni auto avrà un `Guid Id` immutabile (identità tecnica) e una `string Targa` (identità di dominio).
- **Motivazione**: Utilizzare la targa come identificativo tecnico è un errore comune. Le targhe possono cambiare (reimmatricolazione), possono contenere errori di inserimento che richiedono correzioni, o possono essere assenti (auto nuove). Un GUID tecnico garantisce stabilità referenziale per tutta la vita dell'oggetto nel sistema, indipendentemente dai cambiamenti nei dati di dominio.
- **Alternative considerate**: 
    - Usare solo la Targa: Rifiutato perché viola la separazione tra identità tecnica e attributi di business.
    - Usare un `int` incrementale: Rifiutato perché meno adatto a sistemi distribuiti o persistenza asincrona rispetto a un GUID.

## Decisione 2: Stato e Transizioni
- **Decisione**: Lo stato sarà gestito tramite un `enum StatoAuto` (`Disponibile`, `Noleggiata`).
- **Motivazione**: Semplicità e chiarezza didattica.
- **Percorso OOP**: Mutazione dello stato interno e lancio di `InvalidOperationException` per transizioni non valide.
- **Percorso Funzionale**: Transizione tramite record e `with`, utilizzando `Risultato<Auto>` per gestire i fallimenti (ROP).

## Decisione 3: Validazione
- **Decisione**: Il costruttore/init richiederà obbligatoriamente ID e Targa.
- **Motivazione**: Un'auto senza identità tecnica o targa è considerata invalida nel nostro dominio a partire da questa fase.