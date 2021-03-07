using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected readonly List<Wheel> r_WheelsCollection;
        protected readonly string r_LicenseNumber;
        protected string m_ModelName;
        protected float m_EnergyPercentage;
        protected Engine m_Engine;

        public Vehicle(string i_ModelName, string i_LicenseNumber, float i_EnergyPercentage)
        {
            m_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            m_EnergyPercentage = i_EnergyPercentage;
            r_WheelsCollection = new List<Wheel>();
            m_Engine = null;
        }

        public static bool operator ==(Vehicle vehicle1, Vehicle vehicle2)
        {
            return vehicle1.r_LicenseNumber.Equals(vehicle2.r_LicenseNumber);
        }

        public static bool operator !=(Vehicle vehicle1, Vehicle vehicle2)
        {
            return !(vehicle1 == vehicle2);
        }

        public override int GetHashCode()
        {
            return this.r_LicenseNumber.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            Vehicle toCommpareTo = obj as Vehicle;

            if (toCommpareTo != null)
            {
                isEqual = r_LicenseNumber.Equals(toCommpareTo);
            }

            return isEqual;
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
        }

        public float EnergyPercentage
        {
            get
            {
                return m_EnergyPercentage;
            }

            set
            {
                m_EnergyPercentage = value;
            }
        }

        public List<Wheel> WheelsCollection
        {
            get
            {
                return r_WheelsCollection;
            }
        }

        public Engine Engine
        {
            get
            {
                return m_Engine;
            }

            set
            {
                m_Engine = value;
            }
        }

        public override string ToString()
        {
            StringBuilder vehicleGeneralDetails = new StringBuilder();
            vehicleGeneralDetails.AppendFormat("License Number: {0}{1}", r_LicenseNumber, Environment.NewLine);
            vehicleGeneralDetails.AppendFormat("Model Name: {0}{1}", m_ModelName, Environment.NewLine);
            vehicleGeneralDetails.AppendFormat("Number of Wheels: {0}{1}", r_WheelsCollection.Count, Environment.NewLine);
            vehicleGeneralDetails.AppendFormat(r_WheelsCollection[0].ToString());
            vehicleGeneralDetails.AppendFormat(m_Engine.ToString());

            return vehicleGeneralDetails.ToString();
        }
    }
}
