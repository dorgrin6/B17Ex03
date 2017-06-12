using System;
using Ex03.GarageLogic;
namespace Ex03.ConsoleUI
{
    public class Program
    {

        public static void Main()
        {
            UserInterface userInterface = new UserInterface();
            userInterface.Run();
            Console.ReadKey();
        }
    }
}
