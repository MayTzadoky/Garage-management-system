using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageRecord
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleStatusInGarage m_VehicleStatusInGarage;
        private Vehicle m_Vehicle;

        public GarageRecord(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_VehicleStatusInGarage = eVehicleStatusInGarage.InRepair;
            m_Vehicle = i_Vehicle;
        }

        public eVehicleStatusInGarage VehicleStatusInGarage
        {
            get
            {
                return m_VehicleStatusInGarage;
            }

            set
            {
                m_VehicleStatusInGarage = value;
            }
        }

        public Vehicle VehicleInGarage
        {
            get
            {
                return m_Vehicle;
            }

            set
            {
                m_Vehicle = value;
            }
        }

        public static bool operator ==(GarageRecord record1, GarageRecord record2)
        {
            bool isEqual = false;

            if (record1 is null && record2 is null)
            {
                isEqual = true;
            }
            else if (record1 is null || record2 is null)
            {
                isEqual = false;
            }
            else
            {
                isEqual = record1.m_Vehicle.LicenseNumber == record2.m_Vehicle.LicenseNumber;
            }

            return isEqual;
        }

        public static bool operator !=(GarageRecord record1, GarageRecord record2)
        {
            bool isEqual = !(record1 == record2);
            return isEqual;
        }

        public override bool Equals(object record1)
        {
            GarageRecord record = record1 as GarageRecord;

            return this == record;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder RecordDetails = new StringBuilder(string.Format("Record Details:{0}", Environment.NewLine));
            RecordDetails.AppendFormat("==============={0}", Environment.NewLine);
            RecordDetails.AppendFormat("Owner Name: {0}{1}", m_OwnerName, Environment.NewLine);
            RecordDetails.AppendFormat("Owner Phone Number: {0}{1}", m_OwnerPhoneNumber, Environment.NewLine);
            RecordDetails.AppendFormat("Vehicle Status In Garage: {0}{1}{1}", m_VehicleStatusInGarage, Environment.NewLine);
            RecordDetails.Append(m_Vehicle.ToString());
            RecordDetails.AppendFormat("==============={0}", Environment.NewLine);

            return RecordDetails.ToString();
        }
    }
}
