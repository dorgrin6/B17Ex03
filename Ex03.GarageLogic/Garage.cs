namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;

    public class Garage
    {
        public const string k_Filter = "filer method";

        private Dictionary<string, VehicleInGarage> m_Vehicles = new Dictionary<string, VehicleInGarage>();

        public enum eVehicleFilter
        {
            All = 1,

            ByStatus
        }

        public enum eVehicleStatus
        {
            InRepair = 1,

            Repaired,

            Paid
        }

        // AddVehicle: add a vehicle to the garage.
        public void AddVehicle(string i_RegistrationNumber, Vehicle i_Vehicle, Owner i_Owner)
        {
            VehicleInGarage newVehicle = new VehicleInGarage(i_Vehicle, i_Owner);
            m_Vehicles.Add(i_RegistrationNumber, newVehicle);
        }

        // ChargeEnergy: charges vehicle's engine's energy. gets params as input: Engine type, Amount to charge, Type of fuel.
        public void ChargeEnergy(string i_RegistrationNumber, params string[] i_Params)
        {
            Vehicle vehicle = m_Vehicles[i_RegistrationNumber].Vehicle;
            Engine.eEngineType vehicleEngineType = vehicle.VehicleEngine.EngineType;

            if (vehicleEngineType.ToString() == i_Params[0])
            {
                vehicle.ChargeEnergy(i_Params);
            }
            else
            {
                throw new ArgumentException(Engine.k_WrongFuel);
            }
        }

        // GetAllRegistrationNumbers: gets all the vehicle's registration numbers, no matter their vehicle's status.
        public List<string> GetAllRegistrationNumbers()
        {
            return getRegistrationNumbers(eVehicleFilter.All, default(eVehicleStatus));
        }

        // GetRegistrationNumbersByStatus: gets all the vehicle's registration numbers who fits the input vehicle status.
        public List<string> GetRegistrationNumbersByStatus(eVehicleStatus i_Status)
        {
            return getRegistrationNumbers(eVehicleFilter.ByStatus, i_Status);
        }

        // GetVehicle: gets a vehicle from the garage, by its registration number.
        public VehicleInGarage GetVehicle(string i_RegistrationNumber)
        {
            return m_Vehicles[i_RegistrationNumber];
        }

        // GetVehicleDetails: gets all the details about a vehicle & owner properties.
        public void GetVehicleDetails(string i_RegistrationNumber, Dictionary<string, string> i_Details)
        {
            m_Vehicles[i_RegistrationNumber].Vehicle.GetDetails(i_Details);
            m_Vehicles[i_RegistrationNumber].Owner.GetDetails(i_Details);
            i_Details.Add(VehicleInGarage.k_VehicleStatus, m_Vehicles[i_RegistrationNumber].VehicleStatus.ToString());
        }

        // InflateVehicleWheels: inflates the wheels of a vehicle to their maximum amount of air.
        public void InflateVehicleWheels(string i_RegistrationNumber)
        {
            m_Vehicles[i_RegistrationNumber].Vehicle.InflateWheels();
        }

        // isVehicleExistsInGarage: return True if the vehicle is in garage, False else.
        public bool isVehicleExistsInGarage(string i_RegistrationNumber)
        {
            return m_Vehicles.ContainsKey(i_RegistrationNumber);
        }

        // getRegistrationNumbers: gets all the vehicle's registration numbers who fits the input vehicle status.
        private List<string> getRegistrationNumbers(eVehicleFilter i_Filter, eVehicleStatus i_Status)
        {
            List<string> result = new List<string>();
            foreach (string registration in m_Vehicles.Keys)
            {
                if ((i_Filter == eVehicleFilter.All)
                    || (i_Filter == eVehicleFilter.ByStatus && m_Vehicles[registration].VehicleStatus == i_Status))
                {
                    result.Add(registration);
                }
            }

            return result;
        }

        public class VehicleInGarage
        {
            public const string k_VehicleStatus = "vehicle's status in garage";

            private Owner m_Owner;

            private Vehicle m_Vehicle;

            private eVehicleStatus m_VehicleStatus;

            public VehicleInGarage(Vehicle i_Vehicle, Owner i_Owner)
            {
                m_Vehicle = i_Vehicle;
                m_Owner = i_Owner;
                m_VehicleStatus = eVehicleStatus.InRepair;
            }

            public Owner Owner
            {
                get
                {
                    return m_Owner;
                }
            }

            public Vehicle Vehicle
            {
                get
                {
                    return m_Vehicle;
                }
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
    }
}