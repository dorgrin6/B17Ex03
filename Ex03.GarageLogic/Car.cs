namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Car : Vehicle
    {
        public const eColor eColorMin = eColor.Yellow;

        public const eColor eColorMax = eColor.Blue;

        public const eDoorsAmount eDoorsAmountMin = eDoorsAmount.TwoDoors;

        public const eDoorsAmount eDoorsAmountMax = eDoorsAmount.FiveDoors;


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
                this.setterRangeCheck((int)eColorMin, (int)eColorMax, (int)value, "Color");
                
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
                base.setterRangeCheck((int)eDoorsAmountMin, (int)eDoorsAmountMax, (int)value, "Doors amount");

                this.m_DoorsAmount = value;
            }
        }

        public override List<string> GetAdditionalPropertiesNames()
        {
            return new List<string>()
                       {
                           EnumService.GetAllItems<eColor>("Colors:"), EnumService.GetAllItems<eDoorsAmount>("Doors amount:")
                       };
        }

        public enum eColor
        {
            Yellow = 1,
            White,
            Black,
            Blue
        }

        public enum eDoorsAmount
        {
            TwoDoors = 1,
            ThreeDoors,
            FourDoors,
            FiveDoors
        }

        public override string ToString()
        {
            return string.Format("{0} Car", this.m_Engine.ToString());
        }
    }
}
