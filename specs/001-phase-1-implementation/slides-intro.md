# Phase 1: Minimal Fleet Management
## The Journey Begins

Welcome to the first phase of **CarSharp**. In this phase, we lay the foundation for our comparative study of programming paradigms.

### The Objective
Implement a fleet management system that can:
1.  Initialize an **empty fleet**.
2.  **Add** a car to the fleet.
3.  **Remove** a specific car from the fleet.
4.  Track the **total count** of cars.

### The Paradigms
-   **OOP (Object-Oriented Programming)**: We'll focus on **encapsulation** and **mutable state**. The `Fleet` will hold a list that it manages internally.
-   **Functional Programming**: We'll focus on **immutability** and **pure functions**. Every operation on the `Fleet` will return a *new* fleet, leaving the original unchanged.

### Key Concept: Identity
In this phase, cars have no properties (like Plate or Model).
-   In **OOP**, we distinguish cars by their **memory reference**.
-   In **Functional**, we treat them as **values**. (Though in this phase, they are empty records, we'll treat them as distinct value tokens).

Let's see how these two mental models diverge in code.