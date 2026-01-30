# Fase 1: Gestione Parco Mezzi Minimale
## Il Viaggio Inizia

Benvenuti alla prima fase di **CarSharp**. In questa fase, gettiamo le basi per il nostro studio comparativo dei paradigmi di programmazione.

### L'Obiettivo
Implementare un sistema di gestione del parco mezzi che possa:
1.  Inizializzare un **parco mezzi vuoto**.
2.  **Aggiungere** un'auto al parco mezzi.
3.  **Rimuovere** un'auto specifica dal parco mezzi.
4.  Tracciare il **conteggio totale** delle auto.

### I Paradigmi
-   **OOP (Programmazione Orientata agli Oggetti)**: Ci concentreremo sull'**incapsulamento** e sullo **stato mutabile**. Il `ParcoMezzi` conterrà una lista gestita internamente.
-   **Programmazione Funzionale**: Ci concentreremo sull'**immutabilità** e sulle **funzioni pure**. Ogni operazione sul `ParcoMezzi` restituirà un *nuovo* parco mezzi, lasciando invariato l'originale.

### Concetto Chiave: Identità
In questa fase, le auto non hanno proprietà (come Targa o Modello).
-   In **OOP**, distinguiamo le auto in base al loro **riferimento in memoria**.
-   In **Funzionale**, le trattiamo come **valori**. (Sebbene in questa fase siano record vuoti, le tratteremo come token di valore distinti).

Vediamo come questi due modelli mentali divergono nel codice.
