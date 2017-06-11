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
            addNamesToDictionary();
        }

        public bool IsCarryingHazardousMaterials
        {
            get
            {
                return m_IsCarryingHazardousMaterials;
            }

            set
            {
                m_IsCarryingHazardousMaterials = value;
            }
        }

        public float MaxCarryWeight
        {
            get
            {
                return m_MaxCarryingWeight;
            }

            set
            {
                setterRangeCheck((int)value,0,int.MaxValue,"Max carry weight");
                m_MaxCarryingWeight = value;
            }
        }

        public override List<string> GetAdditionalPropertiesNames()
        {
            return new List<string>() { "Is it carrying hazardous matriels?", "What's the max carrying weight?"};
        }

        protected override void addNamesToDictionary()
        {
            NamesDictionary.AddName("IsCarryingHazardousMaterials", "is carrring hazardous materials");
            NamesDictionary.AddName("MaxCarryingWeight", "max carry weight");

        }

        public override string ToString()
        {
            return "Truck";
        }
    }
}
