// ABOUTME: Definizione dello stato dell'auto tramite Enum.
// In OOP, lo stato è spesso rappresentato come un valore di una proprietà dell'oggetto.

namespace CarSharp.Oop;

/// <summary>
/// Rappresenta i possibili stati operativi di un'auto nel parco mezzi.
/// </summary>
public enum StatoAuto
{
    /// <summary>
    /// L'auto è pronta per essere noleggiata.
    /// </summary>
    Disponibile,

    /// <summary>
    /// L'auto è attualmente in uso da un cliente.
    /// </summary>
    Noleggiata
}
