using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
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

        public void InflateToMax()
        {
            CurrentAirPressure = MaxAirPressure;
        }
    }
}
