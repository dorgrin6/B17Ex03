using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private const string k_MaxAirPressure = "wheel's max air pressure";

        private const string k_CurrentAirPressure = "wheel's current air pressure";

        private const string k_Manufacturer = "wheel's manufacturer";

        private readonly float r_MaxAirPressure;

        private const float m_MinAirPressure = 0;

        private float m_CurrentAirPressure;

        private string m_Manufacturer;

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
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
                    throw new ValueOutOfRangeException(MinAirPressure, MaxAirPressure, k_CurrentAirPressure);
                }
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        public float MinAirPressure
        {
            get
            {
                return m_MinAirPressure;
            }
        }

        // AddProperties: adds all the properties that needs to be inserted by user.
        public void AddProperties(Dictionary<string, PropertyHolder> i_Properties)
        {
            i_Properties.Add(k_Manufacturer, PropertyHolder.CreatePropertyForType<string>());
            i_Properties.Add(k_CurrentAirPressure, PropertyHolder.CreatePropertyForType<float>(MaxAirPressure, MinAirPressure));
        }

        // SetProperties: sets all the properties that were inserted by user.
        public void SetProperties(Dictionary<string, string> i_Properties)
        {
            Manufacturer = i_Properties[k_Manufacturer];
            CurrentAirPressure = float.Parse(i_Properties[k_CurrentAirPressure]);
        }

        // GetDetails: gets all the details about this object properties.
        // NOTE: pay attention that the method gets also the wheel's index in it's vehicle.
        public void GetDetails(Dictionary<string, string> i_Details, int i_WheelIndex)
        {
            StringBuilder wheelNumberAndProperty = new StringBuilder();

            wheelNumberAndProperty.AppendFormat("wheel No.{0} ", i_WheelIndex.ToString());
            wheelNumberAndProperty.Append(k_Manufacturer);
            i_Details.Add(wheelNumberAndProperty.ToString(), m_Manufacturer);

            wheelNumberAndProperty.Replace(k_Manufacturer, k_CurrentAirPressure);
            i_Details.Add(wheelNumberAndProperty.ToString(), m_CurrentAirPressure.ToString());

            wheelNumberAndProperty.Replace(k_CurrentAirPressure, k_MaxAirPressure);
            i_Details.Add(wheelNumberAndProperty.ToString(), r_MaxAirPressure.ToString());
        }

        // InflateToMax: inflate the wheel air amount to max.
        public void InflateToMax()
        {
            CurrentAirPressure = MaxAirPressure;
        }
    }
}
