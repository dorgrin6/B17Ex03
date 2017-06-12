namespace Ex03.GarageLogic
{
    public class GasEngine : Vehicle.Engine
    {
        private eFuelType m_FuelType;

        public enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        public GasEngine(eFuelType i_FuelType, float i_MaxEnergy) : base(i_MaxEnergy, eEngineType.Gas)
        {
            m_FuelType = i_FuelType;
        }

    }
}
