using System.Collections.Generic;
using System;
namespace Ex03.GarageLogic
{
    public class Garage
    {

        internal enum e_VehicleStatus
        {
            inRepair,
            Repaired,
            Paid
        }

        public struct VehicleInGarage
        {
            string ownerName;
            string ownerPhoneNumber;
            e_VehicleStatus vehicleStatus;
            Vehicle vehicle;
        }

        Dictionary<string, VehicleInGarage> vehicles;

        public bool isVehicleExist(string i_RegistrationNum)
        {
            return vehicles.ContainsKey(i_RegistrationNum);
        }

        


    }
}
