using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxEnergy) : base(i_MaxEnergy, eEngineType.Electric)
        {
        }

        public override void ChargeEnergy(float i_AddEnergy)
        {
            i_AddEnergy /= 60;
            base.ChargeEnergy(i_AddEnergy);
        }

        public override void GetDetails(Dictionary<string, string> i_Details)
        {
            base.GetDetails(i_Details);
        }
    }
}
