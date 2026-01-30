# Research: Clienti e Sconti

**Branch**: `007-clienti-sconti`
**Date**: 2026-01-30

## 1. Gestione dello Sconto

**Problem**: Come passare e applicare lo sconto percentuale?
**Decision**: Lo sconto sarà un parametro del metodo di processamento batch (`decimal`).
**Rationale**: Le specifiche indicano che è un parametro configurabile. Non deve essere stato persistente del `ParcoMezzi`.
**Alternatives**:
- Salvare lo sconto nel `ParcoMezzi` (Configurazione globale). Scartato perché meno flessibile e introduce stato mutabile non necessario.
- Sconto per singolo cliente. Scartato perché la specifica parla di regole uniformi ("Le regole di sconto sono uniformi per tutti i clienti").

## 2. Identificazione Cliente

**Problem**: Come rappresentare il cliente?
**Decision**: `string ClienteId`.
**Rationale**: Specifica esplicita nelle Clarifications. Semplice e sufficiente per questa fase. Non serve una classe/record `Cliente` separata per ora.

## 3. Calcolo Totali

**Problem**: Come strutturare il risultato del batch per mostrare i totali per cliente?
**Decision**:
- **OOP**: Estendere `RisultatoBatch` (o creare DTO apposito) con una proprietà `TotaliPerCliente` (Dictionary<string, DettaglioCostiCliente>).
- **FP**: Estendere il record `RisultatoBatch` con una mappa/lista di `DettaglioCliente`.
**Rationale**: Necessario per soddisfare FR-007 ("Il RisultatoBatch DEVE riportare il costo totale per cliente").

## 4. Prezzo Non Negativo

**Problem**: Come garantire floor a 0?
**Decision**: `Math.Max(0, prezzoCalcolato)`.
**Rationale**: Semplice e robusto. Da applicare al momento del calcolo finale post-sconto.
