﻿using System;
namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : System.Exception
    {
        private float m_MinValue;

        private float m_MaxValue;

        public ValueOutOfRangeException(float i_MinValue,float i_MaxValue, string i_Message) : base(i_Message)
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
            // this exception should be caught by UI
        }
    }
}
