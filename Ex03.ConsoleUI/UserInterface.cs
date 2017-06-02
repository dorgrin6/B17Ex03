using System;
using Ex03.GarageLogic;
namespace Ex03.ConsoleUI
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    public class UserInterface
    {
        private readonly Garage m_Garage = new Garage();

        public enum eInputValidation
        {
            MainMenu,
            VehicleType,
            RegistrationScreen,
            ChangeVehicleStatusScreen
        }

        public enum eMenuOptions
        {
            LowerBound = 0,
            InsertToGarage = 1,
            ShowRegistrationNums = 2,
            ChangeVechicleStatus = 3,
            InflateWheels = 4,
            RefuelGas = 5,
            ChargeElectric = 6,
            ShowAllDetails = 7,
            UpperBound = 8
        }

        public enum eWantedParams
        {
            RegistartionNum,
            OwnersName,
            OwnersPhoneNum

        }

        public void Run()
        {
            // TODO: when to exit?
            do
            {
                eMenuOptions userChoice = showMainMenu();
                handleMainInput(userChoice);
            }
            while (true);
        }

        private void insertVehicle()
        {
            Console.WriteLine(
@"Please insert the following in respective order:
Registration number, owner's name, owner's phone number");
            string regisrationNum = Console.ReadLine();
            string ownerName = Console.ReadLine();
            string ownerPhoneNum = Console.ReadLine();

            StringBuilder typeMessage = new StringBuilder("Please choose the vehicle's type:");
            string[] vehicleNames = VehicleFactory.GetVehicleNames(); // get names from factory

            for (int i = 0; i < vehicleNames.Length; i++)
            {
                typeMessage.AppendFormat("{0}) {1} {2}", i + 1, vehicleNames[i], Environment.NewLine);
            }

            string input = getUserInput(eInputValidation.VehicleType, typeMessage.ToString());

            Vehicle toInsert = VehicleFactory.GetVehicle((VehicleFactory.eVehicleType)ushort.Parse(input));
            Type vehicleType = toInsert.GetType();

            foreach (MemberInfo memberInfo in toInsert.GetType().GetMembers())
            {
                Console.WriteLine("Please insert {0}", memberInfo.Name);
                string newInput = Console.ReadLine();
                
            }
            //m_Garage.InsertVehicle(regisrationNum, ownerName, ownerPhoneNum, vehicleType);
        }

        private void handleMainInput(eMenuOptions userChoice)
        {
            switch (userChoice)
            {
                case eMenuOptions.InsertToGarage:
                    this.insertVehicle();
                    break;
                case eMenuOptions.ShowRegistrationNums:
                    showRegistrationNums();
                    break;
                case eMenuOptions.ChangeVechicleStatus:
                    this.changeVehicleStatus();
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
                    throw new FormatException("Bad menu option selected");
            }
        }

        private void showRegistrationNums()
        {
            StringBuilder userMessage = 
                new StringBuilder("In which condition should the vehicles be?");

            string[] status = this.m_Garage.GetGarageVehicleStatus();

            for (int i = 0; i < status.Length; i++)
            {
                userMessage.AppendFormat("{0}) {1} {2}", Environment.NewLine, i + 1, status[i]);
            }

            userMessage.AppendFormat("{0}) All {1}", status.Length, Environment.NewLine);

            string input = getUserInput(eInputValidation.RegistrationScreen, userMessage.ToString());

            // filter by status unless "All" has been selected
            bool filterByStatus = ushort.Parse(input) != status.Length;

            List<string> registrationNums = 
                m_Garage.GetRegistrationNums(filterByStatus, (Garage.eVehicleStatus)ushort.Parse(input));

            foreach (string regNum in registrationNums)
            {
                Console.WriteLine(regNum);
            }
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

        private void changeVehicleStatus()
        {
            Garage.VehicleInGarage wantedVehicle = null;
            string message = @"Please enter the vehicle's registration number:";

            Console.WriteLine(message);
            string registrationNum = Console.ReadLine();

            if (!m_Garage.TryFindVehicleByRegistration(registrationNum, out wantedVehicle))
            {
                Console.WriteLine("Given vehicle doesn't exist");
                return;
            }

            Console.WriteLine("Please enter the wanted status:");
            
            string[] status = this.m_Garage.GetGarageVehicleStatus();

            for (int i = 0; i < status.Length; i++)
            {
                Console.WriteLine("{0}) {1} {2}", Environment.NewLine, i + 1, status[i]);
            }

            //TODO: continue here, should get input and change vehicle status
            //wantedVehicle.VehicleStatus = getUserInput(eInputValidation.ChangeVehicleStatusScreen, String.Empty);
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
                    case eInputValidation.RegistrationScreen:
                        isLegalInput = isValidRegistrationInput(userInput);
                        break;
                    case eInputValidation.VehicleType:
                        isLegalInput = isValidVehicleType(i_UserMessage);
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

        private bool isValidVehicleType(string i_UserInput)
        {
            ushort input;
            return ushort.TryParse(i_UserInput, out input)
                   && ((input + 1) >= (ushort)VehicleFactory.eVehicleType.LowerBound
                       && (input + 1) <= (ushort)VehicleFactory.eVehicleType.UpperBound);
        }

        private bool isValidMenuInput(string i_UserInput)
        {
            ushort input;
            return ushort.TryParse(i_UserInput, out input) && ((input + 1) >= (ushort)eMenuOptions.LowerBound
                   && (input + 1) <= (ushort)eMenuOptions.UpperBound);
        }

        private bool isValidRegistrationInput(string i_UserInput)
        {
            ushort input;
            return ushort.TryParse(i_UserInput, out input) && ((input + 1) >= (ushort)VehicleFactory.eVehicleType.LowerBound
                   && (input + 1) <= (ushort)VehicleFactory.eVehicleType.UpperBound);
        }
    }
}

