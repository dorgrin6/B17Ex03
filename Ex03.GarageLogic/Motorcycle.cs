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
                return this.m_RegistrationKind;
            }
            set
            {
                this.setterRangeCheck((int)value,(int)eRegistrationKindMin,(int)eRegistrationKindMax,"Registration kind");
                this.m_RegistrationKind = value;
            }   
        }

        public int EngineVolume
        {
            get
            {
                return this.m_EngineVolume;
            }
            set
            {
                this.setterRangeCheck(value, 0, Int32.MaxValue, "Engine volume");
                this.m_EngineVolume = value;
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
        }

        public override List<string> GetAdditionalPropertiesNames()
        {
            return new List<string>()
                       {
                           EnumService.GetAllItems<eRegistrationKind>("Registration kind:"),
                           "Engine volume:"
                       };
        }

        public override string ToString()
        {
            return this.m_Engine.ToString() + " Motorcycle";
        }
    }
}
