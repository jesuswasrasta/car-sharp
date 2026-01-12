module Tests

open System
open CarSharp.Fleet
open Xunit

[<Fact>]
let ``Empty fleet has zero count`` () =
    Assert.Equal(0, count emptyFleet)