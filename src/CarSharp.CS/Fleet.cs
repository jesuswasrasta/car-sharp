using System.Collections.Generic;

namespace CarSharp.CS;

public class Fleet
{
    private readonly List<Car> _cars = new();

        public int Count => _cars.Count;

    

            public void Add(Car car)

    

            {

    

                _cars.Add(car);

    

            }

    

        

    

            public bool Remove(Car car)

    

            {

    

                return _cars.Remove(car);

    

            }

    

        }

    

        

    