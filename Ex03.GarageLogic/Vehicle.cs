namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class Vehicle
    {
        protected readonly string m_ModelName;

        protected readonly string m_RegistrationNum;

        protected float m_EneregyPercentageLeft;

        protected Engine m_Engine;

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
