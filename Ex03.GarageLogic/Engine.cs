using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected readonly float r_MaxEnergy;
        protected float m_CurrentEnergy;

        public Engine(float i_CurrentEnergyPercentage, float i_MaxEnergy)
        {
            r_MaxEnergy = i_MaxEnergy;
            float currentEnergy = ConvertsThePercentageOfEnergyToCurrentAmount(i_CurrentEnergyPercentage, i_MaxEnergy);

            if (currentEnergy <= i_MaxEnergy && currentEnergy >= 0)
            {
                m_CurrentEnergy = currentEnergy;
            }
            else
            {
                throw new ValueOutOfRangeException(r_MaxEnergy - currentEnergy, 0);
            }
        }

        public float ConvertsThePercentageOfEnergyToCurrentAmount(float i_CurrentEnergyPercentage, float i_MaximumValue)
        {
            return i_MaximumValue * i_CurrentEnergyPercentage / 100;
        }

        public float MaxEnergy
        {
            get
            {
                return r_MaxEnergy;
            }
        }

        public float CurrentEnergy
        {
            get
            {
                return m_CurrentEnergy;
            }

            set
            {
                m_CurrentEnergy = value;
            }
        }
    }
}
