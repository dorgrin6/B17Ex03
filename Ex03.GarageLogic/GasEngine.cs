namespace Ex03.GarageLogic
{
    public class GasEngine : Engine
    {
        private eFuelType m_FuelType;

        public GasEngine(float i_CurrentFuelLevel)
            : base(eEngineType.Gas)
        {
            m_CurrentFuelLevel = i_CurrentFuelLevel;

        }

        enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        public override void RenewEnergy(float i_Amount)
        {
            refuel(i_Amount);
        }


        private refuel(float i_Amount)
        {
            
        }
    }
}
