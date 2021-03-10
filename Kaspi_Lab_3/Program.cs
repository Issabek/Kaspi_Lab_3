using System;

namespace Kaspi_Lab_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int floors=0, capacity=0, currentElevFloor=0;
            char yesNo = 'y';
            Console.WriteLine("Enter number of floors in a building: ");
            floors = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter elevator capacity in number of people it can carry: ");
            capacity = Int32.Parse(Console.ReadLine());
            Elevator myElevator = new Elevator(floors, capacity);

            Console.WriteLine("On which floor you are?");
            currentElevFloor = Int32.Parse(Console.ReadLine());
            myElevator.callFromOutside(currentElevFloor);
        }

    }
}
