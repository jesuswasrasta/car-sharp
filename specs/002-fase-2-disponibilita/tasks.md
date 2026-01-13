# Task Granulari: Fase 2 – Disponibilità e Identità

## 0. Setup & Definizioni
- [ ] **OOP**: Definizione enum `StatoAuto` (Disponibile, Noleggiata) <!-- id: 0 -->
- [ ] **Functional**: Definizione dei tipi `AutoDisponibile` e `AutoNoleggiata` <!-- id: 11 -->

## 1. Percorso OOP (C# Classico)
### 1.1 Evoluzione dell'Entità Auto
- [ ] **Red**: Test costruttore `Auto` richiede Id, Targa e Stato iniziale <!-- id: 1 -->
- [ ] **Green**: Implementazione proprietà `Id`, `Targa` e `Stato` in `Auto.cs` <!-- id: 2 -->

### 1.2 Gestione dello Stato e Noleggio
- [ ] **Red**: Test `Noleggia` cambia stato in `Noleggiata` <!-- id: 3 -->
- [ ] **Green**: Implementazione metodo `Noleggia` con validazione (Exception) <!-- id: 4 -->

### 1.3 Restituzione
- [ ] **Red**: Test `Restituisci` riporta lo stato a `Disponibile` <!-- id: 7 -->
- [ ] **Green**: Implementazione metodo `Restituisci` <!-- id: 8 -->

### 1.4 Monitoraggio Parco
- [ ] **Red**: Test `TotaleDisponibili` conta solo auto in stato `Disponibile` <!-- id: 9 -->
- [ ] **Green**: Implementazione logica di filtraggio in `ParcoMezzi.cs` <!-- id: 10 -->

## 2. Percorso Funzionale (C# Moderno)
### 2.1 Trasformazioni di Tipo (Type-Driven Design)
- [ ] **Red**: Test (Property) `Noleggia` trasforma `AutoDisponibile` in `AutoNoleggiata` <!-- id: 13 -->
- [ ] **Green**: Implementazione funzione `Noleggia` in `AutoExtensions.cs` <!-- id: 14 -->
- [ ] **Red**: Test `Restituisci` trasforma `AutoNoleggiata` in `AutoDisponibile` <!-- id: 17 -->
- [ ] **Green**: Implementazione funzione `Restituisci` <!-- id: 18 -->

### 2.2 Monitoraggio Parco (Pattern Matching)
- [ ] **Red**: Test `ConteggioDisponibili` conta solo istanze di `AutoDisponibile` <!-- id: 19 -->
- [ ] **Green**: Implementazione query LINQ con pattern matching in `ParcoMezziExtensions.cs` <!-- id: 20 -->

## 3. Conclusione e Raffinamento Didattico
- [ ] **Documentazione**: Stesura `comparison.md` con analisi del contrasto Mutazione vs Transizione <!-- id: 18 -->
- [ ] **Documentazione**: Aggiornamento `README.md` con recap dettagliato della Fase 2 (step, concetti, motivazioni OOP/FP) <!-- id: 25 -->
- [ ] **Refactor**: Revisione finale di tutti i commenti "perché" in italiano per l'audience <!-- id: 17 -->
- [ ] **Chore**: Chiusura branch, merge su `master` e creazione tag `002-fase-2-disponibilita` <!-- id: 19 -->