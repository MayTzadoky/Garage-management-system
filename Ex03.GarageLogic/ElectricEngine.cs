using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_EnergyPercentage, float i_MaxCumulativeTimePerHour) : base(i_EnergyPercentage, i_MaxCumulativeTimePerHour)
        {
        }

        public void ToChargeBettery(float i_numberOfMinutesToAdd)
        {
            float numberOfHoursToAdd = i_numberOfMinutesToAdd / 60;

            if (numberOfHoursToAdd + this.m_CurrentEnergy <= this.r_MaxEnergy && numberOfHoursToAdd >= 0)
            {
                this.m_CurrentEnergy += numberOfHoursToAdd;
            }
            else
            {
                float maxEnergyToInsertInMinutes = (this.r_MaxEnergy - this.m_CurrentEnergy) * 60;
                throw new ValueOutOfRangeException(maxEnergyToInsertInMinutes, 0);
            }
        }

        public override string ToString()
        {
            StringBuilder ElectricEngineDetails = new StringBuilder(string.Format("The Electric Engine details are:{0}", Environment.NewLine));
            ElectricEngineDetails.AppendFormat("--------------------------------{0}", Environment.NewLine);
            ElectricEngineDetails.AppendFormat("Cumulative time left (per hour): {0}{1}", this.m_CurrentEnergy, Environment.NewLine);
            ElectricEngineDetails.AppendFormat("Maximum cumulative time (per hour): {0}{1}", this.r_MaxEnergy, Environment.NewLine);

            return ElectricEngineDetails.ToString();
        }
    }
}
