// ABOUTME: Definizione dei tipi per la Fase 2 (Identità e Stato).
// In FP, lo stato non è una proprietà mutabile, ma è codificato nel TIPO stesso.
// Un'auto disponibile è un tipo diverso da un'auto noleggiata.

using System;

namespace CarSharp.Functional;

/// <summary>
/// Interfaccia marcatore per tutti gli stati dell'auto.
/// </summary>
public interface IAuto
{
    Guid Id { get; }
    string Targa { get; }
    int Capacita { get; }
}

/// <summary>
/// Rappresenta un'auto pronta per essere noleggiata.
/// </summary>
public record AutoDisponibile(Guid Id, string Targa, int Capacita) : IAuto;

/// <summary>
/// Rappresenta un'auto attualmente in uso da un cliente.
/// </summary>
public record AutoNoleggiata(Guid Id, string Targa, int Capacita) : IAuto;

// Manteniamo il record base Auto per compatibilità con i test Fase 1 se necessario,
// o lo rimuoviamo se migriamo tutto. Per ora lo lasciamo ma Fase 2 usa IAuto.
// public record Auto(); // Deprecato in Fase 2
