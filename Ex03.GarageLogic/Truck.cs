namespace Ex03.GarageLogic
{
    using System.Collections.Generic;

    class Truck : Vehicle
    {
        private bool m_IsCarryingHazardousMaterials;

        private float m_MaxCarryingWeight;

        public Truck(ushort i_NumberOfWheels, float i_MaxAirPressure, Engine i_Engine)
            : base(i_NumberOfWheels, i_MaxAirPressure, i_Engine)
        {
        }

        public bool IsCarryingHazardousMaterials
        {
            get
            {
                return this.m_IsCarryingHazardousMaterials;
            }

            set
            {
                this.m_IsCarryingHazardousMaterials = value;
            }
        }

        public float MaxCarryWeight
        {
            get
            {
                return this.m_MaxCarryingWeight;
            }

            set
            {
                this.setterRangeCheck((int)value,0,int.MaxValue,"Max carry weight");
                this.m_MaxCarryingWeight = value;
            }
        }

        public override List<string> GetAdditionalPropertiesNames()
        {
            return new List<string>() { "Is it carrying hazardous matriels?", "What's the max carrying weight?"};
        }

        public override string ToString()
        {
            return "Truck";
        }
    }
}
