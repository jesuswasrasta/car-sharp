module CarSharp.Fleet

type Car = unit
type Fleet = Fleet of Car list

let emptyFleet = Fleet []

let count (Fleet cars) = 0