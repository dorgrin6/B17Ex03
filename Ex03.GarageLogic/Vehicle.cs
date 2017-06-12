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


        // --------------------Wheel------------------------

        public class Wheel
        {
            private const string k_MaxAirPressure = "wheel's max air pressure";

            private const string k_CurrentAirPressure = "wheel's current air pressure";

            private const string k_Manufacturer = "wheel's manufacturer";

            private readonly float m_MaxAirPressure;

            private const float m_MinAirPressure = 0;

            private float m_CurrentAirPressure;

            private string m_Manufacturer;

            public Wheel(float i_MaxAirPressure)
            {
                m_MaxAirPressure = i_MaxAirPressure;
                m_CurrentAirPressure = 0;
            }

            public string Manufacturer
            {
                get
                {
                    return m_Manufacturer;
                }
                set
                {
                    m_Manufacturer = value;
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
                    if (value > MaxAirPressure || value < MinAirPressure)
                    {
                        throw new ValueOutOfRangeException(MinAirPressure, MaxAirPressure, string.Empty);
                    }
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

            public void AddProperties(Dictionary<string, PropertyHolder> i_Properties)
            {
                i_Properties.Add(k_Manufacturer, PropertyHolder.createPropertyForType<string>());
                i_Properties.Add(k_CurrentAirPressure, PropertyHolder.createPropertyForType<float>(MaxAirPressure, MinAirPressure, true));
            }

            public void SetProperties(Dictionary<string, string> i_Properties)
            {
                Manufacturer = i_Properties[k_Manufacturer];
                CurrentAirPressure = float.Parse(i_Properties[k_CurrentAirPressure]);
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
            private const string k_MaxEnergy = "engine's max energy";

            private const string k_CurrentEnergy = "engine's current energy";

            private const string k_EngineType = "type of engine";

            private readonly float m_MaxEnergy;

            private const float m_MinEnergy = 0;

            private float m_CurrentEnergy;

            private eEngineType m_EngineType;

            public enum eEngineType
            {
                Electric = 1,
                Gas
            }

            protected Engine(float i_MaxEnergy, eEngineType i_EngineType)
            {
                m_EngineType = i_EngineType;
                m_MaxEnergy = i_MaxEnergy;
                m_CurrentEnergy = 0;
            }

            public float CurrentEnergy
            {
                get
                {
                    return m_CurrentEnergy;
                }
                set
                {
                    if (value > MaxEnergy || value < MinEnergy)
                    {
                        throw new ValueOutOfRangeException(MinEnergy, MaxEnergy, string.Empty);
                    }
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

            public eEngineType EngineType
            {
                get
                {
                    return m_EngineType;
                }
                set
                {
                    m_EngineType = value;
                }
            }

            public void AddProperties(Dictionary<string, PropertyHolder> i_Properties)
            {
                i_Properties.Add(k_CurrentEnergy, PropertyHolder.createPropertyForType<float>(MaxEnergy,MinEnergy));
            }

            public void SetProperties(Dictionary<string, string> i_Properties)
            {
                CurrentEnergy = float.Parse(i_Properties[k_CurrentEnergy]);
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
            m_Wheels = new List<Wheel>();
            for(int i = 0; i < i_NumberOfWheels; i++)
            {
                m_Wheels.Add(new GarageLogic.Vehicle.Wheel(i_MaxAirPressure));
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
            //i_Properties.Add(k_RegistrationNum, PropertyHolder.createPropertyForType<string>());
            i_Properties.Add(k_ModelName, PropertyHolder.createPropertyForType<string>());
            VehicleEngine.AddProperties(i_Properties);
            VehicleWheel.AddProperties(i_Properties);
        }

        public virtual void SetProperties(Dictionary<string, string> i_Properties)
        {
            //m_RegistrationNum = i_Properties[k_RegistrationNum];
            ModelName = i_Properties[k_ModelName];
            VehicleEngine.SetProperties(i_Properties);
            EnergyPercentageLeft = (VehicleEngine.CurrentEnergy / VehicleEngine.MaxEnergy) * 100;
            foreach (Wheel whl in m_Wheels)
            {
                whl.SetProperties(i_Properties);
            }
        }

        public virtual void GetDetails(Dictionary<string,string> i_Details)
        {

        }

        /*
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
        */
    }
}
