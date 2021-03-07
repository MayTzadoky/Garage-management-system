using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eColor m_CarColor;
        private eDoorNumber m_NumberOfDoors;

        public Car(string i_ModelName, string i_LicenseNumber, float i_EnergyPercentage) : base(i_ModelName, i_LicenseNumber, i_EnergyPercentage)
        {
        }

        public eColor CarColor
        {
            get
            {
                return this.m_CarColor;
            }

            set
            {
                this.m_CarColor = value;
            }
        }

        public eDoorNumber NumberOfDoors
        {
            get
            {
                return this.m_NumberOfDoors;
            }

            set
            {
                this.m_NumberOfDoors = value;
            }
        }

        public void SetCarColorFromString(string i_Color)
        {
            eColor[] colors = (eColor[])Enum.GetValues(typeof(eColor));

            foreach(eColor color in colors)
            {
                if(color.ToString() == i_Color)
                {
                    this.m_CarColor = color;
                    break;
                }
            }

            if (!Enum.IsDefined(typeof(eColor), this.m_CarColor))
            {
                throw new ArgumentException("Not valid option.");
            }
        }

        public void SetNumberOfDoorsFromString(string i_NumOfDoor)
        {
            eDoorNumber[] NumOfDoorOptions = (eDoorNumber[])Enum.GetValues(typeof(eDoorNumber));

            foreach (eDoorNumber numOfDoor in NumOfDoorOptions)
            {
                if (numOfDoor.ToString() == i_NumOfDoor)
                {
                    this.m_NumberOfDoors = numOfDoor;
                    break;
                }
            }

            if (!Enum.IsDefined(typeof(eDoorNumber), this.m_NumberOfDoors))
            {
                throw new ArgumentException("Not valid option.");
            }
        }

        public override string ToString()
        {
            StringBuilder carDetails = new StringBuilder(string.Format("The car details are: {0}", Environment.NewLine));
            carDetails.AppendFormat("--------------------{0}", Environment.NewLine);
            carDetails.AppendFormat("Color: {0}{1}", this.m_CarColor, Environment.NewLine);
            carDetails.AppendFormat("Number Of Doors: {0}{1}", this.m_NumberOfDoors, Environment.NewLine);
            carDetails.Append(base.ToString());

            return carDetails.ToString();
        }
    }
}
