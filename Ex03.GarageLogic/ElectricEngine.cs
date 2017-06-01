namespace Ex03.GarageLogic
{
    public class ElectricEngine : Vehicle.Engine
    {
        public ElectricEngine(float i_MaxEnergy, float i_CurrentEnergy) : base(i_MaxEnergy, i_CurrentEnergy)
        {
        }

        public void Charge(float i_ChargeAmount)
        {
            if (m_CurrentEnergy + i_ChargeAmount <= m_MaxEnergy)
            {
                this.m_CurrentEnergy += i_ChargeAmount;
            }

            else
            {
              //throw new ValueOutOfRangeException(0,this.m_MaxEnergy,"");    
            }
        }
    }
}
