using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsDeliverDangerousMaterial;
        private float m_CargoCapacity;

        public Truck(string i_ModelName, string i_LicenseNumber, float i_EnergyPercentage) : base(i_ModelName, i_LicenseNumber, i_EnergyPercentage)
        {
        }

        public bool IsDeliverDangerousMatirial
        {
            get
            {
                return this.m_IsDeliverDangerousMaterial;
            }

            set
            {
                this.m_IsDeliverDangerousMaterial = value;
            }
        }

        public float CargoCapacity
        {
            get
            {
                return this.m_CargoCapacity;
            }

            set
            {
                this.m_CargoCapacity = value;
            }
        }

        public void SetIsDeliverDangerousMaterialFromString(string i_IsDeliverDangerousMaterial)
        {
            bool isCanDeliver;
            if(bool.TryParse(i_IsDeliverDangerousMaterial, out isCanDeliver))
            {
                this.m_IsDeliverDangerousMaterial = isCanDeliver;
            }
            else
            {
                throw new ArgumentException("Can only be true of false");
            }
        }

        public void SetCargoCapacityFromString(string i_CargoCapacity)
        {
            if (!float.TryParse(i_CargoCapacity, out this.m_CargoCapacity))
            {
                throw new ArgumentException("Not valid");
            }
        }

        public override string ToString()
        {
            string isDeliverDangerousmaterials = this.m_IsDeliverDangerousMaterial ? "yes" : "no";

            StringBuilder TruckDetails = new StringBuilder(string.Format("Truck The details are: {0}", Environment.NewLine));
            TruckDetails.AppendFormat("----------------------{0}", Environment.NewLine);
            TruckDetails.AppendFormat("Is Delivering Dangerous materials: {0}{1}", isDeliverDangerousmaterials, Environment.NewLine);
            TruckDetails.AppendFormat("Cargo Capacity: {0}{1}", this.m_CargoCapacity, Environment.NewLine);
            TruckDetails.Append(base.ToString());

            return TruckDetails.ToString();
        }
    }
}
