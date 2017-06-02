using System;
using Ex03.GarageLogic;
namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private readonly Garage m_Garage = new Garage();

        public enum eInputValidation
        {
            MainMenu
        }

        public enum eMenuOptions
        {
            LowerBound = 0,
            InsertToGarage = 1,
            ShowRegistrationNums = 2,
            ChangeVechicleStus = 3,
            InflateWheels = 4,
            RefuelGas = 5,
            ChargeElectric = 6,
            ShowAllDetails = 7,
            UpperBound = 8
        }

        public void Run()
        {
            eMenuOptions userChoice = showMainMenu();
            handleMainInput(userChoice);
        }

        private void handleMainInput(eMenuOptions userChoice)
        {
            switch (userChoice)
            {
                case eMenuOptions.InsertToGarage:
                    getVehicleToInsert();
                    break;
                case eMenuOptions.ShowRegistrationNums:
                    break;
                case eMenuOptions.ChangeVechicleStus:
                    break;
                case eMenuOptions.InflateWheels:
                    break;
                case eMenuOptions.RefuelGas:
                    break;
                case eMenuOptions.ChargeElectric:
                    break;
                case eMenuOptions.ShowAllDetails:
                    break;
                default:
                    throw new FormatException(string.Format("Bad menu option selected"));
                    break;
            }
        }

        private void getVehicleToInsert()
        {
            string userMessage = @"Please insert the following in respective order: 
    Vehicle model, vehicle registartion number, vehicle's owner:";

        }

        private bool isValidMenuInput(string i_UserInput)
        {
            ushort input;
            return ushort.TryParse(i_UserInput, out input) && (input >= (ushort)eMenuOptions.LowerBound + 1
                   && input <= (ushort)eMenuOptions.UpperBound - 1);
        }

        private string getUserInput(eInputValidation i_InputKind, string i_UserMessage)
        {
            string userInput;
            bool isLegalInput = false;

            do
            {
                Console.WriteLine(i_UserMessage);
                userInput = Console.ReadLine();

                // check input by kind
                switch (i_InputKind)
                {
                    case eInputValidation.MainMenu:
                        isLegalInput = isValidMenuInput(userInput);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(i_InputKind), i_InputKind, null);
                }

                if (!isLegalInput)
                {
                    Console.WriteLine("Wrong input, please try again");
                }
            }
            while (!isLegalInput);

            return userInput;
        }

        private eMenuOptions showMainMenu()
        {
            string input;
            eMenuOptions userChoice;

            const string optionsMenu = 
@"Choose operation:
1) Insert a vehicle to garage.
2) See all registration numbers of vehicle in garage.
3) Change a vehicle's status.
4) Inflate car's wheels to maximum.
5) Refuel gas engine of vehicle.
6) Charge electric engine of vehicle.
7) Show All details of vehicle.";

            input = getUserInput(eInputValidation.MainMenu, optionsMenu);
            userChoice = (eMenuOptions)ushort.Parse(input);

            return userChoice;
        }
    }
}

