# Task Granulari: Fase 2 – Disponibilità e Identità

## 0. Setup & Definizioni Comuni
- [ ] Definizione enum `StatoAuto` (Disponibile, Noleggiata) <!-- id: 0 -->

## 1. Percorso OOP (C# Classico)
### 1.1 Evoluzione dell'Entità Auto
- [ ] **Red**: Test costruttore `Auto` richiede GUID e Targa <!-- id: 1 -->
- [ ] **Green**: Implementazione proprietà `Id` (GUID) e `Targa` in `Auto.cs` <!-- id: 2 -->

### 1.2 Gestione dello Stato e Noleggio
- [ ] **Red**: Test `Noleggia` cambia stato in `Noleggiata` <!-- id: 3 -->
- [ ] **Green**: Implementazione proprietà `Stato` e metodo `Noleggia` <!-- id: 4 -->
- [ ] **Red**: Test `Noleggia` lancia `InvalidOperationException` se già noleggiata <!-- id: 5 -->
- [ ] **Green**: Aggiunta guardia di validazione in `Noleggia` <!-- id: 6 -->

### 1.3 Restituzione
- [ ] **Red**: Test `Restituisci` riporta lo stato a `Disponibile` <!-- id: 7 -->
- [ ] **Green**: Implementazione metodo `Restituisci` <!-- id: 8 -->

### 1.4 Monitoraggio Parco
- [ ] **Red**: Test `TotaleDisponibili` conta solo auto in stato `Disponibile` <!-- id: 9 -->
- [ ] **Green**: Implementazione logica di filtraggio in `ParcoMezzi.cs` <!-- id: 10 -->

## 2. Percorso Funzionale (C# Moderno)
### 2.1 Evoluzione del Record Auto
- [ ] **Red**: Test creazione `Auto` con GUID, Targa e Stato <!-- id: 11 -->
- [ ] **Green**: Aggiornamento record `Auto` in `Auto.cs` <!-- id: 12 -->

### 2.2 Logica di Noleggio (ROP)
- [ ] **Red**: Test (Property) `Noleggia` restituisce `Success` con nuovo stato <!-- id: 13 -->
- [ ] **Green**: Creazione `AutoExtensions.cs` e funzione `Noleggia` <!-- id: 14 -->
- [ ] **Red**: Test `Noleggia` restituisce `Failure` se già noleggiata <!-- id: 15 -->
- [ ] **Green**: Implementazione logica di errore ROP (Result pattern) <!-- id: 16 -->

### 2.3 Restituzione e Monitoraggio
- [ ] **Red**: Test `Restituisci` restituisce `Success` con stato `Disponibile` <!-- id: 17 -->
- [ ] **Green**: Implementazione funzione `Restituisci` <!-- id: 18 -->
- [ ] **Red**: Test `ConteggioDisponibili` riflette lo stato dei record <!-- id: 19 -->
- [ ] **Green**: Implementazione query LINQ in `ParcoMezziExtensions.cs` <!-- id: 20 -->

## 3. Conclusione e Raffinamento Didattico
- [ ] **Documentazione**: Stesura `comparison.md` con analisi del contrasto Mutazione vs Transizione <!-- id: 18 -->
- [ ] **Documentazione**: Aggiornamento `README.md` con recap dettagliato della Fase 2 (step, concetti, motivazioni OOP/FP) <!-- id: 25 -->
- [ ] **Refactor**: Revisione finale di tutti i commenti "perché" in italiano per l'audience <!-- id: 17 -->
- [ ] **Chore**: Chiusura branch, merge su `master` e creazione tag `002-fase-2-disponibilita` <!-- id: 19 -->