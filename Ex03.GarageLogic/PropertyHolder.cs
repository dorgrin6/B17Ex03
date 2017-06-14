namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;

    public class PropertyHolder
    {
        private bool m_IsFloatRanged; // holds True if property's value is ranged, False else.

        private float m_MaxBound; // holds the maximum value if the property's value is ranged.

        private float m_MinBound; // holds the minimum value if the property's value is ranged.

        private List<string> m_OptionalEnumValues; // holds the type of values property can holds if it is an Enum.

        private Type m_ValueType;

        public PropertyHolder(Type i_ValueType)
        {
            m_ValueType = i_ValueType;
            m_IsFloatRanged = false;
            m_OptionalEnumValues = new List<string>();
        }

        public PropertyHolder(Type i_ValueType, float i_MaxBound, float i_MinBound)
        {
            m_ValueType = i_ValueType;
            m_MaxBound = i_MaxBound;
            m_MinBound = i_MinBound;
            m_IsFloatRanged = true;
        }

        public bool isFloatRanged
        {
            get
            {
                return m_IsFloatRanged;
            }
        }

        public float MaxFloatValue
        {
            get
            {
                return m_MaxBound;
            }
        }

        public float MinFloatValue
        {
            get
            {
                return m_MinBound;
            }
        }

        public List<string> OptionalEnumValues
        {
            get
            {
                return m_OptionalEnumValues;
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

        public static PropertyHolder CreatePropertyForType<T>()
        {
            const bool v_isPropertyValueIsRanged = true;

            return createPropertyForType<T>(default(float), default(float), !v_isPropertyValueIsRanged);
        }

        public static PropertyHolder CreatePropertyForType<T>(float i_MaxBound, float i_MinBound)
        {
            const bool v_isPropertyValueIsRanged = true;

            return createPropertyForType<T>(i_MaxBound, i_MinBound, v_isPropertyValueIsRanged);
        }

        private static PropertyHolder createPropertyForType<T>(float i_MaxBound, float i_MinBound, bool i_IsFloatRanged)
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
                    property = new PropertyHolder(typeof(float), i_MaxBound, i_MinBound);
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