# Confronto Paradigmi: Fase 2 – Disponibilità e Identità

In questa fase, il contrasto tra l'approccio Object-Oriented e quello Funzionale si è spostato dalla gestione delle collezioni alla rappresentazione dello **Stato** e dell'**Identità**.

## 1. Identità Tecnica vs Identità di Dominio
Entrambi i paradigmi hanno adottato la distinzione tra `Guid` (identità tecnica stabile) e `Targa` (attributo di dominio). 
- In **OOP**, l'identità è garantita dal riferimento all'oggetto in memoria, ma il `Guid` fornisce una base solida per la persistenza futura.
- In **FP**, dove i record vengono ricreati ad ogni trasformazione, il `Guid` è l'unico modo per tracciare la "stessa" auto attraverso i suoi cambiamenti di stato.

## 2. Stato come Proprietà (OOP) vs Stato come Tipo (FP)
Questa è la divergenza più significativa di questa fase.

### Approccio OOP: Mutazione dello Stato
L'auto è un'entità che "cambia" internamente.
- **Meccanismo**: Un `enum StatoAuto` e una proprietà mutabile.
- **Validazione**: Il metodo `Noleggia()` deve controllare lo stato corrente e lanciare un'eccezione (`InvalidOperationException`) se l'operazione non è permessa.
- **Pro**: Intuitivo, modella bene il cambiamento fisico dell'oggetto.
- **Contro**: Lo stato "illegale" è rappresentabile (un'auto noleggiata ha comunque il metodo noleggia), e bisogna proteggersi con guardie manuali.

### Approccio Funzionale: Trasformazione di Tipo
L'auto non cambia mai; viene invece trasformata in un "dato" differente.
- **Meccanismo**: Due tipi distinti, `AutoDisponibile` e `AutoNoleggiata`.
- **Validazione**: È intrinseca nel sistema dei tipi (**Type-Driven Design**). La funzione `Noleggia` accetta solo `AutoDisponibile`. Non è possibile (compilation error) chiamare `Noleggia` su una `AutoNoleggiata`.
- **Pro**: "Rende gli stati illegali irrappresentabili". La logica di business è codificata nella firma delle funzioni.
- **Contro**: Richiede una gestione più complessa a livello di collezioni (uso di interfacce o pattern matching per distinguere i tipi nel parco mezzi).

## 3. Gestione degli Errori
- **OOP**: Utilizza le **Eccezioni**. È un approccio "interruttivo": se qualcosa non va, il flusso si ferma e risale lo stack.
- **FP**: Utilizza il **Pattern Result** (Railway-Oriented Programming). L'errore è un valore di ritorno che deve essere gestito esplicitamente, mantenendo il flusso lineare. In questa fase, la trasformazione di tipo ha reso alcuni errori impossibili, semplificando ulteriormente la logica.
