# Phase 1: Minimal Fleet Management

## Context
In this first phase, we establish the foundation of our Car Rental system. We start with a minimal set of requirements to manage a fleet of cars.

### Requirements
1.  **Zero State**: A new fleet is empty.
2.  **Add**: We can add a car to the fleet.
3.  **Count**: We can know how many cars are in the fleet.
4.  **Remove**: We can remove a car from the fleet.

## Learning Objectives

### 1. Mutable vs Immutable State
*   **C# Approach**: The `Fleet` class *encapsulates* state. Methods like `Add` modify the internal list. The object's identity remains the same, but its content changes.
*   **F# Approach**: The `Fleet` type *is* data. Functions like `add` take an existing fleet and return a *new* fleet with the car added. The original fleet remains unchanged.

### 2. Testing Strategies
*   **Example-Based Testing (xUnit)**: We verify specific scenarios.
    *   *Example*: "Given an empty fleet, when I add a car, count should be 1."
*   **Property-Based Testing (FsCheck)**: We verify universal truths (invariants).
    *   *Example*: "For ANY fleet and ANY car, adding the car increases the count by 1."

## The Journey
We will implement this phase twice:
1.  **Session 1**: As Enterprise C# Developers (Classes, Void methods, Exceptions/Bools).
2.  **Session 2**: As Functional F# Programmers (Types, Pure functions, Results).
