using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const string k_RegistrationKind = "registration kind";

        private const string k_EngineVolume = "engine volume";

        private eRegistrationKind m_RegistrationKind;

        private int m_EngineVolume;

        private const int k_MinEngineVolume = 0;

        public enum eRegistrationKind
        {
            A = 1,
            AB,
            A2,
            B1
        }

        public Motorcycle(ushort i_NumberOfWheels, float i_MaxAirPressure, Engine i_Engine)
            : base(i_NumberOfWheels, i_MaxAirPressure, i_Engine)
        {
        }

        public eRegistrationKind RegistrationKind
        {
            get
            {
                return m_RegistrationKind;
            }
            set
            {
                m_RegistrationKind = value;
            }   
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }
            set
            {
                if (value < k_MinEngineVolume)
                {
                    throw new ValueOutOfRangeException(k_MinEngineVolume, k_EngineVolume);
                }
                m_EngineVolume = value;
            }
        }

        // AddProperties: adds all the properties that needs to be inserted by user.
        public override void AddProperties(Dictionary<string, PropertyHolder> i_Properties)
        {
            base.AddProperties(i_Properties);
            i_Properties.Add(k_RegistrationKind, PropertyHolder.CreatePropertyForType<eRegistrationKind>());
            i_Properties.Add(k_EngineVolume, PropertyHolder.CreatePropertyForType<int>());
        }

        // SetProperties: sets all the properties that were inserted by user.
        public override void SetProperties(Dictionary<string, string> i_Properties)
        {
            base.SetProperties(i_Properties);
            RegistrationKind = (eRegistrationKind)Enum.Parse(typeof(eRegistrationKind), i_Properties[k_RegistrationKind]);
            EngineVolume = int.Parse(i_Properties[k_EngineVolume]);
        }

        // GetDetails: gets all the details about this object properties.
        public override void GetDetails(Dictionary<string, string> i_Details)
        {
            base.GetDetails(i_Details);
            i_Details.Add(k_EngineVolume, m_EngineVolume.ToString());
            i_Details.Add(k_RegistrationKind, m_RegistrationKind.ToString());
        }

        public override string ToString()
        {
            return VehicleEngine.EngineType.ToString() + " Motorcycle";
        }
    }
}
