namespace Ex03.GarageLogic
{
    public class EnumHolder<T>
    {
        private T m_CurrentValue;

        private T m_Min;

        private T m_Max;

        public T CurrentValue
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

        public T Max
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

        public T Min
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
