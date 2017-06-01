using System;
namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : System.Exception
    {
        private float m_MinValue;

        private float m_MaxValue;

        public ValueOutOfRangeException(float i_MinValue,float i_MaxValue, string i_Message) : base(i_Message)
        {
            this.m_MinValue = i_MinValue;
            this.m_MaxValue = i_MaxValue;
            // TODO: should we print here
            //Console.WriteLine("Given value should be in range [{0},{1}]",this.m_MinValue,this.m_MaxValue);
        }
    }
}
