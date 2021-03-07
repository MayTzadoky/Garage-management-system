using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, GarageRecord> r_Records;

        public Garage()
        {
            r_Records = new Dictionary<string, GarageRecord>();
        }

        public void PutVehicleInGarage(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_vehicleToGarage)
        {
            GarageRecord isVehicleInGarage = GetRecordByLicenseNumber(i_vehicleToGarage.LicenseNumber);

            if (isVehicleInGarage == null)
            {
                GarageRecord newRecord = new GarageRecord(i_OwnerName, i_OwnerPhoneNumber, i_vehicleToGarage);
                r_Records.Add(i_vehicleToGarage.LicenseNumber, newRecord);
            }
            else
            {
                isVehicleInGarage.VehicleStatusInGarage = eVehicleStatusInGarage.InRepair;
                throw new ArgumentException("Vehicle already in garage. Status change to in repair.");
            }
        }

        public List<string> ListOfLicenseNumbersByStatus(eVehicleStatusInGarage i_StatusInGarage)
        {
            List<string> licenseNumbersByStatus = new List<string>();

            foreach (string licenseNumber in r_Records.Keys)
            {
                if (r_Records[licenseNumber].VehicleStatusInGarage == i_StatusInGarage)
                {
                    licenseNumbersByStatus.Add(licenseNumber);
                }
            }

            return licenseNumbersByStatus;
        }

        public List<string> ListOfAllLicenseNumbersInGarage()
        {
            List<string> licenseNumbers = new List<string>();

            foreach (string licenseNumber in r_Records.Keys)
            {
                licenseNumbers.Add(licenseNumber);
            }

            return licenseNumbers;
        }

        public bool IsVehicleInGarage(string i_VehicleLicenseNumber)
        {
            bool isVehicleInGarage = false;
            GarageRecord vehicle = GetRecordByLicenseNumber(i_VehicleLicenseNumber);

            if (vehicle != null)
            {
                isVehicleInGarage = true;
            }

            return isVehicleInGarage;
        }

        public GarageRecord GetRecordByLicenseNumber(string i_VehicleLicenseNumber)
        {
            GarageRecord garageRecord;

            try
            {
                garageRecord = r_Records[i_VehicleLicenseNumber];
            }
            catch(KeyNotFoundException)
            {
                garageRecord = null;
            }

            return garageRecord;
        }

        public void ChangeVehiclStatus(string i_LicenseNumber, eVehicleStatusInGarage i_NewVehicleStatus)
        {
            if(IsVehicleInGarage(i_LicenseNumber))
            {
                r_Records[i_LicenseNumber].VehicleStatusInGarage = i_NewVehicleStatus;
            }
            else
            {
                throw new ArgumentException("Vehicle is not in garage, nothing to do.");
            }
        }

        public void InflateAirPressureToMax(string i_LicenseNumber)
        {
            if (!IsVehicleInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle is not in garage, nothing to do.");
            }

            Vehicle vehicle = r_Records[i_LicenseNumber].VehicleInGarage;
            List<Wheel> wheels = vehicle.WheelsCollection;

            float QuantityForInflation = wheels[0].MaxAirPressureByManufecture - wheels[0].CurrentAirPressure;

            foreach(Wheel wheel in wheels)
            {
                wheel.ToInflate(QuantityForInflation);
            }
        }

        public void FuelVehicle(string i_LicenseNumber, float i_LitersToAdd, eFuelType i_FuelTypeToAdd)
        {
            if (!IsVehicleInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle is not in garage, nothing to do.");
            }

            if (r_Records[i_LicenseNumber].VehicleInGarage.Engine is FuelEngine)
            {
                FuelEngine engine = r_Records[i_LicenseNumber].VehicleInGarage.Engine as FuelEngine;

                engine.ToFuel(i_LitersToAdd, i_FuelTypeToAdd);
                r_Records[i_LicenseNumber].VehicleInGarage.EnergyPercentage = engine.CurrentEnergy / engine.MaxEnergy * 100;
            }
            else
            {
                throw new ArgumentException("The type of vehicle can not be fueled.");
            }
        }

        public void ChargeElectricVehicle(string i_LicenseNumber, float i_numberOfMinutesToAdd)
        {
            if (!IsVehicleInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle is not in garage, nothing to do.");
            }

            if (r_Records[i_LicenseNumber].VehicleInGarage.Engine is ElectricEngine)
            {
                ElectricEngine engine = r_Records[i_LicenseNumber].VehicleInGarage.Engine as ElectricEngine;

                engine.ToChargeBettery(i_numberOfMinutesToAdd);
                r_Records[i_LicenseNumber].VehicleInGarage.EnergyPercentage = engine.CurrentEnergy / engine.MaxEnergy * 100;
            }
            else
            {
                throw new ArgumentException("This type of vehicle can not be charged");
            }    
        }

        public string VehicleDataAccordingLicenseNumberToString(string i_LicenseNumber)
        {
            if (!IsVehicleInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle is not in garage, nothing to do.");
            }

            StringBuilder RecordDetails = new StringBuilder();
            RecordDetails.Append(r_Records[i_LicenseNumber].ToString());

            return RecordDetails.ToString();
        }
    }
}
