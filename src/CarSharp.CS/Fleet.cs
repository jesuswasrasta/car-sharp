using System.Collections.Generic;

namespace CarSharp.CS;

public class Fleet
{
    private readonly List<Car> _cars = new();

    public int Count => _cars.Count;
}