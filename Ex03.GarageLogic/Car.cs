namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Car : Vehicle
    {
        //        public const eColor eColorMin = eColor.Yellow;
        //
        //        public const eColor eColorMax = eColor.Blue;
        //
        //        public const eDoorsAmount eDoorsAmountMin = eDoorsAmount.TwoDoors;
        //
        //        public const eDoorsAmount eDoorsAmountMax = eDoorsAmount.FiveDoors;




        
        private PropertyHolder m_Color = new PropertyHolder("color", typeof(Enum));



        private PropertyHolder m_DoorsAmount = new PropertyHolder("doors amount", typeof(Enum));

        public Car(ushort i_NumberOfWheels, float i_MaxAirPressure, Engine i_Engine)
            : base(i_NumberOfWheels, i_MaxAirPressure, i_Engine)
        {
        }

        public PropertyHolder Color
        {
            get
            {
                return m_Color;
            }
        }


        public PropertyHolder DoorsAmount
        {
            get
            {
                return m_DoorsAmount;
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
            Two = 1,
            Three,
            Four,
            Five
        }

        public override string ToString()
        {
            return string.Format("{0} Car", m_Engine.ToString());
        }

    }
}
