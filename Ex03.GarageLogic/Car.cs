namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        enum eColor
        {
            Yellow,
            White,
            Black,
            Blue
        }

        enum eDoorsAmount
        {
            // 2, 3, 4, 5
        }

        eColor m_Color;
        eDoorsAmount m_DoorsAmount;

        public Car(ushort i_NumberOfWheels, float i_MaxAirPressure, Engine i_Engine)
            : base(i_NumberOfWheels, i_MaxAirPressure, i_Engine)
        { 
        }
    }
}
