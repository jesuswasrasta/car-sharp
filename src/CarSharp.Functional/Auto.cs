// ABOUTME: Evoluzione del dominio Auto verso il Type-Driven Design.
// Invece di una proprietà 'Stato', utilizziamo tipi distinti per rappresentare 
// stati mutuamente esclusivi, rendendo il codice più sicuro e auto-documentante.

namespace CarSharp.Functional;

/// <summary>
/// Interfaccia base per tutti i tipi di auto.
/// Permette di mantenere l'identità tecnica e di dominio indipendentemente dallo stato.
/// </summary>
public interface IAuto
{
    Guid Id { get; init; }
    string Targa { get; init; }
}

/// <summary>
/// Rappresenta un'auto pronta per il noleggio.
/// </summary>
public record AutoDisponibile(Guid Id, string Targa) : IAuto;

/// <summary>
/// Rappresenta un'auto attualmente impegnata in un noleggio.
/// </summary>
public record AutoNoleggiata(Guid Id, string Targa) : IAuto;

// Nota per l'audience: In questa fase non usiamo un record base per evitare
// la 'falsa eredità'. Ogni stato ha il suo tipo, e il parco mezzi conterrà 
// una collezione di IAuto (o una Union Type simulata).
