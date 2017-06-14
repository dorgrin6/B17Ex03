namespace Ex03.GarageLogic
{
    using System.Collections.Generic;

    public class Truck : Vehicle
    {
        private const string k_IsCarryingHazardousMaterials = "has hazardous materials";

        private const string k_MaxCarryingWeight = "max carrying weight";

        private const float k_MinCarryWeight = 0;

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
                if (value < k_MinCarryWeight)
                {
                    throw new ValueOutOfRangeException(0, k_MaxCarryingWeight);
                }
                m_MaxCarryingWeight = value;
            }
        }

        // AddProperties: adds all the properties that needs to be inserted by user.
        public override void AddProperties(Dictionary<string, PropertyHolder> i_Properties)
        {
            base.AddProperties(i_Properties);
            i_Properties.Add(k_IsCarryingHazardousMaterials, PropertyHolder.CreatePropertyForType<bool>());
            i_Properties.Add(k_MaxCarryingWeight, PropertyHolder.CreatePropertyForType<float>());
        }

        // GetDetails: gets all the details about this object properties.
        public override void GetDetails(Dictionary<string, string> i_Details)
        {
            base.GetDetails(i_Details);
            i_Details.Add(k_IsCarryingHazardousMaterials, m_IsCarryingHazardousMaterials.ToString());
            i_Details.Add(k_MaxCarryingWeight, m_MaxCarryingWeight.ToString());
        }

        // SetProperties: sets all the properties that were inserted by user.
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
    }
}