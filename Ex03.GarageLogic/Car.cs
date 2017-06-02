namespace Ex03.GarageLogic
{
    using System;

    public class Car : Vehicle
    {
        private eDoorsAmount m_DoorsAmount;

        private eColor m_Color;

        public Car(ushort i_NumberOfWheels, float i_MaxAirPressure, Engine i_Engine)
            : base(i_NumberOfWheels, i_MaxAirPressure, i_Engine)
        {
        }

        public eColor Color
        {
            get
            {
                return this.m_Color;
            }

            set
            { 
                // can insert logic
                this.m_Color = value;
            }
        }

        public eDoorsAmount DoorsAmount
        {
            get
            {
                return this.m_DoorsAmount;
            }

            set
            {
                this.m_DoorsAmount = value;
            }
        }

        public enum eColor
        {
            LowerBound,
            Yellow,
            White,
            Black,
            Blue,
            UpperBound
        }

        public enum eDoorsAmount
        {
            LowerBound,
            TwoDoors,
            ThreeDoors,
            FourDoors,
            FiveDoors,
            UpperBound
        }
    }
}
