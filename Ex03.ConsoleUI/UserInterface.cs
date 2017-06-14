using System;
using Ex03.GarageLogic;
namespace Ex03.ConsoleUI
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Text;

    public enum eEnumBounds
    {
        Max,

        Min
    }

    public class UserInterface
    {
        private readonly Garage m_Garage = new Garage();

        private const string k_BoundaryLine = "-----------------------------";

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
            InsertToGarage = 1,
            ShowRegistrationNums,
            ChangeVechicleStatus,
            InflateWheels,
            RefuelGas,
            ChargeElectric,
            ShowAllDetails,
            Exit
        }

        public void Run()
        {
            string input;
            bool isRunning = true;
            eMenuOptions userChoice;
            Type type = typeof(eMenuOptions);

            do
            {
                showMainMenu();
                input = 
                    getUserInput<Enum>(getEnumBound(type, eEnumBounds.Max), getEnumBound(type, eEnumBounds.Min), true);
                userChoice = (eMenuOptions)Enum.Parse(typeof(eMenuOptions), input);
                isRunning = handleMainInput(userChoice);
            }
            while (isRunning);
        }

        private void showMainMenu()
        {
            const string k_MainMenu =
@"Choose operation:
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

        private string getUserInput<T>()
        {
            return getUserInput<T>(default(float), default(float), false);
        }

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
                        handleInput<int>(input, int.Parse);
                    }
                    else if (typeof(T) == typeof(bool))
                    {
                        handleInput<bool>(input, bool.Parse);
                    }
                    else if (typeof(T) == typeof(float) || typeof(T) == typeof(Enum))
                    {
                        handleInput<float>(input, i_MaxRange, i_MinRange, i_IsRanged, float.Parse);
                    }
                }
                catch (ValueOutOfRangeException exception)
                {
                    isLegalInput = false;
                    Console.WriteLine("Wrong input. Value should be between {0} to {1}.", exception.minValue, exception.maxValue);
                    Console.WriteLine("Please try again.");
                }
                catch (FormatException)
                {
                    isLegalInput = false;
                    Console.WriteLine("Wrong Input. Please try again.");
                }
            }
            while (!isLegalInput);

            return input;
        }


        private void handleInput<T>(string i_Input, Func<string, T> i_Parse)
        {
            handleInput<T>(i_Input, default(float), default(float), false, i_Parse);
        }

        private void handleInput<T>(string i_Input, float i_MaxRange, float i_MinRange, bool i_IsRanged, Func<string, T> i_Parse)
        {
            T value;
            IComparable valueComparable;

            value = i_Parse(i_Input);
            valueComparable = (IComparable)value;
            if (i_IsRanged && (valueComparable.CompareTo(i_MaxRange) > 0 || valueComparable.CompareTo(i_MinRange) < 0))
            {
                throw new ValueOutOfRangeException(i_MinRange, i_MaxRange, string.Empty);
            }
        }

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

        private void insertVehicleToGarage()
        {
            string messageRegistrationNumber = "Please enter the vehicle's registration number:";
            string inputRegistrationNumber;
            bool isLegalInput = true;
            Vehicle newVehicle = null;
            Owner newOwner = null;

            Console.WriteLine(messageRegistrationNumber);
            try
            {
                do
                {
                    inputRegistrationNumber = Console.ReadLine();
                    isLegalInput = !(m_Garage.isVehicleExistsInGarage(inputRegistrationNumber));
                    if (isLegalInput == false)
                    {
                        Console.WriteLine("The vehicle is already exists in the garage. Please try another registration number.");
                        m_Garage.GetVehicle(inputRegistrationNumber).VehicleStatus = Garage.eVehicleStatus.InRepair;
                        printBounderyLine();
                    }
                }
                while (!isLegalInput);
                getNewVehicleProperties(inputRegistrationNumber, out newVehicle, out newOwner);
                m_Garage.AddVehicle(inputRegistrationNumber, newVehicle, newOwner);
                printResult(
                    string.Format("Vehicle {0} was added successfully to garage.", inputRegistrationNumber));
            }
            catch
            {
                printResult("Wrong input. Vehicle wasn't added to garage.");
            }

        }

        private void printResult(string i_Title)
        {
            Console.Clear();
            Console.WriteLine(i_Title);
            printBounderyLine();
        }

        private void getNewVehicleProperties(string i_RegistrationNumber, out Vehicle o_Vehicle, out Owner o_Owner)
        {
            string messageVehicleType = "Please choose the vehicle's type:";
            string inputVehicleType;

            Console.WriteLine(messageVehicleType);
            inputVehicleType = getEnumAnswerHelper<VehicleFactory.eVehicleType>();
            o_Vehicle = VehicleFactory.GetVehicle((VehicleFactory.eVehicleType)ushort.Parse(inputVehicleType));
            o_Owner = new Owner();

            getAdditionalVehicleProperties(i_RegistrationNumber, o_Vehicle);
            getAdditionalOwnerDetails(o_Owner);
        }

        private void getAdditionalVehicleProperties(string i_RegistrationNumber, Vehicle i_Vehicle)
        {
            Dictionary<string, PropertyHolder> propertiesInfo = new Dictionary<string, PropertyHolder>();
            Dictionary<string, string> propertiesDone = new Dictionary<string, string>();

            i_Vehicle.RegistrationNumber = i_RegistrationNumber;
            i_Vehicle.AddProperties(propertiesInfo);
            getVehiclePropertiesFromUser(propertiesInfo, propertiesDone);
            i_Vehicle.SetProperties(propertiesDone);
        }

        private void getVehiclePropertiesFromUser(Dictionary<string, PropertyHolder> i_PropertiesInfo, Dictionary<string, string> i_PropertiesDone)
        {
            Type typeOfProperty;
            foreach (string prop in i_PropertiesInfo.Keys)
            {
                Console.WriteLine("Please enter {0}:", prop);
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

        private void getFloatProperty(string i_PropertyName, Dictionary<string, PropertyHolder> i_PropertiesInfo,
            Dictionary<string, string> i_PropertiesDone)
        {
            if (i_PropertiesInfo[i_PropertyName].isFloatRanged)
            {
                i_PropertiesDone.Add(
                    i_PropertyName, getUserInput<float>(
                        i_PropertiesInfo[i_PropertyName].MaxFloatValue, i_PropertiesInfo[i_PropertyName].MinFloatValue, true));
            }
            else
            {
                i_PropertiesDone.Add(i_PropertyName, getUserInput<float>());
            }
        }

        private void getBoolProperty(string i_PropertyName, Dictionary<string, PropertyHolder> i_PropertiesInfo,
            Dictionary<string, string> i_PropertiesDone)
        {
            Console.WriteLine("Choose True/False.");
            i_PropertiesDone.Add(i_PropertyName, getUserInput<bool>());
        }

        private void getEnumProperty(string i_PropertyName, Dictionary<string, PropertyHolder> i_PropertiesInfo,
            Dictionary<string, string> i_PropertiesDone)
        {
            Type type;
            string input;

            type = i_PropertiesInfo[i_PropertyName].ValueType;
            Console.WriteLine(createEnumaration(i_PropertiesInfo[i_PropertyName].OptionalEnumValues.ToArray()));
            input =
                 getUserInput<Enum>(getEnumBound(type, eEnumBounds.Max), getEnumBound(type, eEnumBounds.Min), true);
            i_PropertiesDone.Add(i_PropertyName, input);
        }

        private string getEnumAnswerHelper<T>()
        {
            Type type = typeof(T);
            string input;

            Console.WriteLine(createEnumaration(Enum.GetNames(type)));
            input = 
                getUserInput<Enum>(getEnumBound(type, eEnumBounds.Max), getEnumBound(type, eEnumBounds.Min), true);

            return input;
        }

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

        /*
        private int getEnumMaxValue(Type i_Enum)
        {
            Array enumValues = Enum.GetValues(i_Enum);
            return (int)enumValues.GetValue(enumValues.Length - 1);
        }

        private int getEnumMinValue(Type i_Enum)
        {
            Array enumValues = Enum.GetValues(i_Enum);
            return (int)enumValues.GetValue(0);
        }
        */

        private void getAdditionalOwnerDetails(Owner i_Owner)
        {
            string messageName = "Enter owner's name:";
            string messagePhoneNumber = "Enter owner's phone number:";

            i_Owner.Name = getOwnersDetail(messageName, char.IsLetter);
            i_Owner.PhoneNumber = getOwnersDetail(messagePhoneNumber, char.IsDigit);
        }

        private string getOwnersDetail(string i_Message, Func<char, bool> i_checkChar)
        {
            string messageWrongInput = "Invalid input. Please try again.";
            string input;
            bool isLegalInput;
            do
            {
                isLegalInput = true;
                Console.WriteLine(i_Message);
                input = getUserInput<string>();
                isLegalInput = isAllLettersOrDigits(input, i_checkChar);
                if (!isLegalInput)
                {
                    Console.WriteLine(messageWrongInput);
                }
            }
            while (!isLegalInput);

            return input;
        }

        private bool isAllLettersOrDigits(string i_Input, Func<char, bool> i_checkChar)
        {
            bool isAllLettersOrDigits = true;

            foreach (char ch in i_Input)
            {
                if (!i_checkChar(ch))
                {
                    isAllLettersOrDigits = false;
                }
            }
            return isAllLettersOrDigits;
        }


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

        private void showRegistrationNumbers()
        {
            List<string> registrstionNumbers;
            string input;
            Garage.eVehicleFilter filter;
            Garage.eVehicleStatus status;

            Console.WriteLine("Please choose a filter method:");
            input = getEnumAnswerHelper<Garage.eVehicleFilter>();
            filter = (Garage.eVehicleFilter)ushort.Parse(input);

            switch (filter)
            {
                case Garage.eVehicleFilter.All:
                    registrstionNumbers = m_Garage.GetAllRegistrationNumbers();
                    break;
                case Garage.eVehicleFilter.ByStatus:
                    Console.WriteLine("Please choose a status:");
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

        private void printRegistrationNumbers(List<string> i_RegistrationNumbers)
        {
            Console.Clear();
            if (i_RegistrationNumbers.Count == 0)
            {
                Console.WriteLine("No vehicles were found in the garage which fits the search.");
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

        private void changeVehicleStatus()
        {
            string registrationNumber;
            string input;
            Garage.eVehicleStatus status;

            Console.WriteLine("Please enter a vehicle's registration number:");
            registrationNumber = getUserInput<string>();

            if (m_Garage.isVehicleExistsInGarage(registrationNumber))
            {
                Console.WriteLine("Please choose a status:");
                input = getEnumAnswerHelper<Garage.eVehicleStatus>();
                status = (Garage.eVehicleStatus)ushort.Parse(input);
                m_Garage.GetVehicle(registrationNumber).VehicleStatus = status;
                Console.Clear();
                Console.WriteLine("Vehicle {0} status was changed to: {1}", registrationNumber, status.ToString());
                printBounderyLine();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Vehicle wasn't found.");
                printBounderyLine();
            }
        }

        private void inflateVehicleWheels()
        {
            string registrationNumber;

            Console.WriteLine("Please enter a vehicle's registration number:");
            registrationNumber = getUserInput<string>();

            if (m_Garage.isVehicleExistsInGarage(registrationNumber))
            {
                m_Garage.InflateVehicleWheels(registrationNumber);
                Console.Clear();
                Console.WriteLine("Vehicle {0} wheel's were inflated to max.", registrationNumber);
                printBounderyLine();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Vehicle wasn't found.");
                printBounderyLine();
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
                        m_Garage.ChargeEnergy(registrationNumber, i_EngineType.ToString(), addEnergy.ToString(), fuelType.ToString());
                        message = "fueled";
                    }
                    Console.Clear();
                    Console.WriteLine("Vehicle {0} was successfully {1}.", registrationNumber, message);
                    printBounderyLine();
                }
            }
            catch (ArgumentException)
            {
                //we should send exceptiom.message
                Console.Clear();
                Console.WriteLine("Wrong input. Type of fuel/charge is not suitable with the vehicle's engine type.");
                printBounderyLine();
            }
            catch (ValueOutOfRangeException exception)
            {
                //we should send exception.message
                Console.Clear();
                Console.WriteLine("Wrong input. Amount of energy should be between {0} to {1}.", exception.minValue, exception.maxValue);
                printBounderyLine();
            }
        }


        private bool getBasicDataForCharging(Engine.eEngineType i_EngineType, out string o_RegistrationNumber, out float o_AddEnergy)
        {
            string input;
            o_AddEnergy = default(float);
            bool isVehicleExists = true;

            Console.WriteLine("Please enter a vehicle's registration number:");
            o_RegistrationNumber = getUserInput<string>();
            if (m_Garage.isVehicleExistsInGarage(o_RegistrationNumber))
            {
                if (i_EngineType == Engine.eEngineType.Gas)
                {
                    Console.WriteLine("Please enter the amount of gas to fuel:");
                }
                else if (i_EngineType == Engine.eEngineType.Electric)
                {
                    Console.WriteLine("Please enter the amount of electricity to charge (in minutes):");
                }
                input = getUserInput<float>();
                o_AddEnergy = float.Parse(input);
            }
            else
            {
                isVehicleExists = false;
                Console.Clear();
                Console.WriteLine("Vehicle wasn't found.");
                printBounderyLine();
            }
            return isVehicleExists;
        }

        private GasEngine.eFuelType getFuelForCharging()
        {
            string input;
            GasEngine.eFuelType fuel;

            Console.WriteLine("Please enter the type of fuel:");
            input = getEnumAnswerHelper<GasEngine.eFuelType>();
            fuel = (GarageLogic.GasEngine.eFuelType)ushort.Parse(input);

            return fuel;
        }

        private void printVehicleDetails()
        {
            string registrationNumber;
            Dictionary<string, string> details = new Dictionary<string, string>();

            Console.WriteLine("Please enter a vehicle's registration number:");
            registrationNumber = getUserInput<string>();

            if (m_Garage.isVehicleExistsInGarage(registrationNumber))
            {
                m_Garage.GetVehicleDetails(registrationNumber, details);
                Console.Clear();
                foreach (string prop in details.Keys)
                {
                    Console.Write("{0}: ", prop);
                    Console.WriteLine(details[prop]);
                }
                printBounderyLine();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Vehicle wasn't found.");
                printBounderyLine();
            }

        }

        private void printBounderyLine()
        {
            Console.WriteLine(k_BoundaryLine);
        }
    }
}

