using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private const string k_MaxEnergy = "engine's max energy";

        private const string k_CurrentEnergy = "engine's current energy";

        private const string k_EngineType = "type of engine";

        private const string k_ChargeAmount = "charge amount";

        public const string k_WrongFuel = "Type of fuel / charge is not suitable with the vehicle's engine type.";

        private readonly float m_MaxEnergy;

        private const float k_MinEnergy = 0;

        private float m_CurrentEnergy;

        private eEngineType m_EngineType;

        public enum eEngineType
        {
            Electric = 1,
            Gas
        }

        protected Engine(float i_MaxEnergy, eEngineType i_EngineType)
        {
            m_EngineType = i_EngineType;
            m_MaxEnergy = i_MaxEnergy;
            m_CurrentEnergy = 0;
        }

        public float CurrentEnergy
        {
            get
            {
                return m_CurrentEnergy;
            }

            set
            {
                if (value > MaxEnergy || value < MinEnergy)
                {
                    throw new ValueOutOfRangeException(MinEnergy, MaxEnergy, k_CurrentEnergy);
                }

                m_CurrentEnergy = value;
            }
        }

        public float MaxEnergy
        {
            get
            {
                return m_MaxEnergy;
            }
        }

        public float MinEnergy
        {
            get
            {
                return k_MinEnergy;
            }
        }

        public eEngineType EngineType
        {
            get
            {
                return m_EngineType;
            }

            set
            {
                m_EngineType = value;
            }
        }

        // AddProperties: adds all the properties that needs to be inserted by user.
        public void AddProperties(Dictionary<string, PropertyHolder> i_Properties)
        {
            i_Properties.Add(k_CurrentEnergy, PropertyHolder.CreatePropertyForType<float>(MaxEnergy, MinEnergy));
        }

        // SetProperties: sets all the properties that were inserted by user.
        public void SetProperties(Dictionary<string, string> i_Properties)
        {
            CurrentEnergy = float.Parse(i_Properties[k_CurrentEnergy]);
        }

        // GetDetails: gets all the details about this object properties.
        public virtual void GetDetails(Dictionary<string, string> i_Details)
        {
            i_Details.Add(k_EngineType, m_EngineType.ToString());
            i_Details.Add(k_CurrentEnergy, m_CurrentEnergy.ToString());
            i_Details.Add(k_MaxEnergy, m_MaxEnergy.ToString());
        }

        // ChargeEnergy: charges the engine's energy. gets params as input: Engine type, Amount to charge, Type of fuel.
        public virtual void ChargeEnergy(params string[] i_Params)
        {
            float addEnergy = float.Parse(i_Params[1]);

            if (CurrentEnergy + addEnergy <= MaxEnergy)
            {
                CurrentEnergy += addEnergy;
            }
            else
            {
                throw new ValueOutOfRangeException(MinEnergy, MaxEnergy - CurrentEnergy, k_ChargeAmount);
            }
        }
    }
}
