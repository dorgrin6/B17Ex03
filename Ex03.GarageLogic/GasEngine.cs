using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class GasEngine : Engine
    {
        public const string k_FuelType = "type of fuel";

        private eFuelType m_FuelType;

        public enum eFuelType
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98
        }

        public GasEngine(eFuelType i_FuelType, float i_MaxEnergy) : base(i_MaxEnergy, eEngineType.Gas)
        {
            m_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
        }

        // GetDetails: gets all the details about this object properties.
        public override void GetDetails(Dictionary<string, string> i_Details)
        {
            base.GetDetails(i_Details);
            i_Details.Add(k_FuelType, m_FuelType.ToString());
        }

        // ChargeEnergy: charges the engine's energy. gets params as input: Engine type, Amount to charge, Type of fuel.
        public override void ChargeEnergy(params string[] i_Params)
        {
            if (FuelType.ToString() == i_Params[2])
            {
                base.ChargeEnergy(i_Params);
            }
            else
            {
                throw new ArgumentException(Engine.k_WrongFuel);
            }
        }
    }
}
