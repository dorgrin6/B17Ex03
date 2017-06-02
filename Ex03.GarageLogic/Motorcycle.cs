namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eRegistrationKind m_RegistrationKind;

        private int m_EngineVolume;

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
    }
}
