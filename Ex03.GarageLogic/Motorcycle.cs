namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Motorcycle : Vehicle
    {
        public const eRegistrationKind eRegistrationKindMin = eRegistrationKind.A;

        public const eRegistrationKind eRegistrationKindMax = eRegistrationKind.B1;

        private eRegistrationKind m_RegistrationKind;

        private int m_EngineVolume;

        public eRegistrationKind RegistrationKind
        {
            get
            {
                return m_RegistrationKind;
            }
            set
            {
                setterRangeCheck((int)value,(int)eRegistrationKindMin,(int)eRegistrationKindMax,"Registration kind");
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
                setterRangeCheck(value, 0, Int32.MaxValue, "Engine volume");
                m_EngineVolume = value;
            }
        }


        public enum eRegistrationKind
        {
            A,
            AB,
            A2,
            B1
        }

        public Motorcycle(ushort i_NumberOfWheels, float i_MaxAirPressure, Engine i_Engine)
            : base(i_NumberOfWheels, i_MaxAirPressure, i_Engine)
        {
            addNamesToDictionary();
        }

        public override List<string> GetAdditionalPropertiesNames()
        {
            return new List<string>()
                {
                    EnumService.GetAllItems<eRegistrationKind>("Registration kind:"),
                    "Engine volume:"
                };
        }

        protected override void addNamesToDictionary()
        {
            NamesDictionary.AddName("RegistrationKind", "kind or registration");
            NamesDictionary.AddName("EngineVolume", "engine volume");
            NamesDictionary.AddName("A", "A");
            NamesDictionary.AddName("AB", "AB");
            NamesDictionary.AddName("A2", "A2");
            NamesDictionary.AddName("B1", "B1");
        }

        public override string ToString()
        {
            return m_Engine.ToString() + " Motorcycle";
        }
    }
}
