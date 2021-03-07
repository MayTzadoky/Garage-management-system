using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private float m_EngineCapacity;

        public Motorcycle(string i_ModelName, string i_LicenseNumber, float i_EnergyPercentage) : base(i_ModelName, i_LicenseNumber, i_EnergyPercentage)
        {
        }

        public eLicenseType LicenseType
        {
            get
            {
                return this.m_LicenseType;
            }

            set
            {
                this.m_LicenseType = value;
            }
        }

        public float EngineCapacity
        {
            get
            {
                return this.m_EngineCapacity;
            }

            set
            {
                this.m_EngineCapacity = value;
            }
        }

        public void SetEngineCapacityFromString(string i_EngineCapacity)
        {
            if (!float.TryParse(i_EngineCapacity, out this.m_EngineCapacity))
            {
                throw new ArgumentException("Not valid");
            }
        }

        public void SetLicenseTypeFromString(string i_LicenseType)
        {
            eLicenseType[] licenseTypeOptions = (eLicenseType[])Enum.GetValues(typeof(eLicenseType));

            foreach (eLicenseType licenseType in licenseTypeOptions)
            {
                if (licenseType.ToString() == i_LicenseType)
                {
                    this.m_LicenseType = licenseType;
                    break;
                }
            }

            if(!Enum.IsDefined(typeof(eLicenseType), this.m_LicenseType))
            {
                throw new ArgumentException("Not valid option.");
            }
        }

        public override string ToString()
        {
            StringBuilder MotorcycleDetails = new StringBuilder(string.Format("The Motorcycle details are: {0}", Environment.NewLine));
            MotorcycleDetails.AppendFormat("---------------------------{0}", Environment.NewLine);
            MotorcycleDetails.AppendFormat("License type: {0}{1}", this.m_LicenseType, Environment.NewLine);
            MotorcycleDetails.AppendFormat("Engine capacity (cc): {0}{1}", this.m_EngineCapacity, Environment.NewLine);
            MotorcycleDetails.Append(base.ToString());

            return MotorcycleDetails.ToString();
        }
    }
}
