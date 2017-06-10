namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class Vehicle
    {
        private readonly string[] m_Names = new string[] { "model name", "registration number", "current energy", "engine" };

        public enum eProperty
        {
            eModelName,
            eRegistrationNum,
            eCurrentEnergyPercentage,
            eEngine
        }

        private ushort m_PropertyIndex = 0;

        public void SetProperty(eProperty i_Property, string i_Value)
        {
            switch (i_Property)
            {
               case eProperty.eCurrentEnergyPercentage:
                    // check valid
                    m_EneregyPercentageLeft = float.Parse(i_Value);
                    break;
                case eProperty.eRegistrationNum:
                    break;
                case eProperty.eModelName:
                    break;
                case eProperty.eEngine:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(i_Property), i_Property, null);
            }
        }

        public Dictionary<string, eProperty> GetProperties()
        {
            Dictionary<string, eProperty> result = new Dictionary<string, eProperty>();

            int index = 0;
            foreach (eProperty prop in Enum.GetValues(typeof(eProperty)))
            {
                result.Add(m_Names[index], prop);
                ++index;
            }

            return result;
        }

        protected readonly string m_ModelName;

        protected readonly string m_RegistrationNum;

        protected float m_EneregyPercentageLeft;

        protected Engine m_Engine;

        private const string k_RegistrationNumName = "registration number";

        private const string k_ModelName = "model name";

        private const string k_CurrentEnergyLevelName = "current energy precentage";

        // TODO: should m_EneregyPercentageLeft be also/just in Engine??

        protected List<Wheel> m_Wheels;

        // Wheel
        protected class Wheel
        {
            private readonly string m_Manufacturer;

            private float m_CurrentAirPressure;

            private float m_MaxAirPressure;

            public Wheel(string i_Manufacturer, float i_CurrentAirPressure, float i_MaxAirPressure)
            {
                m_Manufacturer = i_Manufacturer;
                m_CurrentAirPressure = i_CurrentAirPressure;
                m_MaxAirPressure = i_MaxAirPressure;
            }

            public void Inflate(float i_AddAir)
            {
                if (m_CurrentAirPressure + i_AddAir <= m_MaxAirPressure)
                {
                    m_CurrentAirPressure += i_AddAir;
                }
                else
                {
                    //throw new ValueOutOfRangeException();
                }
            }
        }

        public abstract class Engine
        {
            protected readonly float m_MaxEnergy;

            protected float m_CurrentEnergy;

            public enum eEngineType
            {
                Electric,
                Gas
            }

            protected Engine(float i_MaxEnergy)
            {
                this.m_MaxEnergy = i_MaxEnergy;
                this.m_CurrentEnergy = i_MaxEnergy;
            }

            /*
            protected Engine(float i_MaxEnergy, float i_CurrentEnergy)
            {
                m_MaxEnergy = i_MaxEnergy;
                m_CurrentEnergy = i_CurrentEnergy;
            }*/

            public override string ToString()
            {
                string result = string.Empty;
                if (this is ElectricEngine)
                {
                    result = "Electric";
                }
                else
                {
                    if (this is GasEngine)
                    {
                        result = "Gas";
                    }
                }

                return result;
            }
        }

        public Vehicle(ushort i_NumberOfWheels, float i_MaxAirPressure, Engine i_Engine)
        {
            m_Wheels = new List<Wheel>(i_NumberOfWheels);
            // foreach wheel set maxAirPressure or during the constructor

            m_Engine = i_Engine;
        }

        public virtual void GetVehicleProperties(Dictionary<string, AnswerBank> i_VehicleProperties, Garage.VehicleInGarage i_VehicleToSet)
        {
            i_VehicleProperties.Add(k_RegistrationNumName, new AnswerBank(typeof(string)));
           // i_VehicleProperties.Add(k_CurrentEnergyLevelName, new AnswerBank());
        }

        protected void setterRangeCheck(int i_Value, int i_MinVal, int i_MaxVal, string i_Name)
        {
            if (i_Value > i_MaxVal || i_Value < i_MinVal)
            {
                throw new ValueOutOfRangeException(i_MinVal, i_MaxVal, i_Name);
            }
        }

        public abstract List<string> GetAdditionalPropertiesNames();

        public override int GetHashCode()
        {
            return int.Parse(this.m_RegistrationNum);
        }

        public override bool Equals(object i_Other)
        {
            bool result;

            if (!(i_Other is Vehicle))
            {
                result = false;
            }
            else
            {
                result = (i_Other as Vehicle).m_RegistrationNum == this.m_RegistrationNum;
            }

            return result;
        }
    }
}
