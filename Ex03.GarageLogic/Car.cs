namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        readonly ushort m_DoorsAmount;
        private eColor m_Color;
        
        public Car(ushort i_NumberOfWheels, float i_MaxAirPressure, Engine i_Engine)
            : base(i_NumberOfWheels, i_MaxAirPressure, i_Engine)
        { 
        }

        public enum eColor
        {
            Yellow,
            White,
            Black,
            Blue
        }
    }
}
