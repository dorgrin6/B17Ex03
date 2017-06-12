using System;

namespace Ex03.GarageLogic
{
    public class GasEngine : Engine
    {
        private eFuelType m_FuelType;

        public enum eFuelType
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98
        }

        public GasEngine(eFuelType i_FuelType, float i_MaxEnergy) : base(i_MaxEnergy, eEngineType.Gas)
        {
            m_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
        }

        public override void RefuelGas(float i_AddEnergy, GasEngine.eFuelType i_FuelType)
        {
            if (FuelType == i_FuelType)
            {
                base.ChargeEnergy(i_AddEnergy);
            }
            else
            {
                throw new ArgumentException();
            }
            
        }


    }
}
