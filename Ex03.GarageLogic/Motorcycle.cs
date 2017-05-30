namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public Motorcycle(string i_ModelName, string i_RegistrationNum, float i_EneregyPercentageLeft, ushort i_WheelsAmount, Engine.eEngineType i_EngineType)
            : base(i_ModelName, i_RegistrationNum, i_EneregyPercentageLeft, i_WheelsAmount, i_EngineType)
        {
        }
    }
}
