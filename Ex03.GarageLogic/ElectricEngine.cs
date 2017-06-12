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
    }
}
