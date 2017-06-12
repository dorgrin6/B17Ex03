namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class Vehicle
    {
        private const string k_RegistrationNum = "registration number";

        private const string k_ModelName = "model name";

        private const string k_EnergyPercentageLeft = "energy percentage left";

        private string m_RegistrationNum;

        private string m_ModelName;

        private float m_EnergyPercentageLeft;

        private Engine m_Engine;

        private List<Wheel> m_Wheels;
 
        public Vehicle(ushort i_NumberOfWheels, float i_MaxAirPressure, Engine i_Engine)
        {
            m_Wheels = new List<Wheel>();
            for(int i = 0; i < i_NumberOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(i_MaxAirPressure));
            }
            m_Engine = i_Engine;
        }

        public string RegistrationNumber
        {
            get
            {
                return m_RegistrationNum;
            }
            set
            {
                m_RegistrationNum = value;
            }
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
            set
            {
                m_EnergyPercentageLeft = value;
            }
        }

        public Engine VehicleEngine
        {
            get
            {
                return m_Engine;
            }
        }

        public Wheel VehicleWheel
        {
            get
            {
                return m_Wheels[0];
            }
        }
        

        public virtual void AddProperties(Dictionary<string, PropertyHolder> i_Properties)
        {
            i_Properties.Add(k_ModelName, PropertyHolder.createPropertyForType<string>());
            VehicleEngine.AddProperties(i_Properties);
            VehicleWheel.AddProperties(i_Properties);
        }

        public virtual void SetProperties(Dictionary<string, string> i_Properties)
        {
            m_ModelName = i_Properties[k_ModelName];
            VehicleEngine.SetProperties(i_Properties);
            m_EnergyPercentageLeft = (VehicleEngine.CurrentEnergy / VehicleEngine.MaxEnergy) * 100;
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.SetProperties(i_Properties);
            }
        }

        public virtual void GetDetails(Dictionary<string,string> i_Details)
        {
            int wheelIndex = 1;

            i_Details.Add(k_RegistrationNum, m_RegistrationNum);
            i_Details.Add(k_ModelName, m_ModelName);
            VehicleEngine.GetDetails(i_Details);
            i_Details.Add(k_EnergyPercentageLeft, m_EnergyPercentageLeft.ToString());
            foreach(Wheel wheel in m_Wheels)
            {
                wheel.GetDetails(i_Details, wheelIndex);
                wheelIndex++;
            }
        }

        public void InflateWheels()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.InflateToMax();
            }
        }
    }
}
