// ABOUTME: Modello dati per le richieste di noleggio nel paradigma funzionale.
// Utilizziamo un record per incapsulare i parametri della richiesta.

namespace CarSharp.Functional;

public record RichiestaNoleggio(int PostiMinimi, Guid? IdAuto = null);
