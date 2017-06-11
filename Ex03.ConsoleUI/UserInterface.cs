﻿using System;
using Ex03.GarageLogic;
namespace Ex03.ConsoleUI
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Policy;
    using System.Text;

    public class UserInterface
    {
        private readonly Garage m_Garage = new Garage();

        public enum eInputValidation
        {
            MainMenu,
            VehicleType,
            RegistrationScreen,
            ChangeVehicleStatusScreen,
            Blank
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

        private void insertVehicleToGarage()
        {
            Console.WriteLine(
@"Please insert the following in respective order:
Registration number, owner's name, owner's phone number.");
            string regisrationNum = Console.ReadLine();
            string ownerName = Console.ReadLine();
            string ownerPhoneNum = Console.ReadLine();
            
            string message = "Please choose the vehicle's type:";
            Console.WriteLine(message);
            VehicleFactory.addNamesToDictionary();
            Console.WriteLine(createEnumaration(Enum.GetNames(typeof(VehicleFactory.eVehicleType))));
            string input = getUserInput(eInputValidation.VehicleType, message);



            Vehicle toInsert = VehicleFactory.GetVehicle((VehicleFactory.eVehicleType)ushort.Parse(input));
            setAdditionalProperties(toInsert);


            // TODO: ask for properties and insert them
            //m_Garage.InsertVehicle(regisrationNum, ownerName, ownerPhoneNum, );
        }

/*        private void setBasicProperties(Vehicle i_Vehicle)
        {
            string wheelManufacturer;
            float currentAirPressure;

            Console.WriteLine("Please enter the model name:");
            i_Vehicle.ModelName = Console.ReadLine();
            Console.WriteLine("Please enter the wheel's manufacturer:");
            wheelManufacturer = Console.ReadLine();
            Console.WriteLine("Please enter the wheel's current air pressure:");
             
           

        }*/


        private void setAdditionalProperties(object i_Object)
        {
            Type propertyType = null;

            foreach (PropertyInfo prop in i_Object.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                propertyType = prop.GetValue(i_Object).GetType();
                if (prop.CanWrite)
                {
                    Console.WriteLine("Please enter the {0}:", NamesDictionary.GetValue(prop.Name));
                    if (propertyType == typeof(string))
                    {
                        //TODO: check if all chars are letters?
                    }
                    else if (propertyType == typeof(bool))
                    {

                    }
                    else if (propertyType == typeof(int) || propertyType == typeof(float) || propertyType == typeof(double))
                    {

                    }
                    else if (propertyType.IsEnum)
                    {
                        Console.WriteLine(createEnumaration(propertyType.GetEnumNames()));
                        //we need to check is input isDefined in enum range of values
                    }
                }
                else if (propertyType.IsClass)
                {
                    setAdditionalProperties(prop.GetValue(i_Object));
                }
            }

            /*
            PropertyInfo[] info = i_Vehicle.GetType().GetProperties();




            foreach (PropertyInfo prop in info)
            {
                //Console.WriteLine((prop.GetValue(i_Vehicle) as PropertyHolder).Name);
                
                if ((prop.GetValue(i_Vehicle) as PropertyHolder).ValueType == typeof(Enum))
                {
                    Type zzz = (prop.GetValue(i_Vehicle)).GetType();
                    Console.WriteLine(createEnumaration(zzz.GetEnumNames()));
                }

                //if ((prop.GetValue(i_Vehicle) as PropertyHolder).ValueType.IsEnum)
                //{

                //Type zachi = (prop.GetValue(i_Vehicle) as PropertyHolder).ValueType;

                //Type zachi = (prop.GetValue(i_Vehicle) as PropertyHolder<Enum>).ValueType;

                // Console.WriteLine(createEnumaration(zachi.GetEnumNames()));
                //                    
                //
                //
                //                    foreach (var enums in zachi.GetEnumValues())
                //                    {
                //                        Console.WriteLine(enums);
                //                    }
                //                   


            


//                Type typ = prop.GetValue(i_Vehicle)
//                    
//                    .GetType();
//                {
//                    
//                }
//
//                Type propertyType = prop.GetValue(i_Vehicle).GetType();

                //bool check = Enum.IsDefined(propertyType, 5);

                //object deff = prop.PropertyType;
            }

            /*
            PropertyInfo[] info = i_Vehicle.GetType().GetProperties();

            foreach (PropertyInfo prop in info)
            {
                bool validInput;
                do
                {
                    Console.WriteLine(string.Format("Please insert {0}:", prop.Name));
                    string input = Console.ReadLine();
                    int inputNum;
                    
                    validInput = true;
                    try
                    {
                        if (int.TryParse(input, out inputNum))
                        {
                            prop.SetValue(i_Vehicle, inputNum, null);
                        }
                        else
                        {
                            validInput = false;
                        }
                    }
                    catch (ValueOutOfRangeException i_Except)
                    {
                        validInput = false;
                        Console.WriteLine(
                            "Wrong input. Please insert values in range({0}-{1})",
                            i_Except.minValue,
                            i_Except.maxValue);
                    }
                    catch (FormatException i_Except)
                    {
                        validInput = false;
                        Console.WriteLine("Wrong input format");
                    }
                }
                while (!validInput);
            }
            */
        }


        private string createEnumaration(string[] i_Enumarte)
        {
            StringBuilder builder = new StringBuilder();

            int index = 1;
            foreach (string str in i_Enumarte)
            {
                builder.AppendFormat("{0}) {1} ", index, NamesDictionary.GetValue(str));
                if (index != i_Enumarte.Length)
                {
                    builder.AppendLine();
                }

                ++index;
            }

            return builder.ToString();
        }

        private void handleMainInput(eMenuOptions userChoice)
        {
            switch (userChoice)
            {
                case eMenuOptions.InsertToGarage:
                    insertVehicleToGarage();
                    break;
                case eMenuOptions.ShowRegistrationNums:
                    showRegistrationNums();
                    break;
                case eMenuOptions.ChangeVechicleStatus:
                    changeVehicleStatus();
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

            string[] status = m_Garage.GetGarageVehicleStatus();

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
            
            string[] status = m_Garage.GetGarageVehicleStatus();

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
            ushort input;

            do
            {
                Console.WriteLine(i_UserMessage);
                userInput = Console.ReadLine();

                // check input by kind
                switch (i_InputKind)
                {
                    case eInputValidation.Blank:
                        isLegalInput = ushort.TryParse(userInput, out input);
                        break;
                    case eInputValidation.MainMenu:
                        isLegalInput = isValidMenuInput(userInput);
                        break;
                    case eInputValidation.RegistrationScreen:
                        //isLegalInput = isValidRegistrationInput(userInput);
                        isLegalInput = true;
                        break;
                    case eInputValidation.VehicleType:
                        //isLegalInput = isValidVehicleType(userInput);
                        isLegalInput = true;
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

       
        /*
        private bool isValidVehicleType(string i_UserInput)
        {
            ushort input;
            return ushort.TryParse(i_UserInput, out input)
                   && ((input + 1) >= (ushort)VehicleFactory.eVehicleType.LowerBound
                       && (input + 1) <= (ushort)VehicleFactory.eVehicleType.UpperBound);
        }
        */
        private bool isValidMenuInput(string i_UserInput)
        {
            ushort input;
            return ushort.TryParse(i_UserInput, out input) && ((input + 1) >= (ushort)eMenuOptions.LowerBound
                   && (input + 1) <= (ushort)eMenuOptions.UpperBound);
        }

        /*
        private bool isValidRegistrationInput(string i_UserInput)
        {
            ushort input;
            return ushort.TryParse(i_UserInput, out input) && ((input + 1) >= (ushort)VehicleFactory.eVehicleType.LowerBound
                   && (input + 1) <= (ushort)VehicleFactory.eVehicleType.UpperBound);
        }
        */
    }
}

