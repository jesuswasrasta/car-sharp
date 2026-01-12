module CarSharp.Fleet

// In F#, we define types to represent our data. 
// Car is a simple unit type for now (Phase 1).
type Car = unit

// Fleet is a Discriminated Union wrapping a list of cars.
// In the functional paradigm, data is immutable by default.
type Fleet = Fleet of Car list

// Error type for our Result pattern.
type Error = string

// We define a starting value (empty fleet) as a simple value, not a class instance.
let emptyFleet = Fleet []

// A pure function to count cars. It deconstructs the Fleet type to access the list.
let count (Fleet cars) = cars.Length

// 'add' takes a car and a fleet, and returns a NEW fleet with the car prepended.
// This is a pure transformation: the original fleet remains unchanged.
let add (car: Car) (Fleet cars) =
    Fleet (car :: cars)

// 'remove' returns a Result type, forcing the caller to handle failures explicitly.
// This is the beginning of "Railway Oriented Programming" (ROP).
let remove (car: Car) (Fleet cars) =
    match List.tryFindIndex ((=) car) cars with
    | Some index -> Ok (Fleet (List.removeAt index cars))
    | None -> Result.Error "Car not found"
