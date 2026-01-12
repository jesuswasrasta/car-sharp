module CarSharp.Fleet

type Car = unit
type Fleet = Fleet of Car list
type Error = string

let emptyFleet = Fleet []

let count (Fleet cars) = cars.Length

let add (car: Car) (Fleet cars) =
    Fleet (car :: cars)

let remove (car: Car) (Fleet cars) =
    match List.tryFindIndex ((=) car) cars with
    | Some index -> Ok (Fleet (List.removeAt index cars))
    | None -> Result.Error "Car not found"
