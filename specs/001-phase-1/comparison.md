# Comparison: Phase 1 - Minimal Fleet Management

This phase highlights the fundamental shift from Object-Oriented Programming (OOP) to Functional Programming (FP) using C# and F#.

## 1. State Management: Mutable vs Immutable

### C# (OOP)
In C#, the `Fleet` class encapsulates its state using a private `List<Car>`.
Operations like `Add` and `Remove` modify this internal list.

```csharp
public void Add(Car car)
{
    _cars.Add(car); // Side-effect: modifies internal state
}
```

*   **Pros**: Familiar to most developers, efficient for large collections (in-place modification).
*   **Cons**: Harder to reason about in multi-threaded environments, state can change unexpectedly.

### F# (FP)
In F#, `Fleet` is an immutable type (a wrapper around a list).
Functions like `add` and `remove` take a fleet and return a *new* fleet.

```fsharp
let add (car: Car) (Fleet cars) =
    Fleet (car :: cars) // Pure function: returns a new value
```

*   **Pros**: Thread-safe by default, predictable behavior (pure functions), easier to test and reason about.
*   **Cons**: Can be less efficient if not using persistent data structures (though F# lists are efficient for prepending).

## 2. Testing: Examples vs Properties

### C# (Example-Based)
Using xUnit `[Fact]`, we test specific examples.

```csharp
[Fact]
public void Add_Should_IncrementCount()
{
    var fleet = new Fleet();
    fleet.Add(new Car());
    Assert.Equal(1, fleet.Count);
}
```

We manually define the "Given/When/Then" for specific inputs.

### F# (Property-Based)
Using FsCheck `[Property]`, we verify universal invariants.

```fsharp
[Property]
let ``Adding a car increments count`` (car: Car) (fleet: Fleet) =
    let newFleet = add car fleet
    count newFleet = count fleet + 1
```

FsCheck generates hundreds of random `Car` and `Fleet` instances to find edge cases we might have missed.

## 3. Error Handling: Bools vs Results

### C# (Classic)
`Remove` returns a `bool` to indicate success or failure.

```csharp
public bool Remove(Car car)
{
    return _cars.Remove(car);
}
```

### F# (Railway Oriented)
`remove` returns a `Result<Fleet, Error>`, forcing the caller to handle both cases explicitly.

```fsharp
let remove (car: Car) (Fleet cars) =
    match List.tryFindIndex ((=) car) cars with
    | Some index -> Ok (Fleet (List.removeAt index cars))
    | None -> Result.Error "Car not found"
```

This is the beginning of **Railway Oriented Programming**, where we chain operations that can fail.
