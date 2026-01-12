module CarSharp.Fleet

type Car = unit
type Fleet = Fleet of Car list

let emptyFleet = Fleet []



let count (Fleet cars) = cars.Length



let add (car: Car) (Fleet cars) =

    Fleet (car :: cars)
