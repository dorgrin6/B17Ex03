namespace Ex03.GarageLogic
{
    using System.Collections.Generic;

    public abstract class Vehicle
    {
        public const string k_RegistrationNum = "registration number";

        private const string k_EnergyPercentageLeft = "energy percentage left";

        private const string k_ModelName = "model name";

        private float m_EnergyPercentageLeft;

        private Engine m_Engine;

        private string m_ModelName;

        private string m_RegistrationNum;

        private List<Wheel> m_Wheels;

        public Vehicle(ushort i_NumberOfWheels, float i_MaxAirPressure, Engine i_Engine)
        {
            m_Wheels = new List<Wheel>();
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(i_MaxAirPressure));
            }
            m_Engine = i_Engine;
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

        // AddProperties: adds all the properties that needs to be inserted by user.
        public virtual void AddProperties(Dictionary<string, PropertyHolder> i_Properties)
        {
            i_Properties.Add(k_ModelName, PropertyHolder.CreatePropertyForType<string>());
            VehicleEngine.AddProperties(i_Properties);
            VehicleWheel.AddProperties(i_Properties);
        }

        // ChargeEnergy: charges the engine's energy and calculates the updated energy percentage.
        public void ChargeEnergy(params string[] i_Params)
        {
            VehicleEngine.ChargeEnergy(i_Params);
            CalculateEnergyPercentage();
        }

        // GetDetails: gets all the details about this object properties.
        public virtual void GetDetails(Dictionary<string, string> i_Details)
        {
            int wheelIndex = 1;

            i_Details.Add(k_RegistrationNum, m_RegistrationNum);
            i_Details.Add(k_ModelName, m_ModelName);
            VehicleEngine.GetDetails(i_Details);
            i_Details.Add(k_EnergyPercentageLeft, m_EnergyPercentageLeft.ToString());
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.GetDetails(i_Details, wheelIndex);
                wheelIndex++;
            }
        }

        // InflateWheels: infalets the wheels to their maximum about of air.
        public void InflateWheels()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.InflateToMax();
            }
        }

        // SetProperties: sets all the properties that were inserted by user.
        public virtual void SetProperties(Dictionary<string, string> i_Properties)
        {
            m_ModelName = i_Properties[k_ModelName];
            VehicleEngine.SetProperties(i_Properties);
            CalculateEnergyPercentage();
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.SetProperties(i_Properties);
            }
        }

        // CalculateEnergyPercentage: calculates the energy percentage.
        private void CalculateEnergyPercentage()
        {
            m_EnergyPercentageLeft = (VehicleEngine.CurrentEnergy / VehicleEngine.MaxEnergy) * 100;
        }
    }
}