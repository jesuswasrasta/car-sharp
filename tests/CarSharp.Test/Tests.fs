module Tests

open System
open CarSharp.Fleet
open Xunit
open FsCheck.Xunit

[<Fact>]
let ``Empty fleet has zero count`` () =
    Assert.Equal(0, count emptyFleet)

[<Property>]
let ``Adding a car increments count`` (car: Car) (fleet: Fleet) =
    let newFleet = add car fleet
    count newFleet = count fleet + 1