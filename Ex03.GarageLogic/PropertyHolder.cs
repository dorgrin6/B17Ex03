using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class PropertyHolder
    {
        private Type m_ValueType;

        private List<string> m_OptionalEnumValues; //holds the type of values property can holds if it is an Enum.

        private float m_MaxFloatValue; //holds the maximum value if the property's value is ranged.

        private float m_MinFloatValue; //holds the minimum value if the property's value is ranged.

        private bool m_IsFloatRanged; //holds True if property's value is ranged, False else.

        public PropertyHolder(Type i_ValueType)
        {
            m_ValueType = i_ValueType;
            m_IsFloatRanged = false;
            m_OptionalEnumValues = new List<string>();
        }

        public PropertyHolder(Type i_ValueType, float i_MaxFloatValue, float i_MinFloatValue)
        {
            m_ValueType = i_ValueType;
            m_MaxFloatValue = i_MaxFloatValue;
            m_MinFloatValue = i_MinFloatValue;
            m_IsFloatRanged = true;
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

        public List<string> OptionalEnumValues
        {
            get
            {
                return m_OptionalEnumValues;
            }
        }

        public float MaxFloatValue
        {
            get
            {
                return m_MaxFloatValue;
            }
        }

        public float MinFloatValue
        {
            get
            {
                return m_MinFloatValue;
            }
        }

        public bool isFloatRanged
        {
            get
            {
                return m_IsFloatRanged;
            }
        }

        public static PropertyHolder createPropertyForType<T>()
        {
            return createPropertyForType<T>(default(float), default(float), false);
        }

        public static PropertyHolder createPropertyForType<T>(float i_MaxFloat, float i_MinFloat)
        {
            return createPropertyForType<T>(i_MaxFloat, i_MinFloat, true);
        }

        public static PropertyHolder createPropertyForType<T>(float i_MaxFloat, float i_MinFloat, bool i_IsFloatRanged)
        {
            PropertyHolder property = null;

            if (typeof(T) == typeof(string) || typeof(T) == typeof(int))
            {
                property = new PropertyHolder(typeof(T));
            }
            else if (typeof(T) == typeof(float))
            {
                if (i_IsFloatRanged)
                {
                    property = new PropertyHolder(typeof(float), i_MaxFloat, i_MinFloat);
                }
                else
                {
                    property = new PropertyHolder(typeof(float));
                }   
            }
            else if (typeof(T) == typeof(bool))
            {
                property = new PropertyHolder(typeof(bool));
                property.OptionalEnumValues.Add(true.ToString());
                property.OptionalEnumValues.Add(false.ToString());
            }
            else if (typeof(T).IsEnum)
            {
                property = new PropertyHolder(typeof(T));
                foreach (string name in Enum.GetNames(typeof(T)))
                {
                    property.OptionalEnumValues.Add(name);
                }
            }

            return property;
        }


    }
}
