using System;
using Ex03.GarageLogic;
namespace Ex03.ConsoleUI
{
    public class Program
    {
        Garage m_garage = new Garage();

        public static void Main()
        {
            Run();
        }

        public void Run()
        {
            
            
        }


        public void getUserOption()
        {
            string userInput;
            ushort numberOfOption;

            do
            {
                printOptionsMenu();
                userInput = Console.ReadLine();
                if (UInt16.TryParse(userInput, out numberOfOption))
                {

                }
                else
                {
                    // throw exception - the input was illeagal
                }
            }
            while (true);
        }

        public void printOptionsMenu()
        {
            string optionsMenu =
            @"Choose operation:
            1) Enter a vehicle to garage.
            2) See all registration numbers of vehicle in garage.
            3) Change vehicle's status.
            4) Inflate car's wheels to maximum.
            5) Refuel gas engine of vehicle.
            6) Charge electric engine of vehicle.
            7) Show All details of vehicle.";
            Console.WriteLine(optionsMenu);
        }


    }
}
