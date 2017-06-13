using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxEnergy) : base(i_MaxEnergy, eEngineType.Electric)
        {
        }

        public override void ChargeEnergy(params string[] i_Params)
        {
            float addEnergy = float.Parse(i_Params[1]);
            addEnergy /= 60;
            i_Params[1] = addEnergy.ToString();
            base.ChargeEnergy(i_Params);
        }

        public override void GetDetails(Dictionary<string, string> i_Details)
        {
            base.GetDetails(i_Details);
        }
    }
}
