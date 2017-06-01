namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private eEngineType m_Type;

        private float m_CurrentEnergyLevel;

        private readonly float m_MaxEnergyLevel;

        protected Engine(eEngineType i_EngineType)
        {
            m_Type = i_EngineType;
        }

        protected abstract void RenewEnergy(float i_Amount);

        public eEngineType Type
        {
            get
            {
                return m_Type;
            }

            set
            {
                m_Type = value;
            }
        }

        public enum eEngineType
        {
            Gas,
            Electric
        }
    }
}
