# Quickstart: Clienti e Sconti

Come utilizzare le nuove funzionalità di associazione clienti e sconti batch.

## Prerequisiti

Avere un'istanza di `ParcoMezzi` popolata con auto disponibili.

## Esempio OOP

```csharp
// 1. Setup
var parco = new ParcoMezzi();
parco.AggiungiAuto(new Auto("AB123CD", Auto.Segmento.CityCar));
parco.AggiungiAuto(new Auto("XY987ZW", Auto.Segmento.Suv));

// 2. Creazione Richieste con ClienteId
var richieste = new List<RichiestaNoleggio>
{
    new RichiestaNoleggio("AB123CD", 2, "Cliente1"),
    new RichiestaNoleggio("XY987ZW", 3, "Cliente1") // Stesso cliente
};

// 3. Processamento Batch con sconto 10% (0.10m)
var risultatoBatch = parco.PrenotaBatch(richieste, 0.10m);

// 4. Verifica Risultati
var dettaglioCliente1 = risultatoBatch.RiepilogoClienti["Cliente1"];
Console.WriteLine($"Totale Netto Cliente1: {dettaglioCliente1.TotaleNetto}");
// Dovrebbe essere scontato perché ha 2 prenotazioni
```

## Esempio Funzionale

```csharp
// 1. Setup
var parco = ParcoMezzi.Vuoto
    .AggiungiAuto(new Auto("AB123CD", Segmento.CityCar))
    .Value
    .AggiungiAuto(new Auto("XY987ZW", Segmento.Suv))
    .Value;

// 2. Creazione Richieste
var richieste = new List<RichiestaNoleggio>
{
    new RichiestaNoleggio("AB123CD", 2, "Cliente1"),
    new RichiestaNoleggio("XY987ZW", 3, "Cliente1")
};

// 3. Processamento Batch
var risultato = ParcoMezzi.PrenotaBatch(parco, richieste, 0.10m);

// 4. Gestione Risultato
risultato.Match(
    success: batch => {
        var cliente1 = batch.RiepilogoClienti.First(c => c.ClienteId == "Cliente1");
        Console.WriteLine($"Totale Netto: {cliente1.TotaleNetto}");
    },
    failure: error => Console.WriteLine($"Errore: {error}")
);
```
