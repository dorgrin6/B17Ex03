namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;

    public class VehicleFactory
    {
        public enum eVehicleType
        {
            GasMotorcycle = 1,
            ElectricMotorcycle,
            GasCar,
            ElectricCar,
            Truck
        }

        public static Vehicle GetVehicle(eVehicleType i_VehicleType)
        {
            Vehicle vehicle = null;
            
            switch (i_VehicleType)
            {
                case eVehicleType.GasMotorcycle:
                    vehicle = new Motorcycle(2, 33, new GasEngine(GasEngine.eFuelType.Octan95, 5.5f));
                    break;
                case eVehicleType.ElectricMotorcycle:
                    vehicle = new Motorcycle(2, 33, new ElectricEngine(2.7f));
                    break;
                case eVehicleType.GasCar:
                    vehicle = new Car(4, 30, new GasEngine(GasEngine.eFuelType.Octan98, 42));
                    break;
                case eVehicleType.ElectricCar:
                    vehicle = new Car(4, 30, new ElectricEngine(2.5f));
                    break;

                case eVehicleType.Truck:
                    vehicle = new Truck(12, 42, new GasEngine(GasEngine.eFuelType.Octan96, 135));
                    break;
                default:
                    throw new ArgumentException(string.Format("Vehicle {0} isn't supported!", i_VehicleType));
            }

            return vehicle;
        }
    }
}
