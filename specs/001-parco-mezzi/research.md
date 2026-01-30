# Ricerca Tecnica: Fase 1

## Decisioni sull'Uguaglianza
Per la Fase 1, abbiamo deciso di utilizzare l'uguaglianza nativa del linguaggio per distinguere le auto.

### OOP: Uguaglianza per Riferimento
In C#, le classi sono tipi di riferimento. Per impostazione predefinita, `Equals` e `==` confrontano gli indirizzi di memoria. Questo si adatta bene al concetto di "Entità" in cui un oggetto è unico.

### Funzionale: Uguaglianza per Valore
I record in C# 9+ forniscono l'uguaglianza basata sul valore. Due istanze di un record con le stesse proprietà sono considerate uguali. Questo si adatta bene al concetto di "Value Object".

## Scelta della Collezione per l'Immutabilità
Abbiamo scelto `System.Collections.Immutable.ImmutableList<T>` per il percorso funzionale perché:
1. Fornisce un'immutabilità vera (non solo in sola lettura).
2. Supporta lo structural sharing efficiente per le operazioni di aggiunta e rimozione.
3. È la libreria standard per le strutture dati immutabili in .NET.
