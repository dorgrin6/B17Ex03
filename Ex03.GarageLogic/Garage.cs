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
            private Owner m_Owner;

            private Vehicle m_Vehicle;

            private eVehicleStatus m_VehicleStatus;

            public VehicleInGarage(Vehicle i_Vehicle, Owner i_Owner)
            {
                m_Vehicle = i_Vehicle;
                m_Owner = i_Owner;
                m_VehicleStatus = eVehicleStatus.InRepair;
            }

            public eVehicleStatus VehicleStatus
            {
                get
                {
                    return m_VehicleStatus;
                }

                set
                {
                    m_VehicleStatus = value;
                }
            }
        }
        /*
        public void InsertVehicle(string i_RegistrationNumber, string i_OwnerName, string i_PhoneNumber, string i_VehicleType)
        {
            VehicleInGarage vehicleInGarage;
            if (TryFindVehicleByRegistration(i_RegistrationNumber, out vehicleInGarage))
            {
                vehicleInGarage.VehicleStatus = eVehicleStatus.InRepair;
                throw new ArgumentException("vehicleExists");
            }
            // vehicle not found, create new one
            Vehicle newVehicle;
            if (!VehicleFactory.TryGetNewVehicle((VehicleFactory.eVehicleType)ushort.Parse(i_VehicleType), out newVehicle))
            {
                // should we use this exception?
                throw new ArgumentException("VechicleTypeNotExists", i_VehicleType);
            }

            vehicleInGarage = new VehicleInGarage(i_OwnerName, i_PhoneNumber, newVehicle);
            m_Vehicles.Add(i_RegistrationNumber, vehicleInGarage);
        }
        */

        public bool isVehicleExistsInGarage(string i_RegistrationNumber)
        {
            return m_Vehicles.ContainsKey(i_RegistrationNumber);
        }

        public void AddVehicleToGarage(string i_RegistrationNumber, Vehicle i_Vehicle, Owner i_Owner)
        {
            VehicleInGarage newVehicle = new VehicleInGarage(i_Vehicle, i_Owner);
            m_Vehicles.Add(i_RegistrationNumber, newVehicle);
        }

        public bool TryFindVehicleByRegistration(string i_RegistrationNum, out VehicleInGarage vehicle)
        {
            bool result = false;
            vehicle = null;

            if (m_Vehicles.TryGetValue(i_RegistrationNum, out vehicle))
            {
                result = true;
            }

            return result;
        }

        public List<string> GetRegistrationNums(bool filterByStatus, eVehicleStatus status)
        {
            List<string> result = new List<string>();

            foreach (KeyValuePair<string, VehicleInGarage> pair in m_Vehicles)
            {
                VehicleInGarage currentVehicle = pair.Value;
                if (!filterByStatus || currentVehicle.VehicleStatus == status)
                {
                    result.Add(pair.Key);
                }
            }

            return result;
        }
    }
}
