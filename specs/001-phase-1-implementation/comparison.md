# Comparison: Phase 1 - Minimal Fleet Management

This phase highlights the foundational differences between Object-Oriented and Functional paradigms in C#.

## 1. Identity and Equality
- **OOP (`Car` class)**: Uses **Reference Equality**. Each `new Car()` is a unique entity in memory. Removing a car requires passing the exact same object reference that was added.
- **Functional (`Car` record)**: Uses **Value Equality**. Since our `Car` is empty, all `new Car()` instances are technically equal. However, records provide a stable way to handle data as values rather than identities.

## 2. State Management
- **OOP (`Fleet.AddCar`)**: Uses **In-place Mutation**. The `Fleet` object's internal list is modified. The object identity of the `Fleet` remains the same, but its content changes. This is efficient but can lead to side effects if the fleet is shared.
- **Functional (`Fleet.AddCar`)**: Uses **Immutability**. The original `Fleet` is never changed. Instead, a `with` expression creates a new copy with the updated list. This is safer for concurrency and shared state but requires more allocations (mitigated by C# records and Immutable Collections).

## 3. Structural Sharing
- **Functional (`Fleet.RemoveCar`)**: We implemented a check: if `Remove` doesn't find the car, it returns the *same* instance of the list. We leveraged this to return the *same* `Fleet` instance. This "Structural Sharing" is a key optimization in functional programming, minimizing unnecessary copies.

## 4. API Design
- **OOP**: Methods are members of the class (`fleet.AddCar(car)`) and return `void` (relying on side effects) or `bool` (indicating success).
- **Functional**: We used **Extension Methods** (`fleet.AddCar(car)`) to separate data (`record Fleet`) from logic. Every operation returns the new state, enabling **fluent chaining**.

## 5. Testing
- **OOP**: **Example-Based (Facts)**. We verify specific "Given-When-Then" scenarios.
- **Functional**: **Property-Based (FsCheck)**. We verify that "Adding $N$ cars ALWAYS results in $N$ total cars," regardless of what $N$ is. This uncovers edge cases more effectively.
