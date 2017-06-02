namespace Ex03.GarageLogic
{
    public class ElectricEngine : Vehicle.Engine
    {
        public ElectricEngine(float i_MaxEnergy) : base(i_MaxEnergy)
        {
        }

        public void Charge(float i_ChargeAmount)
        {
            if (m_CurrentEnergy + i_ChargeAmount <= m_MaxEnergy)
            {
                m_CurrentEnergy += i_ChargeAmount;
            }

            else
            {
              //throw new ValueOutOfRangeException(0,this.m_MaxEnergy,"");    
            }
        }
    }
}
