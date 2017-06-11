namespace Ex03.GarageLogic
{
    public class Quantity
    {
        private float m_CurrentValue;

        private readonly float m_Min;

        private readonly float m_Max;

        public Quantity(float i_Min, float i_Max)
        {
            m_Min = i_Min;
            m_Max = i_Max;
        }

        public float CurrentValue
        {
            get
            {
                return m_CurrentValue;
            }
            set
            {
               m_CurrentValue = value;
            }
        }

        public float Max
        {
            get
            {
                return m_Max;
            }
        }

        public float Min
        {
            get
            {
                return m_Min;
            }
        }

        public bool IsInRange(float i_Value)
        {
            return i_Value >= m_Min && i_Value <= m_Max;
        }
    }
}
