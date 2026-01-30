# Confronto: Fase 2 - Disponibilità e Identità

In questa fase, il contrasto tra l'approccio Object-Oriented e quello Funzionale si sposta dalla gestione delle collezioni alla rappresentazione dello **Stato** e dell'**Identità**.

## 1. Identità Tecnica vs Identità di Dominio
Entrambi i paradigmi hanno adottato la distinzione tra `Guid` (identità tecnica stabile) e `Targa` (attributo di dominio).
- In **OOP**, l'identità è garantita dal riferimento all'oggetto in memoria, ma il `Guid` fornisce una base solida per la persistenza futura.
- In **FP**, dove i record vengono ricreati ad ogni trasformazione, il `Guid` è l'unico modo per tracciare la "stessa" auto attraverso i suoi cambiamenti di stato.

## 2. Stato come Proprietà (OOP) vs Stato come Tipo (FP)
Questa è la divergenza più significativa di questa fase.

### Approccio OOP: Mutazione dello Stato
L'auto è un'entità che "cambia" internamente.
- **Meccanismo**: Un `enum StatoAuto` e una proprietà mutabile `Stato`.
- **Validazione**: Il metodo `Noleggia()` controlla lo stato corrente e lancia un'eccezione (`InvalidOperationException`) se l'operazione non è permessa.
- **Pro**: Intuitivo, modella bene il cambiamento fisico dell'oggetto nel tempo.
- **Contro**: Lo stato "illegale" è rappresentabile (un'auto noleggiata ha comunque il metodo `Noleggia` esposto), e bisogna proteggersi con guardie manuali (eccezioni).

### Approccio Funzionale: Trasformazione di Tipo
L'auto non cambia mai; viene invece trasformata in un "dato" differente.
- **Meccanismo**: Due tipi distinti, `AutoDisponibile` e `AutoNoleggiata`, che implementano `IAuto`.
- **Validazione**: È intrinseca nel sistema dei tipi (**Type-Driven Design**). La funzione di estensione `Noleggia` accetta solo `AutoDisponibile`. Non è possibile (errore di compilazione) chiamare `Noleggia` su una `AutoNoleggiata`.
- **Pro**: "Rende gli stati illegali irrappresentabili". La logica di business è codificata nella firma delle funzioni.
- **Contro**: Richiede una gestione più complessa a livello di collezioni (uso di interfacce o pattern matching per distinguere i tipi eterogenei nel parco mezzi).

## 3. Gestione degli Errori
- **OOP**: Utilizza le **Eccezioni**. È un approccio "interruttivo": se qualcosa non va (es. noleggio su auto occupata), il flusso si ferma e risale lo stack.
- **FP**: In questa fase specifica, la trasformazione di tipo ha reso alcuni errori impossibili (non compila), eliminando la necessità di gestire l'errore a runtime per quei casi. Per altri casi (es. rimozione), si continua a usare `Result` (Railway-Oriented Programming).