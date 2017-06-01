namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {

        public ElectricEngine() : base(i_EngineType)
        {
            
        }

        public override void RenewEnergy(float i_Amount)
        {
            Charge(i_Amount);
        }

        private void Charge(float i_ChargeAmount)
        {
            if (m_BatteryLeft + i_ChargeAmount <= m_MaxBatteryLife)
            {
                m_BatteryLeft += i_ChargeAmount;
            }

            else
            {
                // throw exception    
            }
        }
    }
}
