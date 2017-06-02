using System.Collections.Generic;
using System;
namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleInGarage> m_Vehicles = new Dictionary<string, VehicleInGarage>();

        public enum eVehicleStatus
        {
            InRepair,
            Repaired,
            Paid
        }

        public class VehicleInGarage
        {
            public string m_OwnerName;

            public string m_OwnerPhoneNumber;

            public eVehicleStatus m_VehicleStatus;

            public Vehicle m_Vehicle;

            public VehicleInGarage(string i_OwnerName, string i_PhoneNum, Vehicle i_Vehicle)
            {
                this.m_OwnerName = i_OwnerName;
                this.m_OwnerPhoneNumber = i_PhoneNum;
                this.m_Vehicle = i_Vehicle;
                this.m_VehicleStatus = eVehicleStatus.InRepair;
            }
        }

        public void InsertVehicle(string i_RegistrationNumber, string i_OwnerName, string i_PhoneNumber, string i_VehicleType)
        {
            VehicleInGarage vehicleInGarage;
            try
            {
                if (!isVehicleExist(i_RegistrationNumber))
                {
                    throw new ArgumentException(
                        string.Format("Vehicle with registration #{0} already exists", i_RegistrationNumber),
                        i_RegistrationNumber);
                }

                // vehicle not found, create new one
                Vehicle newVehicle;
                if (!VehicleFactory.TryGetVehicle(i_VehicleType, out newVehicle))
                {
                    // should we use this exception?
                    throw new ValueOutOfRangeException(
                        (float)VehicleFactory.eVehicleType.LowerBound,
                        (float)VehicleFactory.eVehicleType.UpperBound,
                        string.Format("Vehicle type {0} doesn't exist", i_VehicleType));
                }

                vehicleInGarage = new Garage.VehicleInGarage(i_OwnerName, i_PhoneNumber, newVehicle);
                m_Vehicles.Add(i_RegistrationNumber, vehicleInGarage);
            }
            catch (ArgumentException i_Except)
            {
                // assuming vehicleInGarage was found
                vehicleInGarage = m_Vehicles[i_RegistrationNumber];
                vehicleInGarage.m_VehicleStatus = Garage.eVehicleStatus.InRepair;
            }
        }

        private bool isVehicleExist(string i_RegistrationNum)
        {
            return m_Vehicles.ContainsKey(i_RegistrationNum);
        }
    }
}
