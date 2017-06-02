namespace Ex03.GarageLogic
{
    public class GasEngine : Vehicle.Engine
    {
        private eFuelType m_FuelType;

        public GasEngine(eFuelType i_FuelType, float i_MaxEnergy) : base(i_MaxEnergy)
        {
            m_FuelType = i_FuelType;
        }

        public enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }
    }
}
