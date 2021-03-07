using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_ManufectureName;
        private readonly float r_MaxAirPressureByManufecture;
        private float m_CurrentAirPressure;

        public Wheel(string i_ManufectureName, float i_CurrentAirPressure, float i_MaxAirPressureByManufecture)
        {
            r_ManufectureName = i_ManufectureName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaxAirPressureByManufecture = i_MaxAirPressureByManufecture;
        }

        public void ToInflate(float i_AirToAdd)
        {
            if (m_CurrentAirPressure + i_AirToAdd <= r_MaxAirPressureByManufecture)
            {
                m_CurrentAirPressure += i_AirToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(r_MaxAirPressureByManufecture, 0.0f);
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressureByManufecture
        {
            get
            {
                return r_MaxAirPressureByManufecture;
            }
        }

        public override string ToString()
        {
            StringBuilder wheelDetails = new StringBuilder();

            wheelDetails.AppendFormat("Wheels Manufacturer: {0}{1}", r_ManufectureName, Environment.NewLine);
            wheelDetails.AppendFormat("Current Air Pressure: {0}{1}", m_CurrentAirPressure, Environment.NewLine);
            wheelDetails.AppendFormat("Maximum Air Pressure: {0}{1}{1}", r_MaxAirPressureByManufecture, Environment.NewLine);
            return wheelDetails.ToString();
        }
    }
}
