namespace Ex03.GarageLogic
{
    using System.Collections.Generic;

    public abstract class Vehicle
    {
        protected string m_ModelName;

        protected string m_RegistrationNum;

        protected float m_EneregyPercentageLeft;

        protected Engine m_Engine;

        // TODO: should m_EneregyPercentageLeft be also/just in Engine??

        protected List<Wheel> m_Wheels;



        // Wheel
        protected class Wheel
        {
            private string m_Manufacturer;

            private float m_CurrentAirPressure;

            private float m_MaxAirPressure;

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

            public Wheel(string i_Manufacturer, float i_CurrentAirPressure, float i_MaxAirPressure)
            {
                m_Manufacturer = i_Manufacturer;
                m_CurrentAirPressure = i_CurrentAirPressure;
                m_MaxAirPressure = i_MaxAirPressure;
            }
        }

        protected Vehicle(string i_ModelName, string i_RegistrationNum, float i_EneregyPercentageLeft, ushort i_WheelsAmount, Engine.eEngineType i_EngineType)
        {
            // TODO: input validation
            m_ModelName = i_ModelName;
            m_RegistrationNum = i_RegistrationNum;
            m_EneregyPercentageLeft = i_EneregyPercentageLeft;
            m_Wheels = new List<Wheel>(i_WheelsAmount);

            /*if (i_EngineType == Engine.eEngineType.Electric)
            {
                m_Engine = new ElectricEngine();
            }
            else if (i_EngineType == Engine.eEngineType.Gas)
            {
                m_Engine = new GasEngine();
            }
            else
            {
                // TODO: exception
            }*/
        }
    }
}
