// ABOUTME: Questo file definisce il record Auto per il percorso Funzionale.
// Utilizziamo un 'record' per sfruttare l'uguaglianza basata sul valore, un concetto
// fondamentale nella programmazione funzionale dove i dati sono trattati come valori immutabili.

namespace CarSharp.Functional;

/// <summary>
/// Rappresenta un'auto nel parco mezzi.
/// In questa implementazione Funzionale, l'Auto è un 'Value Object'.
/// Anche se vuoti, i record forniscono un'uguaglianza integrata basata sul valore.
/// 
/// Contrasto con OOP:
/// In OOP, due istanze 'new Auto()' sono diverse perché sono oggetti diversi 
/// in memoria. In C# Funzionale, due record 'new Auto()' sono considerati 
/// uguali perché contengono gli stessi dati (che al momento sono nulli).
/// </summary>
public record Auto();