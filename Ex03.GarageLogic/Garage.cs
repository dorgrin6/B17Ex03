using System.Collections.Generic;
using System;
namespace Ex03.GarageLogic

{
    public class Garage
    {
        private Dictionary<string, VehicleInGarage> m_Vehicles = new Dictionary<string, VehicleInGarage>();

        public enum eVehicleStatus
        {
            InRepair = 1,
            Repaired,
            Paid
        }

        public enum eVehicleFilter
        {
            All = 1,
            ByStatus
        }

        public class VehicleInGarage
        {
            public const string k_VehicleStatus = "status in garage";

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

            public Vehicle Vehicle
            {
                get
                {
                    return m_Vehicle;
                }
            }

            public Owner Owner
            {
                get
                {
                    return m_Owner;
                }
            }
        }

        public bool isVehicleExistsInGarage(string i_RegistrationNumber)
        {
            return m_Vehicles.ContainsKey(i_RegistrationNumber);
        }

        public void AddVehicle(string i_RegistrationNumber, Vehicle i_Vehicle, Owner i_Owner)
        {
            VehicleInGarage newVehicle = new VehicleInGarage(i_Vehicle, i_Owner);
            m_Vehicles.Add(i_RegistrationNumber, newVehicle);
        }

        public VehicleInGarage GetVehicle(string i_RegistrationNumber)
        {
            return m_Vehicles[i_RegistrationNumber];
        }

        public List<string> GetAllRegistrationNumbers()
        {
            return GetRegistrationNumbers(eVehicleFilter.All, default(eVehicleStatus));
        }

        public List<string> GetRegistrationNumbersByStatus(eVehicleStatus i_Status)
        {
            return GetRegistrationNumbers(eVehicleFilter.ByStatus, i_Status);
        }

        private List<string> GetRegistrationNumbers(eVehicleFilter i_Filter, eVehicleStatus i_Status)
        {
            List<string> result = new List<string>();
            foreach (string registration in m_Vehicles.Keys)
            {
                if ((i_Filter == eVehicleFilter.All) || (i_Filter == eVehicleFilter.ByStatus && m_Vehicles[registration].VehicleStatus == i_Status))
                {
                    result.Add(registration);
                }

            }
            return result;
        }

        public void InflateVehicleWheels(string i_RegistrationNumber)
        {
            m_Vehicles[i_RegistrationNumber].Vehicle.InflateWheels();

        }

        public void fuelGasVehicle(string i_RegistrationNumber, float i_AddCharge, GasEngine.eFuelType i_FuelType)
        {
            checkLegalFuelOrCharge(i_RegistrationNumber, Engine.eEngineType.Gas);
            m_Vehicles[i_RegistrationNumber].Vehicle.VehicleEngine.RefuelGas(i_AddCharge, i_FuelType);
        }

        public void chargeElectricVehicle(string i_RegistrationNumber, float i_AddCharge)
        {
            checkLegalFuelOrCharge(i_RegistrationNumber, Engine.eEngineType.Electric);
            m_Vehicles[i_RegistrationNumber].Vehicle.VehicleEngine.ChargeEnergy(i_AddCharge);
        }

        public void checkLegalFuelOrCharge(string i_RegistrationNumber, Engine.eEngineType i_WantedEngine)
        {
            Engine.eEngineType vehicleEngine = m_Vehicles[i_RegistrationNumber].Vehicle.VehicleEngine.EngineType;
            if (vehicleEngine != i_WantedEngine)
            {
                throw new ArgumentException();
            }
        }

        public void GetVehicleDetails(string i_RegistrationNumber, Dictionary<string,string> i_Details)
        {
            m_Vehicles[i_RegistrationNumber].Vehicle.GetDetails(i_Details);
            m_Vehicles[i_RegistrationNumber].Owner.GetDetails(i_Details);
            i_Details.Add(VehicleInGarage.k_VehicleStatus, m_Vehicles[i_RegistrationNumber].VehicleStatus.ToString());
        }
        /*
        public bool TryGetVehicle(string i_RegistrationNum, out VehicleInGarage vehicle)
        {
            bool result = false;
            vehicle = null;

            if (m_Vehicles.TryGetValue(i_RegistrationNum, out vehicle))
            {
                result = true;
            }

            return result;
        }
        */
    }
}
