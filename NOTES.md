# Prompt di progetto originale
In questo repo Git c'è un piccolo progetto in F#. Si tratta di un esercizio volto ad acquisire competenze sulla programmazione funzionale tramite F#. 
A partire dall informazioni e dal codice presente, vorrei che mi creassi del materiale didattico per condurre una presentazione ad un pubblico di programmatori abituali a C# e alla programmazione OOP (Object Oriented Programming). 
Lo scopo finale della presentazione è mettere a confornto un approccio OOP "classico", con quello funzionale di F#.

Vorrei procedere così.
- Partiamo dalla Fase 1 – Parco mezzi minimale 
  - Implemenetiamo i requisiti indicati nella maniera OOP classica con C#; facciamo TDD con xUnit e approccio "example based", implementiamo un test alla volta, e piano piano arriviamo a costruire la prima bozza della libreria, in grado di gestire gli stati e le regole di business indicate.
  - Giunti a descrivere l'implementazione classica, "cambiamo il cappello" e da programmatore OOP con C# passiamo a "programmatore F# funzionale". Implementiamo ex novo stati e regole di business della libreria, procedendo sempre in TDD. Però stavolta per i test usiamo un approccio "property based testing", usando la libreira FsCheck. Man mano facciamo cose, commentiamo le diversità dei due approcci, i pro e i contro, evidenziando le differenze degli approcci.

E così via, andremo avanti a sviluppare le altre fasi.

Procedi così.
Ragiona per piccoli step: ogni test che implementiamo e risolviamo deve avere a corredo un commento esaustivo, in modo che chi vede il repo in futuro possa capire cosa abbiamo fatto, lo scopo dell'esercizio e dello step, e in modo che io che presento il codice possa ricordarmi cosa dire nel particolare step.
Per fase prepra una piccola intorduzione, un testo che possa usare come slide da presentare al pubblico prima di procedere col codice.
Per ogni step inplementativo segui semrpe i dettami del TDD: prima 1 test rosso che non passa, poi facciamo passare il test con il minimo codice necessario, poi rifattorizziamo. Eseguiamo un commit ad ogni passo del TDD.

Generami un piano dettagliato secondo le isctruzioni che ti ho dato, così poi lo validiamo e passiamo alla implementazione

# Prompt suggerito
Ecco una proposta di piano dettagliato per strutturare il materiale didattico e la presentazione. L'obiettivo è creare una narrazione fluida che accompagni l'audience dalla loro "comfort zone" (C# OOP) verso il nuovo paradigma (F# Funzionale), utilizzando il **Car Renting** come caso studio evolutivo.

---

# Piano di Lavoro: Fase 1 – Parco Mezzi Minimale

## 1. Introduzione e Slide di Apertura

Prima di scrivere codice, definiremo il contenuto della slide introduttiva per questa fase.

* **Titolo:** Fase 1 - Gestione del Parco Mezzi.
* **Contesto:** Definizione dei requisiti minimi (Aggiunta, Rimozione, Conteggio).
* **Obiettivo Didattico:**
* Osservare la gestione dello **Stato Mutabile** (C#) vs **Stato Immutabile** (F#).
* Confrontare **Example-Based Testing** (xUnit standard) vs **Property-Based Testing** (FsCheck).



---

## 2. Sessione C# (OOP Classica)

In questa sezione agiremo come "Sviluppatori Enterprise C#". Useremo classi, incapsulamento e stato interno.

* **Step 1.1: Setup e Zero State (TDD Red)**
* *Azione:* Creiamo un test `NewFleet_Should_BeEmpty`.
* *Commento:* Verifichiamo che una nuova istanza di `Fleet` parta da zero.
* *Commit:* `C# Phase 1: Test NewFleet empty`


* **Step 1.2: Implementazione Base (TDD Green/Refactor)**
* *Azione:* Creiamo la classe `Fleet` con una lista interna privata inizializzata vuota e una proprietà `Count`.
* *Commento:* Incapsulamento dello stato.
* *Commit:* `C# Phase 1: Impl Fleet class`


* **Step 1.3: Aggiunta Mezzi (TDD Red)**
* *Azione:* Test `Add_Should_IncrementCount`. Aggiungiamo un oggetto `Car` (classe vuota per ora) e verifichiamo `Count == 1`.
* *Commit:* `C# Phase 1: Test Add increments count`


* **Step 1.4: Implementazione Add (TDD Green)**
* *Azione:* Implementiamo metodo `void Add(Car car)` che modifica la lista interna.
* *Commento:* Side-effect: il metodo non restituisce nulla, cambia lo stato del mondo.
* *Commit:* `C# Phase 1: Impl Add method`


* **Step 1.5: Rimozione Mezzi (TDD Red)**
* *Azione:* Test `Remove_ExistingCar_Should_DecrementCount`.
* *Commit:* `C# Phase 1: Test Remove decrements count`


* **Step 1.6: Implementazione Remove (TDD Green)**
* *Azione:* Implementiamo `bool Remove(Car car)`.
* *Commento:* Gestione del ritorno (successo/fallimento) tramite bool o eccezioni (tipico C#).
* *Commit:* `C# Phase 1: Impl Remove method`



---

## 3. Sessione F# (Funzionale & Property Based)

Qui "cambiamo cappello". Non scriviamo classi che *contengono* dati, ma tipi e funzioni che *trasformano* dati. Introduciamo **FsCheck**.

* **Step 1.7: Definizione Tipi e Zero State (TDD Red/Green)**
* *Azione:* Definiamo il tipo `Fleet` (wrapper di lista) e `Car` (per ora semplice tipo unit o dummy). Creiamo un test semplice (Fact) `empty fleet has zero count`.
* *Commento:* Differenza tra Classe (comportamento + dati) e Tipo (solo dati). `emptyFleet` è un valore, non un'istanza mutabile.
* *Commit:* `F# Phase 1: Types and Empty Fleet`


* **Step 1.8: Introduzione Property Based Testing (Aggiunta)**
* *Concetto Slide:* Spiegazione rapida di PBT. "Non testiamo che 1+1=2. Testiamo che x+1 > x per ogni x".
* *Azione (TDD Red):* Scriviamo una **Proprietà**: `adding a car increments count`. FsCheck genererà N flotte e N auto random.
* *Commit:* `F# Phase 1: Property Add increments count`


* **Step 1.9: Implementazione Add (TDD Green)**
* *Azione:* Implementiamo `add car fleet`.
* *Commento Cruciale:* La funzione deve restituire una **nuova** `Fleet`. L'originale non cambia. Immutabilità.
* *Commit:* `F# Phase 1: Impl Add (Immutable)`


* **Step 1.10: Proprietà di Invarianza (Rimozione)**
* *Azione (TDD Red):* Proprietà: `removing a car previously added returns original count`.
* *Commento:* Questo test verifica la reversibilità dell'azione, una proprietà forte del sistema.
* *Commit:* `F# Phase 1: Property Remove restores state`


* **Step 1.11: Implementazione Remove con Result (TDD Green)**
* *Azione:* Implementiamo `remove`.
* *Design Choice:* Invece di `bool` o eccezioni (come in C#), usiamo il tipo `Result<Fleet, Error>` per gestire il caso "auto non trovata" in modo funzionale (Railway Oriented Programming intro).
* *Commit:* `F# Phase 1: Impl Remove with Result pattern`



---

## 4. Wrap-up e Confronto

Finale della Fase 1.

* **Slide di confronto:**
* **Stato:** Mutabile (C#) vs Immutabile (F#).
* **Firma Metodi:** `void Add(Car)` (C#) vs `Fleet -> Fleet` (F#).
* **Testing:** Esempi specifici vs Verità universali (Properties).
* **Error Handling:** Eccezioni/Bool vs Tipi espliciti (`Result`).

