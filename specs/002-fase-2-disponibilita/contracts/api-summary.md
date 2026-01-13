# Sommario API: Fase 2

## Interfaccia OOP
```csharp
// Auto.cs
public class Auto
{
    public Guid Id { get; }
    public string Targa { get; }
    public StatoAuto Stato { get; }
    
    public Auto(Guid id, string targa);
    public void Noleggia();
    public void Restituisci();
}
```

## Interfaccia Funzionale

```csharp

// Auto.cs

public record Auto(Guid id, string targa, StatoAuto stato);



// AutoExtensions.cs

public static Result<Auto> Noleggia(this Auto auto);

public static Result<Auto> Restituisci(this Auto auto);

```
