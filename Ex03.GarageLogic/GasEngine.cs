namespace Ex03.GarageLogic
{
    public class GasEngine : Vehicle.Engine
    {
        private eFuelType m_FuelType;

        public GasEngine(float i_MaxEnergy, float i_CurrentEnergy, eFuelType i_FuelType) : base(i_MaxEnergy, i_CurrentEnergy)
        {
            this.m_FuelType = i_FuelType;
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
