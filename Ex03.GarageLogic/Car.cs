using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const string k_Color = "color";

        private const string k_DoorsAmount = "doors amount";

        private eColor m_Color;

        private eDoorsAmount m_DoorsAmount;

        public Car(ushort i_NumberOfWheels, float i_MaxAirPressure, Engine i_Engine)
            : base(i_NumberOfWheels, i_MaxAirPressure, i_Engine)
        {
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

        // AddProperties: adds all the properties that needs to be inserted by user.
        public override void AddProperties(Dictionary<string, PropertyHolder> i_Properties)
        {
            base.AddProperties(i_Properties);
            i_Properties.Add(k_Color, PropertyHolder.CreatePropertyForType<eColor>());
            i_Properties.Add(k_DoorsAmount, PropertyHolder.CreatePropertyForType<eDoorsAmount>());
        }

        // SetProperties: sets all the properties that were inserted by user.
        public override void SetProperties(Dictionary<string, string> i_Properties)
        {
            base.SetProperties(i_Properties);
            Color = (eColor)Enum.Parse(typeof(eColor), i_Properties[k_Color]);
            DoorsAmount = (eDoorsAmount)Enum.Parse(typeof(eDoorsAmount), i_Properties[k_DoorsAmount]);
        }

        // GetDetails: gets all the details about this object properties.
        public override void GetDetails(Dictionary<string, string> i_Details)
        {
            base.GetDetails(i_Details);
            i_Details.Add(k_Color, m_Color.ToString());
            i_Details.Add(k_DoorsAmount, m_DoorsAmount.ToString());
        }

        public override string ToString()
        {
            return string.Format("{0} Car", VehicleEngine.EngineType.ToString());
        }
    }
}
