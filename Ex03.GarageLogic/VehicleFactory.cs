namespace Ex03.GarageLogic
{

    public class VehicleFactory
    {
        public enum eTypeOfVehicle
        {
            GasCar,
            ElectricCar,
            GasMotorcycle,
            ElectricMotorcycle,
            Truck
        }

        public Vehicle GetVehicle(eTypeOfVehicle i_vehicleType)
        {
            Vehicle vehicle = null;
            
            switch (i_vehicleType)
            {
                case eTypeOfVehicle.GasCar:
                    vehicle = new Car(4, 30, new Vehicle(Vehicle.GasEngine.eTypeOfFuel.Octan98, 42)); //get as parameters: number of wheels 4, maxAirPresuure 30, GasEngine(Octan98, max 42 fuel)
                    break;
                case eTypeOfVehicle.ElectricCar:
                    vehicle = new Car(); //get as parameters: number of wheels 4, maxAirPresuure 30, ElectricEngine(max 2.5 hours)
                    break;
                case eTypeOfVehicle.GasMotorcycle:
                    vehicle = new Motorcycle(); //get as parameters: number of wheels 2, maxAirPressure 33, GasEngine(Octan95, max 5.5 fuel)
                    break;
                case eTypeOfVehicle.ElectricMotorcycle:
                    vehicle = new Motorcycle(); //get as parameters: number of wheels 2, maxAirPressure 33, ElectricEngine(max 2.7 hours)
                    break;
                case eTypeOfVehicle.Truck:
                    vehicle = new Truck(); //get as parameters: number of wheels 12, maxAirPressure 32, GasEngine(Octan96, max 135 fuel)
                    break;
                default:
                    // throw notSupportedException
                    break;
            }

            return vehicle;
        }
    }
}
