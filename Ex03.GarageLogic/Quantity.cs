namespace Ex03.GarageLogic
{
    public class Quantity
    {
        private float m_CurrentValue;

        private float m_Min;

        private float m_Max;


        public bool IsInRange(float i_Value)
        {
            return i_Value >= m_Min && i_Value <= m_Max;
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
            set
            {
                m_Max = value;
            }
        }

        public float Min
        {
            get
            {
                return m_Min;
            }
            set
            {
                m_Min = value;
            }
        }

    }
}
