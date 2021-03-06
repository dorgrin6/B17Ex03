﻿namespace Ex03.ConsoleUI
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Ex03.GarageLogic;

    public class UserInterface
    {
        private const string k_AddFail = "Vehicle wasn't added to garage.";

        private const string k_BoundaryLine = "-----------------------------";

        private const string k_Enter = "Please enter";

        private const string k_EnterElectricityAmount = "Please enter the amount of electricity to charge (in minutes):";

        private const string k_EnterGasAmount = "Please enter the amount of gas to fuel:";

        private const float k_MinimumValueForInput = 0;

        private const string k_SearchFail = "No vehicles were found in the garage which fits the search.";

        private const string k_TryAgain = "Please try again.";

        private const string k_VehicleExist =
            "The vehicle is already exists in the garage. It's status changed to InRepair.";

        private const string k_VehicleWasntFound = "Vehicle wasn't found.";

        private const string k_WrongInput = "Wrong input.";

        private readonly Garage m_Garage = new Garage();

        public enum eEnumBounds
        {
            Max,

            Min
        }

        public enum eMenuOptions
        {
            InsertToGarage = 1,

            ShowRegistrationNums,

            ChangeVechicleStatus,

            InflateWheels,

            RefuelGas,

            ChargeElectric,

            ShowAllDetails,

            Exit
        }

        public enum eStringFilter
        {
            AllLetters,

            AllDigits
        }

        // Run: runs the program.
        public void Run()
        {
            string input; // holds user's input
            eMenuOptions userChoice; // holds the input choice of menu option
            bool isRunning = true; // holds True as long as program running, False if user asks to exit.
            Type type = typeof(eMenuOptions);

            do
            {
                showMainMenu();
                input = getUserInput<Enum>(
                    getEnumBound(type, eEnumBounds.Max),
                    getEnumBound(type, eEnumBounds.Min),
                    true);
                userChoice = (eMenuOptions)Enum.Parse(typeof(eMenuOptions), input);
                isRunning = handleMainInput(userChoice);
            }
            while (isRunning);
        }

        // changeVehicleStatus: changes vehicle 
        private void changeVehicleStatus()
        {
            string registrationNumber;
            string input;
            Garage.eVehicleStatus status;

            Console.WriteLine(k_Enter + " " + Vehicle.k_RegistrationNum + ":");
            registrationNumber = getUserInput<string>();

            if (m_Garage.IsVehicleExistsInGarage(registrationNumber))
            {
                Console.WriteLine(k_Enter + " " + Garage.VehicleInGarage.k_VehicleStatus + ":");
                input = getEnumAnswerHelper<Garage.eVehicleStatus>();
                status = (Garage.eVehicleStatus)ushort.Parse(input);
                m_Garage.GetVehicle(registrationNumber).VehicleStatus = status;
                printResult($"Vehicle {registrationNumber} status was changed to: {status.ToString()}");
            }
            else
            {
                printResult(k_VehicleWasntFound);
            }
        }

        private void chargeVehicleEnergy(Engine.eEngineType i_EngineType)
        {
            string registrationNumber;
            float addEnergy;
            GasEngine.eFuelType fuelType;
            bool isVehicleExists = true;
            string message = string.Empty;
            try
            {
                isVehicleExists = getBasicDataForCharging(i_EngineType, out registrationNumber, out addEnergy);
                if (isVehicleExists)
                {
                    if (i_EngineType == Engine.eEngineType.Electric)
                    {
                        m_Garage.ChargeEnergy(registrationNumber, i_EngineType.ToString(), addEnergy.ToString());
                        message = "charged";
                    }
                    else if (i_EngineType == Engine.eEngineType.Gas)
                    {
                        fuelType = getFuelForCharging();
                        m_Garage.ChargeEnergy(
                            registrationNumber,
                            i_EngineType.ToString(),
                            addEnergy.ToString(),
                            fuelType.ToString());
                        message = "fueled";
                    }

                    printResult($"Vehicle {registrationNumber} was successfully {message}.");
                }
            }
            catch (ArgumentException exception)
            {
                printResult(exception.Message);
            }
            catch (ValueOutOfRangeException exception)
            {
                printResult(exception.Message);
            }
        }

        // createEnumaration: prints Enum values to console.
        private string createEnumaration(string[] i_Enumarte)
        {
            StringBuilder builder = new StringBuilder();

            int index = 1;
            foreach (string str in i_Enumarte)
            {
                builder.AppendFormat("{0}) {1} ", index, str);
                if (index != i_Enumarte.Length)
                {
                    builder.AppendLine();
                }
                ++index;
            }

            return builder.ToString();
        }

        // getAdditionalOwnerProperties: gets additional owner's properties.
        private void getAdditionalOwnerProperties(Owner i_Owner)
        {
            i_Owner.Name = getOwnerProperty(k_Enter + " " + Owner.k_Name, eStringFilter.AllLetters);
            i_Owner.PhoneNumber = getOwnerProperty(k_Enter + " " + Owner.k_PhoneNumber, eStringFilter.AllDigits);
        }

        // getAdditionalVehicleProperties: gets additional vehicle's properties.
        private void getAdditionalVehicleProperties(string i_RegistrationNumber, Vehicle i_Vehicle)
        {
            Dictionary<string, PropertyHolder> propertiesInfo = new Dictionary<string, PropertyHolder>();
            Dictionary<string, string> propertiesDone = new Dictionary<string, string>();

            i_Vehicle.RegistrationNumber = i_RegistrationNumber;
            i_Vehicle.AddProperties(propertiesInfo);
            getVehiclePropertiesFromUser(propertiesInfo, propertiesDone);
            i_Vehicle.SetProperties(propertiesDone);
        }

        private bool getBasicDataForCharging(
            Engine.eEngineType i_EngineType,
            out string o_RegistrationNumber,
            out float o_AddEnergy)
        {
            string input;
            o_AddEnergy = default(float);
            bool isVehicleExists = true;

            Console.WriteLine(k_Enter + " " + Vehicle.k_RegistrationNum + ":");
            o_RegistrationNumber = getUserInput<string>();
            if (m_Garage.IsVehicleExistsInGarage(o_RegistrationNumber))
            {
                if (i_EngineType == Engine.eEngineType.Gas)
                {
                    Console.WriteLine(k_EnterGasAmount);
                }
                else if (i_EngineType == Engine.eEngineType.Electric)
                {
                    Console.WriteLine(k_EnterElectricityAmount);
                }

                input = getUserInput<float>();
                o_AddEnergy = float.Parse(input);
            }
            else
            {
                isVehicleExists = false;
                printResult(k_VehicleWasntFound);
            }

            return isVehicleExists;
        }

        // getBoolProperty: handles with Bool type property input.
        private void getBoolProperty(
            string i_PropertyName,
            Dictionary<string, PropertyHolder> i_PropertiesInfo,
            Dictionary<string, string> i_PropertiesDone)
        {
            Console.WriteLine(k_Enter + " True/False.");
            i_PropertiesDone.Add(i_PropertyName, getUserInput<bool>());
        }

        // getEnumAnswerHelper: handles with Enum type property input, when Enum is known during compilation.
        private string getEnumAnswerHelper<T>()
        {
            Type type = typeof(T);
            string input;

            Console.WriteLine(createEnumaration(Enum.GetNames(type)));
            input = getUserInput<Enum>(getEnumBound(type, eEnumBounds.Max), getEnumBound(type, eEnumBounds.Min), true);

            return input;
        }

        // getEnumBound: gets Enum bounds value. return max value or minimum.
        private int getEnumBound(Type i_Enum, eEnumBounds i_BoundType)
        {
            int result = 0;
            Array enumValues = Enum.GetValues(i_Enum);

            switch (i_BoundType)
            {
                case eEnumBounds.Max:
                    result = (int)enumValues.GetValue(enumValues.Length - 1);
                    break;
                case eEnumBounds.Min:
                    result = (int)enumValues.GetValue(0);
                    break;
            }

            return result;
        }

        // getEnumProperty: handles with Enum type property input.
        private void getEnumProperty(
            string i_PropertyName,
            Dictionary<string, PropertyHolder> i_PropertiesInfo,
            Dictionary<string, string> i_PropertiesDone)
        {
            Type type;
            string input;

            type = i_PropertiesInfo[i_PropertyName].ValueType;
            Console.WriteLine(createEnumaration(i_PropertiesInfo[i_PropertyName].OptionalEnumValues.ToArray()));
            input = getUserInput<Enum>(getEnumBound(type, eEnumBounds.Max), getEnumBound(type, eEnumBounds.Min), true);
            i_PropertiesDone.Add(i_PropertyName, input);
        }

        // getFloatProperty: handles with Float type property input.
        private void getFloatProperty(
            string i_PropertyName,
            Dictionary<string, PropertyHolder> i_PropertiesInfo,
            Dictionary<string, string> i_PropertiesDone)
        {
            if (i_PropertiesInfo[i_PropertyName].isFloatRanged)
            {
                float maxFloatValue = i_PropertiesInfo[i_PropertyName].MaxFloatValue;
                float minFloatValue = i_PropertiesInfo[i_PropertyName].MinFloatValue;
                string userInput = getUserInput<float>(
                        maxFloatValue,
                        minFloatValue,
                        true);
                i_PropertiesDone.Add(i_PropertyName, userInput);
            }
            else
            {
                i_PropertiesDone.Add(i_PropertyName, getUserInput<float>());
            }
        }

        private GasEngine.eFuelType getFuelForCharging()
        {
            string input;
            GasEngine.eFuelType fuel;

            Console.WriteLine(k_Enter + " " + GasEngine.k_FuelType + ":");
            input = getEnumAnswerHelper<GasEngine.eFuelType>();
            fuel = (GasEngine.eFuelType)ushort.Parse(input);

            return fuel;
        }

        // getNewVehicleProperties: gets new vehicle's and owner's properties.
        private void getNewVehicleProperties(string i_RegistrationNumber, out Vehicle o_Vehicle, out Owner o_Owner)
        {
            string inputVehicleType;

            Console.WriteLine(k_Enter + " " + VehicleFactory.k_VehicleType + ":");
            inputVehicleType = getEnumAnswerHelper<VehicleFactory.eVehicleType>();
            o_Vehicle = VehicleFactory.GetVehicle((VehicleFactory.eVehicleType)ushort.Parse(inputVehicleType));
            o_Owner = new Owner();

            getAdditionalVehicleProperties(i_RegistrationNumber, o_Vehicle);
            getAdditionalOwnerProperties(o_Owner);
        }

        // getOwnerProperty: gets one property of owner each time, which fits it's filter.
        private string getOwnerProperty(string i_Message, eStringFilter i_Filter)
        {
            string input;
            bool isLegalInput;

            Console.WriteLine(i_Message);
            do
            {
                isLegalInput = true;
                input = getUserInput<string>();
                isLegalInput = isAllLettersOrDigits(input, i_Filter);
                if (!isLegalInput)
                {
                    Console.WriteLine(k_WrongInput + " " + k_TryAgain);
                }
            }
            while (!isLegalInput);

            return input;
        }

        // getUserInput: gets input from user (without special restriction for input range).
        private string getUserInput<T>()
        {
            return getUserInput<T>(default(float), k_MinimumValueForInput, false);
        }

        // getUserInput: gets ipnut from user.
        private string getUserInput<T>(float i_MaxRange, float i_MinRange, bool i_IsRanged)
        {
            string input;
            bool isLegalInput;

            do
            {
                isLegalInput = true;
                input = Console.ReadLine();
                try
                {
                    if (typeof(T) == typeof(int))
                    {
                        handleInput<int>(int.Parse(input));
                    }
                    else if (typeof(T) == typeof(bool))
                    {
                        handleInput<bool>(bool.Parse(input));
                    }
                    else if (typeof(T) == typeof(float) || typeof(T) == typeof(Enum))
                    {
                        handleInput<float>(float.Parse(input), i_MaxRange, i_MinRange, i_IsRanged);
                    }
                }
                catch (ValueOutOfRangeException exception)
                {
                    isLegalInput = false;
                    Console.WriteLine(exception.Message + " " + k_TryAgain);
                }
                catch (FormatException)
                {
                    isLegalInput = false;
                    Console.WriteLine(k_WrongInput + " " + k_TryAgain);
                }
            }
            while (!isLegalInput);

            return input;
        }

        // getVehiclePropertiesFromUser: gets additional vehicle's properties from user.
        private void getVehiclePropertiesFromUser(
            Dictionary<string, PropertyHolder> i_PropertiesInfo,
            Dictionary<string, string> i_PropertiesDone)
        {
            Type typeOfProperty;

            foreach (string prop in i_PropertiesInfo.Keys)
            {
                Console.WriteLine(k_Enter + " " + prop + ":");
                typeOfProperty = i_PropertiesInfo[prop].ValueType;
                if (typeOfProperty == typeof(int))
                {
                    i_PropertiesDone.Add(prop, getUserInput<int>());
                }
                else if (typeOfProperty == typeof(float))
                {
                    getFloatProperty(prop, i_PropertiesInfo, i_PropertiesDone);
                }
                else if (typeOfProperty == typeof(bool))
                {
                    getBoolProperty(prop, i_PropertiesInfo, i_PropertiesDone);
                }
                else if (typeOfProperty.IsEnum)
                {
                    getEnumProperty(prop, i_PropertiesInfo, i_PropertiesDone);
                }
                else
                {
                    i_PropertiesDone.Add(prop, getUserInput<string>());
                }
            }
        }

        // handleInput: handles the input from user and check if it's legal (without special restriction for input range).
        private void handleInput<T>(T i_Input)
        {
            handleInput<T>(i_Input, default(float), k_MinimumValueForInput, false);
        }

        // handleInput: handles the input from user and check if it's legal.
        private void handleInput<T>(T i_Input, float i_MaxRange, float i_MinRange, bool i_IsRanged)
        {
            IComparable valueComparable;

            valueComparable = (IComparable)i_Input;
            if (i_IsRanged && (valueComparable.CompareTo(i_MaxRange) > 0 || valueComparable.CompareTo(i_MinRange) < 0))
            {
                throw new ValueOutOfRangeException(i_MinRange, i_MaxRange, "Input");
            }
            else if (typeof(T) == typeof(float) && valueComparable.CompareTo(i_MinRange) < 0)
            {
                throw new ValueOutOfRangeException(i_MinRange, "Input");
            }
            else if (typeof(T) == typeof(int) && valueComparable.CompareTo((int)i_MinRange) < 0)
            {
                throw new ValueOutOfRangeException(i_MinRange, "Input");
            }
        }

        // handleMainInput: handles the main menu input and preform an menu's option.
        private bool handleMainInput(eMenuOptions userChoice)
        {
            bool isRunning = true;

            switch (userChoice)
            {
                case eMenuOptions.InsertToGarage:
                    insertVehicleToGarage();
                    break;
                case eMenuOptions.ShowRegistrationNums:
                    showRegistrationNumbers();
                    break;
                case eMenuOptions.ChangeVechicleStatus:
                    changeVehicleStatus();
                    break;
                case eMenuOptions.InflateWheels:
                    inflateVehicleWheels();
                    break;
                case eMenuOptions.RefuelGas:
                    chargeVehicleEnergy(Engine.eEngineType.Gas);
                    break;
                case eMenuOptions.ChargeElectric:
                    chargeVehicleEnergy(Engine.eEngineType.Electric);
                    break;
                case eMenuOptions.ShowAllDetails:
                    printVehicleDetails();
                    break;
                case eMenuOptions.Exit:
                    Console.WriteLine("GoodBye!");
                    isRunning = false;
                    break;
                default:
                    throw new FormatException("Bad menu option selected.");
            }

            return isRunning;
        }

        private void inflateVehicleWheels()
        {
            string registrationNumber;

            Console.WriteLine(k_Enter + " " + Vehicle.k_RegistrationNum + ":");
            registrationNumber = getUserInput<string>();

            if (m_Garage.IsVehicleExistsInGarage(registrationNumber))
            {
                m_Garage.InflateVehicleWheels(registrationNumber);
                printResult($"Vehicle {registrationNumber} wheel's were inflated to max.");
            }
            else
            {
                printResult(k_VehicleWasntFound);
            }
        }

        // insertVehicleToGarage: inserts a new vehicle to garage.
        private void insertVehicleToGarage()
        {
            string registrastionNumber; // holds new vehicle's registration number.
            bool isVehicleExists = true;
            Vehicle newVehicle = null;
            Owner newOwner = null;

            Console.WriteLine(k_Enter + " " + Vehicle.k_RegistrationNum + ":");
            try
            {
                registrastionNumber = Console.ReadLine();
                isVehicleExists = m_Garage.IsVehicleExistsInGarage(registrastionNumber);
                
                // if vehicle exists in the garage already, we need to set it's status to 'InRepair'.
                if (isVehicleExists == true)
                {
                    m_Garage.GetVehicle(registrastionNumber).VehicleStatus = Garage.eVehicleStatus.InRepair;
                    printResult(k_VehicleExist);
                }
                else
                {
                    getNewVehicleProperties(registrastionNumber, out newVehicle, out newOwner);
                    m_Garage.AddVehicle(registrastionNumber, newVehicle, newOwner);
                    printResult($"Vehicle {registrastionNumber} was added successfully to garage.");
                }
            }
            catch
            {
                printResult(k_WrongInput + " " + k_AddFail);
            }
        }

        // isAllLettersOrDigits: return True if all chars are letter or digits, depends on filter.
        private bool isAllLettersOrDigits(string i_Input, eStringFilter i_Filter)
        {
            bool isAllLettersOrDigits = true;

            foreach (char ch in i_Input)
            {
                switch (i_Filter)
                {
                    case eStringFilter.AllLetters:
                        isAllLettersOrDigits = char.IsLetter(ch);
                        break;
                    case eStringFilter.AllDigits:
                        isAllLettersOrDigits = char.IsDigit(ch);
                        break;
                }

                if (!isAllLettersOrDigits)
                {
                    break;
                }
            }

            return isAllLettersOrDigits;
        }

        private void printBounderyLine()
        {
            Console.WriteLine(k_BoundaryLine);
        }

        // printRegistrationNumbers: prints list of registration numbers.
        private void printRegistrationNumbers(List<string> i_RegistrationNumbers)
        {
            Console.Clear();
            if (i_RegistrationNumbers.Count == 0)
            {
                Console.WriteLine(k_SearchFail);
                printBounderyLine();
            }
            else
            {
                foreach (string registrationNumber in i_RegistrationNumbers)
                {
                    Console.WriteLine(registrationNumber);
                }

                printBounderyLine();
            }
        }

        private void printResult(string i_Message)
        {
            Console.Clear();
            Console.WriteLine(i_Message);
            printBounderyLine();
        }

        private void printVehicleDetails()
        {
            string registrationNumber;
            StringBuilder detailsResult = new StringBuilder();
            Dictionary<string, string> details = new Dictionary<string, string>();

            Console.WriteLine(k_Enter + " " + Vehicle.k_RegistrationNum + ":");
            registrationNumber = getUserInput<string>();

            if (m_Garage.IsVehicleExistsInGarage(registrationNumber))
            {
                m_Garage.GetVehicleDetails(registrationNumber, details);
                foreach (string prop in details.Keys)
                {
                    detailsResult.AppendLine($"{prop}: {details[prop]}");
                }

                printResult(detailsResult.ToString());
            }
            else
            {
                printResult(k_VehicleWasntFound);
            }
        }

        // showMainMenu: prints the main menu to console.
        private void showMainMenu()
        {
            const string k_MainMenu = @"Choose operation:
1) Insert a vehicle to garage.
2) See all registration numbers of vehicle in garage.
3) Change a vehicle's status.
4) Inflate car's wheels to maximum.
5) Refuel gas engine of vehicle.
6) Charge electric engine of vehicle.
7) Show All details of vehicle.
8) Exit.";
            Console.WriteLine(k_MainMenu);
        }

        // showRegistrationNumbers: prints garage's vehcile's registration numbers.
        private void showRegistrationNumbers()
        {
            List<string> registrstionNumbers;
            string input;
            Garage.eVehicleFilter filter;
            Garage.eVehicleStatus status;

            Console.WriteLine(k_Enter + " " + Garage.k_Filter + ":");
            input = getEnumAnswerHelper<Garage.eVehicleFilter>();
            filter = (Garage.eVehicleFilter)ushort.Parse(input);

            switch (filter)
            {
                case Garage.eVehicleFilter.All: // prints all vehicles in garage.
                    registrstionNumbers = m_Garage.GetAllRegistrationNumbers();
                    break;
                case Garage.eVehicleFilter.ByStatus: // prints only vehicle which fits the status.
                    Console.WriteLine(k_Enter + " " + Garage.VehicleInGarage.k_VehicleStatus + ":");
                    input = getEnumAnswerHelper<Garage.eVehicleStatus>();
                    status = (Garage.eVehicleStatus)ushort.Parse(input);
                    registrstionNumbers = m_Garage.GetRegistrationNumbersByStatus(status);
                    break;
                default:
                    registrstionNumbers = null;
                    break;
            }

            printRegistrationNumbers(registrstionNumbers);
        }
    }
}