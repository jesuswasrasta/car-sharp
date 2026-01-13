# Quickstart: Fase 2

## Esempio OOP
```csharp
var auto = new Auto(Guid.NewGuid(), "AA123BB");
auto.Noleggia(); // Successo
// auto.Noleggia(); // Lancia InvalidOperationException
```

## Esempio Funzionale
```csharp
var auto = new Auto(Guid.NewGuid(), "XY987ZZ", StatoAuto.Disponibile);
var risultato = auto.Noleggia()
    .Bind(a => a.Restituisci()); // Esempio di chaining ROP
```