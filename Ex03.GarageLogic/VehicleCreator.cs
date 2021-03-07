using System;
using System.Reflection;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public static class VehicleCreator
    {
        private const float k_MaxCumulativeTimePerHourForElectricCar = 4.8f;
        private const float k_MaxCumulativeTimePerHourForElectricMotorcycle = 1.6f;
        private const float k_MaxFuelAmountForFuelCar = 50f;
        private const float k_MaxFuelAmountForFuelMotorcycle = 5.5f;
        private const float k_MaxFuelAmountForFuelTruck = 105f;
        private const float k_MaxAirPressureForCar = 32f;
        private const float k_MaxAirPressureForMotorcycle = 28f;
        private const float k_MaxAirPressureForTruck = 30f;
        private const int k_NumberOfWheelsInCar = 4;
        private const int k_NumberOfWheelsInMotorcycle = 2;
        private const int k_NumberOfWheelsInTruck = 16;
        private const eFuelType k_FuelTypeForCar = eFuelType.Octan96;
        private const eFuelType k_FuelTypeForMotorcycle = eFuelType.Octan95;
        private const eFuelType k_FuelTypeForTruck = eFuelType.Soler;

        public static Vehicle CreateVehicle(string i_LicenseNumber, eVehicleType i_VehicleType, string i_ModelName, float i_EnergyPercentage)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                case eVehicleType.FuelCar:
                    newVehicle = new Car(i_ModelName, i_LicenseNumber, i_EnergyPercentage);
                    break;
                case eVehicleType.ElectricMotorcycle:
                case eVehicleType.FuelMotorcycle:
                    newVehicle = new Motorcycle(i_ModelName, i_LicenseNumber, i_EnergyPercentage);
                    break;
                case eVehicleType.FuelTruck:
                    newVehicle = new Truck(i_ModelName, i_LicenseNumber, i_EnergyPercentage);
                    break;
                default:
                    throw new FormatException("Invalid Vehicle Type");
            }

            CreateEngine(i_VehicleType, i_EnergyPercentage, ref newVehicle);

            return newVehicle;
        }

        private static void CreateEngine(eVehicleType i_VehicleType, float i_EnergyPercentage, ref Vehicle io_Vehicle)
        {
            Engine engine = null;

            switch (i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                    engine = new ElectricEngine(i_EnergyPercentage, k_MaxCumulativeTimePerHourForElectricCar);
                    break;
                case eVehicleType.FuelCar:
                    engine = new FuelEngine(i_EnergyPercentage, k_MaxFuelAmountForFuelCar, k_FuelTypeForCar);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    engine = new ElectricEngine(i_EnergyPercentage, k_MaxCumulativeTimePerHourForElectricMotorcycle);
                    break;
                case eVehicleType.FuelMotorcycle:
                    engine = new FuelEngine(i_EnergyPercentage, k_MaxFuelAmountForFuelMotorcycle, k_FuelTypeForMotorcycle);
                    break;
                case eVehicleType.FuelTruck:
                    engine = new FuelEngine(i_EnergyPercentage, k_MaxFuelAmountForFuelTruck, k_FuelTypeForTruck);
                    break;
            }

            io_Vehicle.Engine = engine;
        }

        public static void AddWheelsToVehicle(eVehicleType i_VehicleType, Vehicle io_Vehicle, string i_WheelsManufectureName, float i_CurrentAirPressureInWheels)
        {
            switch (i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                case eVehicleType.FuelCar:
                {
                    CreateWheelsList(io_Vehicle, k_NumberOfWheelsInCar, i_WheelsManufectureName, i_CurrentAirPressureInWheels, k_MaxAirPressureForCar);
                    break;
                }

                case eVehicleType.ElectricMotorcycle:
                case eVehicleType.FuelMotorcycle:
                {
                    CreateWheelsList(io_Vehicle, k_NumberOfWheelsInMotorcycle, i_WheelsManufectureName, i_CurrentAirPressureInWheels, k_MaxAirPressureForMotorcycle);
                    break;
                }

                case eVehicleType.FuelTruck:
                {
                    CreateWheelsList(io_Vehicle, k_NumberOfWheelsInTruck, i_WheelsManufectureName, i_CurrentAirPressureInWheels, k_MaxAirPressureForTruck);
                    break;
                }
            }
        }

        public static void CreateWheelsList(Vehicle io_Vehicle, int i_AmountOfWheels, string i_WheelsManufectureName, float i_CurrentAirPressureInWheels, float i_MaxAirPressureByManufecture)
        {
            for (int i = 0; i < i_AmountOfWheels; i++)
            {
                Wheel wheel = CreateWheel(i_WheelsManufectureName, i_CurrentAirPressureInWheels, i_MaxAirPressureByManufecture);
                io_Vehicle.WheelsCollection.Add(wheel);
            }
        }

        public static Wheel CreateWheel(string i_WheelsManufectureName, float i_CurrentAirPressureInWheels, float i_MaxAirPressureByManufecture)
        {
            Wheel wheel = null;

            if (i_CurrentAirPressureInWheels >= 0 && i_CurrentAirPressureInWheels <= i_MaxAirPressureByManufecture)
            {
                wheel = new Wheel(i_WheelsManufectureName, i_CurrentAirPressureInWheels, i_MaxAirPressureByManufecture);
            }
            else
            {
                throw new ValueOutOfRangeException(i_MaxAirPressureByManufecture, 0);
            }

            return wheel;
        }

        public static void SetMemberWithProperty(string i_NameOfMember, Vehicle i_VehicleToModify, string i_UserInput)
        {
            string methodToInvoke = "Set" + i_NameOfMember + "FromString";
            MethodInfo SetValueMethodVehicle = i_VehicleToModify.GetType().GetMethod(methodToInvoke);
            try
            {
                if (SetValueMethodVehicle != null)
                {
                    SetValueMethodVehicle.Invoke(i_VehicleToModify, new object[] { i_UserInput });
                }
                else
                {
                    throw new ArgumentException("Invalid member to initialize.");
                }
            }
            catch(TargetInvocationException invokeExeption)
            {
                if (invokeExeption.InnerException is ArgumentException)
                {
                    throw invokeExeption.InnerException;
                }
            }
        }
    }
}
