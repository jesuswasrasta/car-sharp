// ABOUTME: This file defines the Car record for the Functional track.
// We use a 'record' to leverage built-in value equality, a core 
// concept in functional programming where data is treated as immutable values.

namespace CarSharp.Functional;

/// <summary>
/// Represents a car in the fleet.
/// In this Functional implementation, the Car is a 'Value Object'.
/// Even if empty, records provide built-in value-based equality.
/// 
/// Contrast with OOP:
/// In OOP, two 'new Car()' instances are different because they are different 
/// objects in memory. In Functional C#, two 'new Car()' records are 
/// considered equal because they contain the same data (which is currently none).
/// </summary>
public record Car();
