// ABOUTME: Test di validazione per la factory di RichiestaNoleggio (FP).
// Verifichiamo che l'input venga validato producendo un Result.Failure invece di lanciare eccezioni.

using CarSharp.Functional;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace CarSharp.Functional.Tests;

public class RichiestaNoleggio_ValidazioneTests
{
    [Property]
    public bool Factory_DovrebbeRitornareFailure_QuandoCapacitaEInvalida(int capacita)
    {
        // Se la capacità è <= 0, il sistema deve rifiutare la creazione della richiesta.
        if (capacita > 0) return true;

        var risultato = RichiestaNoleggio.Crea("CLIENTE", capacita);

        return risultato.IsFailure;
    }
}
