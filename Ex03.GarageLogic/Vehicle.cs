namespace Ex03.GarageLogic
{
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
                    // TODO: throw ValueOutOfRangeException 
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
            protected Engine(float i_MaxEnergy, float i_CurrentEnergy)
            {
                m_MaxEnergy = i_MaxEnergy;
                m_CurrentEnergy = i_CurrentEnergy;
            }
        }

        public Vehicle(ushort i_NumberOfWheels, float i_MaxAirPressure, Engine.eEngineType i_EngineType)
        {
            m_Wheels = new List<Wheel>(i_NumberOfWheels);
            //foreach wheel set maxAirPressure or during the constructor

            if (i_EngineType == Engine.eEngineType.Electric)
            {
                this.m_Engine = new ElectricEngine();
            }
            else if (i_EngineType == Engine.eEngineType.Gas)
            {
                this.m_Engine = new GasEngine();
            }
        }
    }
}
