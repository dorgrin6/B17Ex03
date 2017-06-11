namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Car : Vehicle
    {
        private eColor m_Color;

        private eDoorsAmount m_DoorsAmount;

        public Car(ushort i_NumberOfWheels, float i_MaxAirPressure, Engine i_Engine)
            : base(i_NumberOfWheels, i_MaxAirPressure, i_Engine)
        {
            addNamesToDictionary();
        }

        public eColor Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                m_Color = value;
            }
        }

        public eDoorsAmount DoorsAmount
        {
            get
            {
                return m_DoorsAmount;
            }
            set
            {
                m_DoorsAmount = value;
            }

        }

        protected override void addNamesToDictionary()
        {
            NamesDictionary.AddName("Color", "color");
            NamesDictionary.AddName("DoorsAmount", "doors amount");
            NamesDictionary.AddName("Yellow", "yellow");
            NamesDictionary.AddName("White", "white");
            NamesDictionary.AddName("Black", "black");
            NamesDictionary.AddName("Blue", "blue");
            NamesDictionary.AddName("Two", "two doors");
            NamesDictionary.AddName("Three", "three doors");
            NamesDictionary.AddName("Four", "four doors");
            NamesDictionary.AddName("Five", "five doors");
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
