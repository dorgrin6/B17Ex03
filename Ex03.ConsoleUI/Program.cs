﻿/* Garage Manager Program
 * 
 * 
 * First Student Details.: 201389681- Dor Grinshpan
 * Second Student Details: 307948836- Idan Goor
 * Delivery Date.........: 14 - June - 2017
 * */

namespace Ex03.ConsoleUI
{
    using System;

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