using System;
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
            eMenuOptions userChoice;
            int enumMaxValue, enumMinValue;

            do
            {
                showMainMenu();
                getEnumMinMaxValue(typeof(eMenuOptions), out enumMaxValue, out enumMinValue);
                userChoice = (eMenuOptions)Enum.Parse(typeof(eMenuOptions),getUserInput<Enum>(enumMaxValue,enumMinValue,true));
                handleMainInput(userChoice);
            }
            while (true);
        }

        private void showMainMenu()
        {
            const string mainMenu =
@"Choose operation:
1) Insert a vehicle to garage.
2) See all registration numbers of vehicle in garage.
3) Change a vehicle's status.
4) Inflate car's wheels to maximum.
5) Refuel gas engine of vehicle.
6) Charge electric engine of vehicle.
7) Show All details of vehicle.
8) Exit.";
            Console.WriteLine(mainMenu);
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


        private void handleInput<T>(string i_Input, Func<string,T> i_Parse)
        {
            handleInput<T>(i_Input, default(float), default(float), false, i_Parse);
        }

        private void handleInput<T>(string i_Input, float i_MaxRange, float i_MinRange, bool i_IsRanged, Func<string,T> i_Parse)
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

        private void handleMainInput(eMenuOptions userChoice)
        {
            switch (userChoice)
            {
                case eMenuOptions.InsertToGarage:
                    insertVehicleToGarage();
                    break;
                case eMenuOptions.ShowRegistrationNums:
                    //showRegistrationNums();
                    break;
                case eMenuOptions.ChangeVechicleStatus:
                    //changeVehicleStatus();
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
                    throw new FormatException("Bad menu option selected.");
            }
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
                    }
                }
                while (!isLegalInput);
                getNewVehicleProperties(inputRegistrationNumber, newVehicle, newOwner);
                m_Garage.AddVehicleToGarage(inputRegistrationNumber, newVehicle, newOwner);
                Console.Clear();
                Console.WriteLine("Vehicle {0} was added successfully to garage.",inputRegistrationNumber);
            }
            catch
            {
                Console.WriteLine("Wrong input. Vehicle wasn't added to garage.");
            }
            
        }

        private void getNewVehicleProperties(string i_RegistrationNumber, Vehicle i_Vehicle, Owner i_Owner)
        {
            string messageVehicleType = "Please choose the vehicle's type:";
            string inputVehicleType;
            int enumMinValue, enumMaxValue;

            Console.WriteLine(messageVehicleType);
            Console.WriteLine(createEnumaration(Enum.GetNames(typeof(VehicleFactory.eVehicleType))));
            getEnumMinMaxValue(typeof(VehicleFactory.eVehicleType), out enumMaxValue, out enumMinValue);
            inputVehicleType = getUserInput<VehicleFactory.eVehicleType>(enumMaxValue,enumMinValue,true);
            i_Vehicle = VehicleFactory.GetVehicle((VehicleFactory.eVehicleType)ushort.Parse(inputVehicleType));
            getAdditionalVehicleProperties(i_RegistrationNumber, i_Vehicle);
            getAdditionalOwnerDetails(i_Owner);
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

        private void getVehiclePropertiesFromUser(Dictionary<string,PropertyHolder> i_PropertiesInfo, Dictionary<string,string> i_PropertiesDone)
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
            int enumMinValue, enumMaxValue;
            Console.WriteLine(createEnumaration(i_PropertiesInfo[i_PropertyName].OptionalEnumValues.ToArray()));
            getEnumMinMaxValue(i_PropertiesInfo[i_PropertyName].ValueType, out enumMaxValue, out enumMinValue);
            i_PropertiesDone.Add(i_PropertyName, getUserInput<Enum>(enumMaxValue, enumMinValue, true));
        }

        private void getEnumMinMaxValue(Type i_Enum, out int o_Max, out int o_Min)
        {
            Array enumValues = Enum.GetValues(i_Enum);
            o_Min = (int)enumValues.GetValue(0);
            o_Max = (int)enumValues.GetValue(enumValues.Length - 1);
        }
        
        private void getAdditionalOwnerDetails(Owner i_Owner)
        {
            string messageName = "Enter owner's name:";
            string messagePhoneNumber = "Enter owner's phone number:";
            string ownerName;
            string ownerPhoneNumber;

            ownerName = getOwnersDetail(messageName, char.IsLetter);
            ownerPhoneNumber = getOwnersDetail(messagePhoneNumber, char.IsDigit);
            i_Owner = new Owner(ownerName, ownerPhoneNumber);
        }

        private string getOwnersDetail(string i_Message, Func<char,bool> i_checkChar)
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

        private bool isAllLettersOrDigits(string i_Input, Func<char,bool> i_checkChar)
        {
            bool isAllLettersOrDigits = true;

            foreach(char ch in i_Input)
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

        /*
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
        */


    }
}

