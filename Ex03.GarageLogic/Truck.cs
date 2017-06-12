using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const string k_IsCarryingHazardousMaterials = "has hazardous materials";

        private const string k_MaxCarryingWeight = "max carrying weight";

        private bool m_IsCarryingHazardousMaterials;

        private float m_MaxCarryingWeight;

        public Truck(ushort i_NumberOfWheels, float i_MaxAirPressure, Engine i_Engine)
            : base(i_NumberOfWheels, i_MaxAirPressure, i_Engine)
        {
        }

        public bool IsCarryingHazardousMaterials
        {
            get
            {
                return m_IsCarryingHazardousMaterials;
            }

            set
            {
                m_IsCarryingHazardousMaterials = value;
            }
        }

        public float MaxCarryingWeight
        {
            get
            {
                return m_MaxCarryingWeight;
            }

            set
            {
                //setterRangeCheck((int)value,0,int.MaxValue,"Max carry weight");
                m_MaxCarryingWeight = value;
            }
        }

        public override void AddProperties(Dictionary<string, PropertyHolder> i_Properties)
        {
            base.AddProperties(i_Properties);
            i_Properties.Add(k_IsCarryingHazardousMaterials, PropertyHolder.createPropertyForType<bool>());
            i_Properties.Add(k_MaxCarryingWeight, PropertyHolder.createPropertyForType<float>());
        }

        public override void SetProperties(Dictionary<string, string> i_Properties)
        {
            base.SetProperties(i_Properties);
            IsCarryingHazardousMaterials = bool.Parse(i_Properties[k_IsCarryingHazardousMaterials]);
            MaxCarryingWeight = float.Parse(i_Properties[k_MaxCarryingWeight]);
        }

        public override string ToString()
        {
            return "Truck";
        }

        public override void GetDetails(Dictionary<string, string> i_Details)
        {
            base.GetDetails(i_Details);
            i_Details.Add(k_IsCarryingHazardousMaterials, m_IsCarryingHazardousMaterials.ToString());
            i_Details.Add(k_MaxCarryingWeight, m_MaxCarryingWeight.ToString());
        }
    }
}
