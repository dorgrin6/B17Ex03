namespace Ex03.GarageLogic
{
    class Truck : Vehicle
    {
        private bool m_IsCarryingHazardousMaterials;

        private float m_MaxCarryingWeight;

        public Truck(ushort i_NumberOfWheels, float i_MaxAirPressure, Engine i_Engine)
            : base(i_NumberOfWheels, i_MaxAirPressure, i_Engine)
        {
        }
    }
}
