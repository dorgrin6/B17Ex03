namespace Ex03.GarageLogic
{
    using System;

    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;

        private float m_MinValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_ValueName)
            : base(string.Format("{0} should be between {1} and {2}.", i_ValueName, i_MinValue, i_MaxValue))
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }

        public ValueOutOfRangeException(float i_MinValue, string i_ValueName)
            : base(string.Format("{0} should be bigger or equal to {1}.", i_ValueName, i_MinValue))
        {
            m_MinValue = i_MinValue;
        }

        public float maxValue
        {
            get
            {
                return m_MaxValue;
            }
        }

        public float minValue
        {
            get
            {
                return m_MinValue;
            }
        }
    }
}