namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Reflection.Emit;

    public class PropertyHolder
    {
        private readonly string r_Name;

        private Type m_ValueType;

        private object m_Value;

        public PropertyHolder(string i_Name, Type i_ValueType)
        {
            r_Name = i_Name;
            m_ValueType = i_ValueType;
        }

        public PropertyHolder(string i_Name, Type i_ValueType, object i_Value)
        {
            r_Name = i_Name;
            m_ValueType = i_ValueType;
            m_Value = i_Value;
        }

        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        public object Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
            }
        }

        public Type ValueType
        {
            get
            {
                return m_ValueType;
            }
            set
            {
                m_ValueType = value;
            }
        }

        public override string ToString()
        {
            return this.r_Name;
        }
    }
}
