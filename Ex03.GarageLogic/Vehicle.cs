namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class Vehicle
    {
        protected readonly string m_RegistrationNum;

        protected string m_ModelName = string.Empty;

        protected float m_EnergyPercentageLeft;

        protected Engine m_Engine;

        protected List<Wheel> m_Wheels;

        // --------------------Wheel------------------------

        public class Wheel
        {
            private readonly string m_Manufacturer;

            private readonly float m_MaxAirPressure;

            private readonly float m_MinAirPressure;

            private float m_CurrentAirPressure;

            public Wheel(string i_Manufacturer, float i_MaxAirPressure)
            {
                m_Manufacturer = i_Manufacturer;
                m_MaxAirPressure = i_MaxAirPressure;
                m_MinAirPressure = 0;
                m_CurrentAirPressure = 0;
            }

            public string Manufacturer
            {
                get
                {
                    return m_Manufacturer;
                }
            }

            public float CurrentAirPressure
            {
                get
                {
                    return m_CurrentAirPressure;
                }
                set
                {
                    m_CurrentAirPressure = value;
                }
            }

            public float MaxAirPressure
            {
                get
                {
                    return m_MaxAirPressure;
                }
            }

            public float MinAirPressure
            {
                get
                {
                    return m_MinAirPressure;
                }
            }

            private void addNamesToDictionary()
            {
                NamesDictionary.AddName("Manufacturer", "manufacturer");
                NamesDictionary.AddName("CurrentAirPressure", "current air pressure");
                NamesDictionary.AddName("MaxAirPressure", "max air pressure");
                NamesDictionary.AddName("MinAirPressure", "min air pressure");
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


        //------------------Engine---------------------------
        public abstract class Engine
        {
            private readonly float m_MaxEnergy;

            private readonly float m_MinEnergy;

            private float m_CurrentEnergy;

            public enum eEngineType
            {
                Electric,
                Gas
            }

            protected Engine(float i_MaxEnergy)
            {
                m_MaxEnergy = i_MaxEnergy;
                m_MinEnergy = 0;
                m_CurrentEnergy = 0;
                addNamesToDictionary();
            }

            public float CurrentEnergy
            {
                get
                {
                    return m_CurrentEnergy;
                }
                set
                {
                    m_CurrentEnergy = value;
                }
            }

            public float MaxEnergy
            {
                get
                {
                    return m_MaxEnergy;
                }
            }

            public float MinEnergy
            {
                get
                {
                    return m_MinEnergy;
                }
            }

            private void addNamesToDictionary()
            {
                NamesDictionary.AddName("CurrentEnergy", "current energy");
                NamesDictionary.AddName("MaxEnergy", "max energy");
                NamesDictionary.AddName("MinEnergy", "min energy");
            }

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
            addVehicleNamesToDictionary();
        }

        protected abstract void addNamesToDictionary();

        private void addVehicleNamesToDictionary()
        {
            NamesDictionary.AddName("RegistrationNum", "registration number");
            NamesDictionary.AddName("ModelName", "model name");
            NamesDictionary.AddName("EnergyPercentageLeft", "energy percentage left");
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
            set
            {
                m_ModelName = value;
            }
        }

        public float EnergyPercentageLeft
        {
            get
            {
                return m_EnergyPercentageLeft;
            }
        }

        public Engine VehicleEngine
        {
            get
            {
                return m_Engine;
            }
        }

        public List<Wheel> VehicleWheels
        {
            get
            {
                return m_Wheels;
            }
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
