using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private readonly eFuelType m_FuelType;
        
        public FuelEngine(float i_EnergyPercentage, float i_MaxFuelAmount, eFuelType i_FuelType) : base(i_EnergyPercentage, i_MaxFuelAmount)
        {
            this.m_FuelType = i_FuelType;
        }

        public void ToFuel(float i_LitersToAdd, eFuelType i_FuelTypeToAdd)
        { 
            if (i_LitersToAdd + this.m_CurrentEnergy <= this.r_MaxEnergy && i_LitersToAdd >= 0 && this.m_FuelType == i_FuelTypeToAdd)
            {
                this.m_CurrentEnergy += i_LitersToAdd;
            }
            else if(i_LitersToAdd + this.m_CurrentEnergy > this.r_MaxEnergy || i_LitersToAdd < 0)
            {
                throw new ValueOutOfRangeException(r_MaxEnergy - m_CurrentEnergy, 0);
            }
            else if(this.m_FuelType != i_FuelTypeToAdd)
            {
                throw new ArgumentException("Wrong fuel type");
            }
        }

        public override string ToString()
        {
            StringBuilder FuelEngineDetails = new StringBuilder(string.Format("The Fuel Engine details are: {0}", Environment.NewLine));
            FuelEngineDetails.AppendFormat("----------------------------{0}", Environment.NewLine);
            FuelEngineDetails.AppendFormat("Fuel type: {0}{1}", this.m_FuelType, Environment.NewLine);
            FuelEngineDetails.AppendFormat("Current fuel quantity: {0}{1}", this.m_CurrentEnergy, Environment.NewLine);
            FuelEngineDetails.AppendFormat("Maximum fuel quantity: {0}{1}", this.r_MaxEnergy, Environment.NewLine);

            return FuelEngineDetails.ToString();
        }
    }
}
