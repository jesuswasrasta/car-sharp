namespace CarSharp.CS;

public class Fleet
{
    // The internal state is managed via a mutable List.
    // This is the classic OOP approach where the object encapsulates and mutates its own data.
    private readonly List<Car> _cars = new();

    // The Count property exposes the current size of the fleet.
    public int Count => _cars.Count;

    // Adding a car modifies the existing list instance.
    // This is a side-effect: the method doesn't return a value but changes the state of the world.
    public void Add(Car car)
    {
        _cars.Add(car);
    }

    // Removing a car also modifies the state and returns a boolean status.
    // In idiomatic C#, bool or Exceptions are often used for flow control.
    public bool Remove(Car car)
    {
        return _cars.Remove(car);
    }
}


    

        

    