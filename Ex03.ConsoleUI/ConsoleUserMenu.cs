using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class ConsoleUserMenu
    {
        protected enum eMenuOptions
        {
            AddNewVehicleToGarage = 1,
            ShowVehiclesLicenseNumber,
            ChangeVehicleStatus,
            InflateAirPressureToMax,
            FuelVehicle,
            ChargeElectricVehicle,
            ShowVehicleDataAccordingLicenseNumber,
            ExitMenu
        }

        private const bool v_LettersNumbersOnly = true;
        private const bool v_NumbersOnly = true;
        private const bool v_LettersOnly = true;
        private readonly Dictionary<eMenuOptions, string> m_MenuOptions;
        private Garage m_Garage;

        public ConsoleUserMenu()
        {
            m_Garage = new Garage();
            m_MenuOptions = new Dictionary<eMenuOptions, string>()
            {
                { eMenuOptions.AddNewVehicleToGarage, "Add new vehicle to the garage" },
                { eMenuOptions.ShowVehiclesLicenseNumber, "Show vehicles license numbers" },
                { eMenuOptions.ChangeVehicleStatus, "Change vehicle status" },
                { eMenuOptions.InflateAirPressureToMax, "Inflate air pressure to max" },
                { eMenuOptions.FuelVehicle, "Fuel vehicle" },
                { eMenuOptions.ChargeElectricVehicle, "Charge electric vehicle" },
                { eMenuOptions.ShowVehicleDataAccordingLicenseNumber, "Show vehicle data according license number" },
                { eMenuOptions.ExitMenu, "Exit menu" }
            };
        }

        public string GetInputFromUser(string i_TextToDisplay)
        {
            if (i_TextToDisplay.Length > 0)
            {
                Console.Write(i_TextToDisplay);
            }

            string userInput = Console.ReadLine();

            return userInput;
        }

        public string GetFieldFromUser(string i_FieldToAdd, bool i_IsLettersAndNumbers, bool i_IsNumbersOnly = false, bool i_IsLettersOnly = false)
        {
            string userInput = GetInputFromUser(i_FieldToAdd);

            if (i_IsLettersAndNumbers)
            {
                while (!IsLettersAndDigitsWord(userInput))
                {
                    Console.WriteLine("The field should contain only letters, numbers and spaces");
                    userInput = GetInputFromUser("Please try again: ");
                }
            }
            else if (i_IsNumbersOnly)
            {
                while (!IsOnlyDigitsWord(userInput))
                {
                    Console.WriteLine("The field should contain only numbers");
                    userInput = GetInputFromUser("Please try again: ");
                }
            }
            else if (i_IsLettersOnly)
            {
                while (!IsOnlyLettersWord(userInput))
                {
                    Console.WriteLine("The field should contain only letters and spaces");
                    userInput = GetInputFromUser("Please try again: ");
                }
            }

            return userInput;
        }

        private bool IsOnlyLettersWord(string i_InputToCheck)
        {
            bool isOnlyLetters = true;
            bool isDigit;

            for (int i = 0; i < i_InputToCheck.Length; ++i)
            {
                int charInWord;
                isDigit = int.TryParse(i_InputToCheck[i].ToString(), out charInWord);

                if (isDigit)
                {
                    isOnlyLetters = false;
                }
            }

            return isOnlyLetters;
        }

        private bool IsOnlyDigitsWord(string i_InputToCheck)
        {
            bool isOnlyDigit = true;
            bool isDigit;

            for (int i = 0; i < i_InputToCheck.Length; ++i)
            {
                int charInWord;
                isDigit = int.TryParse(i_InputToCheck[i].ToString(), out charInWord);

                if (!isDigit)
                {
                    isOnlyDigit = false;
                }
            }

            return isOnlyDigit;
        }

        private bool IsLettersAndDigitsWord(string i_InputToCheck)
        {
            bool isLetterOrDigitsOnly = true;

            for (int i = 0; i < i_InputToCheck.Length; ++i)
            {
                if (!char.IsLetterOrDigit(i_InputToCheck[i]) && i_InputToCheck[i] != ' ')
                {
                    isLetterOrDigitsOnly = false;
                }
            }

            return isLetterOrDigitsOnly;
        }

        public void OpenMenuInConsole()
        {
            string userInput;
            bool toContinue = true;
            do
            {
                GarageShowConsoleMenuOptions();
                userInput = Console.ReadLine();
                int ChoosedOption;

                if (int.TryParse(userInput, out ChoosedOption))
                {
                    toContinue = DoLogicAccordingToUserChoise(ChoosedOption);
                }
                else
                {
                    Console.WriteLine("Wrong Input, please enter again");
                }
            }
            while (toContinue);
        }

        private void GarageShowConsoleMenuOptions()
        {
            Console.WriteLine("Please choose the option from the menu:");
            foreach (eMenuOptions menuOption in m_MenuOptions.Keys)
            {
                Console.WriteLine("{0} - {1}", (int)menuOption, m_MenuOptions[menuOption]);
            }
        }

        private bool DoLogicAccordingToUserChoise(int i_ChoosedOption)
        {
            bool toContinue = true;

            switch (i_ChoosedOption)
            {
                case (int)eMenuOptions.AddNewVehicleToGarage:
                    {
                        addVehicleToGarage();
                        break;
                    }

                case (int)eMenuOptions.ShowVehiclesLicenseNumber:
                    {
                        ShowVehicleLicenseNumber();
                        break;
                    }

                case (int)eMenuOptions.ChangeVehicleStatus:
                    {
                        ChangeVehicleStatus();
                        break;
                    }

                case (int)eMenuOptions.InflateAirPressureToMax:
                    {
                        InflateAirPressureToMax();
                        break;
                    }

                case (int)eMenuOptions.FuelVehicle:
                    {
                        FuelVehicle();
                        break;
                    }

                case (int)eMenuOptions.ChargeElectricVehicle:
                    {
                        ChargeElectricVehicle();
                        break;
                    }

                case (int)eMenuOptions.ShowVehicleDataAccordingLicenseNumber:
                    {
                        ShowVehicleDataAccordingLicenseNumber();
                        break;
                    }

                case (int)eMenuOptions.ExitMenu:
                    {
                        toContinue = false;
                        break;
                    }

                default:
                    {
                        Console.WriteLine("The option not exist, please try again.");
                        break;
                    }
            }

            return toContinue;
        }

        private void addVehicleToGarage()
        {
            Console.WriteLine("Please Enter the following details:");
            string vehicleLicenseNumber = GetLicenseNumberFromUser();
            if (m_Garage.IsVehicleInGarage(vehicleLicenseNumber))
            {
                m_Garage.ChangeVehiclStatus(vehicleLicenseNumber, eVehicleStatusInGarage.InRepair);
                Console.WriteLine("Vehicle is already in the garage, Vehicle with licesnse number: {0} status is: {1}", vehicleLicenseNumber, m_Garage.GetRecordByLicenseNumber(vehicleLicenseNumber).VehicleStatusInGarage);
            }
            else
            {
                string ownerName = GetFieldFromUser("Name: ", !v_LettersNumbersOnly, !v_NumbersOnly, v_LettersOnly);
                string ownerPhoneNumber = GetFieldFromUser("Phone number: ", !v_LettersNumbersOnly, v_NumbersOnly);
                Vehicle newVehicle = SetGenericVehicleInfo(vehicleLicenseNumber);
                SetSpecificDataForVehicleByType(newVehicle);
                m_Garage.PutVehicleInGarage(ownerName, ownerPhoneNumber, newVehicle);
            }
        }

        private void SetSpecificDataForVehicleByType(Vehicle i_VehicleToAddInfo)
        {
            FieldInfo[] fields = i_VehicleToAddInfo.GetType().GetFields(BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo fieldOfSpesificVehicle in fields)
            {
                int delimiterForMemeberName = fieldOfSpesificVehicle.Name.IndexOf('_');
                string memberNameOfVehicleChild = fieldOfSpesificVehicle.Name.Substring(delimiterForMemeberName + 1, fieldOfSpesificVehicle.Name.Length - 2);
                SetMemeberInVehicle(fieldOfSpesificVehicle.FieldType, memberNameOfVehicleChild, i_VehicleToAddInfo);
            }
        }

        private void SetMemeberInVehicle(Type i_TypeOfMember, string i_NameOfMember, Vehicle i_VehicleToModify)
        {
            bool isValid = false;
            while (!isValid)
            {
                try
                {
                    string userInputToCurrentField;

                    if (i_TypeOfMember == typeof(bool))
                    {
                        userInputToCurrentField = GetBooleanType(GetMemberName(i_NameOfMember) + ": ");
                        isValid = true;
                    }
                    else if (i_TypeOfMember.IsEnum)
                    {
                        userInputToCurrentField = ShowEnumTypeGeneric(i_TypeOfMember);
                        isValid = true;
                    }
                    else
                    {
                        userInputToCurrentField = GetInputFromUser(GetMemberName(i_NameOfMember) + ": ");
                        isValid = IsValidInputByType(i_TypeOfMember, userInputToCurrentField);
                    }

                    if(isValid)
                    {
                        VehicleCreator.SetMemberWithProperty(i_NameOfMember, i_VehicleToModify, userInputToCurrentField);
                    }
                    else
                    {
                        Console.WriteLine("Wrong value, please try again: ");
                    }
                }
                catch (ArgumentException argExeption)
                {
                    Console.WriteLine(argExeption.Message + ", please try again: ");
                }
            }
        }

        private bool IsValidInputByType(Type i_Type, string i_InputData)
        {
            bool isValid = false;
            if(i_Type == typeof(int))
            {
                int userInputToCurrentFieldInt;
                isValid = int.TryParse(i_InputData, out userInputToCurrentFieldInt);
            }
            else if(i_Type == typeof(float))
            {
                float userInputToCurrentFieldfloat;
                isValid = float.TryParse(i_InputData, out userInputToCurrentFieldfloat);
            }
            else if(i_Type == typeof(char))
            {
                char userInputToCurrentFieldChar;
                isValid = char.TryParse(i_InputData, out userInputToCurrentFieldChar);
            }
            else if (i_Type == typeof(double))
            {
                double userInputToCurrentFielddouble;
                isValid = double.TryParse(i_InputData, out userInputToCurrentFielddouble);
            }

            return isValid;
        }

        private string GetMemberName(string i_MemberNameNoSeperation)
        {
            int lastIndex = 0;
            List<string> wordsInMember = new List<string>();
            for (int i = 1; i < i_MemberNameNoSeperation.Length; ++i)
            {
                if (!char.IsLower(i_MemberNameNoSeperation[i]))
                {
                    wordsInMember.Add(i_MemberNameNoSeperation.Substring(lastIndex, i - lastIndex));
                    lastIndex = i;
                }
            }

            wordsInMember.Add(i_MemberNameNoSeperation.Substring(lastIndex, i_MemberNameNoSeperation.Length - lastIndex));
            string outPutWord = string.Empty;
            outPutWord += wordsInMember[0];

            for (int i = 1; i < wordsInMember.Count; ++i)
            {
                outPutWord += " " + wordsInMember[i].ToLower();
            }

            return outPutWord;
        }

        private string ShowEnumTypeGeneric(Type i_EnumToCheck)
        {
            string userInput;
            if (i_EnumToCheck == typeof(eColor))
            {
                ShowEnumOptions<eColor>();
                userInput = GetFieldFromUser("Color: ", !v_LettersNumbersOnly, v_NumbersOnly);
                userInput = ParseEnum<eColor>(userInput).ToString();
            }
            else if (i_EnumToCheck == typeof(eDoorNumber))
            {
                ShowEnumOptions<eDoorNumber>();
                userInput = GetFieldFromUser("Number of doors: ", !v_LettersNumbersOnly, v_NumbersOnly);
                userInput = ParseEnum<eDoorNumber>(userInput).ToString();
            }
            else if (i_EnumToCheck == typeof(eLicenseType))
            {
                ShowEnumOptions<eLicenseType>();
                userInput = GetFieldFromUser("License Type: ", !v_LettersNumbersOnly, v_NumbersOnly);
                userInput = ParseEnum<eLicenseType>(userInput).ToString();
            }
            else
            {
                Console.WriteLine("Type is not match, nothing to do.");
                userInput = string.Empty;
            }

            return userInput;
        }

        private Vehicle SetGenericVehicleInfo(string i_LicenseNumber)
        {
            ShowEnumOptions<eVehicleType>();
            string vehicleType = GetFieldFromUser("Vehicle type: ", !v_LettersNumbersOnly, v_NumbersOnly, !v_LettersOnly);
            eVehicleType vehicleTypeEnum = ParseEnum<eVehicleType>(vehicleType);
            string model = GetFieldFromUser("Model's name: ", v_LettersNumbersOnly);
            float energyPrecentage = GetEnergyPrecentage();
            Vehicle newVehicle = VehicleCreator.CreateVehicle(i_LicenseNumber, vehicleTypeEnum, model, energyPrecentage);
            AddWheelInfo(vehicleTypeEnum, newVehicle);

            return newVehicle;
        }

        private float GetEnergyPrecentage()
        {
            string energyPercentage = GetFieldFromUser("Energy percentage left in the vehicle: ", !v_LettersNumbersOnly, v_NumbersOnly, !v_LettersOnly);
            float energyPrecentageFloat = ConvertStringToFloat(energyPercentage);
            while(energyPrecentageFloat > 100 || energyPrecentageFloat < 0)
            {
                energyPercentage = GetFieldFromUser("Wrong Input, Precente should be between 0 - 100, please enter again: ", !v_LettersNumbersOnly, v_NumbersOnly);
                energyPrecentageFloat = ConvertStringToFloat(energyPercentage);
            }

            return energyPrecentageFloat;
        }

        private void AddWheelInfo(eVehicleType i_VehicleType, Vehicle i_ToAddVehicle)
        {
            try
            {
                string manufectureName = GetFieldFromUser("Wheels manufecture name: ", v_LettersNumbersOnly);
                string currentAirPressure = GetFieldFromUser("Current air pressure in wheels: ", !v_LettersNumbersOnly, v_NumbersOnly);
                float currentAirPressureInFloat = ConvertStringToFloat(currentAirPressure);
                VehicleCreator.AddWheelsToVehicle(i_VehicleType, i_ToAddVehicle, manufectureName, currentAirPressureInFloat);
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                Console.WriteLine(valueOutOfRangeException.Message);
                AddWheelInfo(i_VehicleType, i_ToAddVehicle);
            }
        }

        private TEnum ParseEnum<TEnum>(string i_StringToEnum)
        {
            int StringToInt = ConvertStringToInt(i_StringToEnum);

            while (!Enum.IsDefined(typeof(TEnum), StringToInt))
            {
                i_StringToEnum = GetFieldFromUser("Invalid input, try again: ", !v_LettersNumbersOnly, v_NumbersOnly, !v_LettersOnly);
                StringToInt = ConvertStringToInt(i_StringToEnum);
            }

            TEnum convertedEnum = (TEnum)Enum.ToObject(typeof(TEnum), StringToInt);

            return convertedEnum;
        }

        private float ConvertStringToFloat(string i_StringToFloat)
        {
            float numberFromUser;
            bool isValid = float.TryParse(i_StringToFloat, out numberFromUser);

            while (!isValid)
            {
                Console.Write("Invalid input, try again: ");
                i_StringToFloat = Console.ReadLine();
                isValid = float.TryParse(i_StringToFloat, out numberFromUser);
            }

            return numberFromUser;
        }

        private int ConvertStringToInt(string i_StringToInt)
        {
            int parsedInt;
            bool isValid = int.TryParse(i_StringToInt, out parsedInt);

            while (!isValid)
            {
                Console.Write("Invalid input, try again: ");
                i_StringToInt = Console.ReadLine();
                isValid = int.TryParse(i_StringToInt, out parsedInt);
            }

            return parsedInt;
        }

        private void ShowEnumOptions<TEbum>()
        {
            string[] enumTypes = Enum.GetNames(typeof(TEbum));
            int counter = 1;
            foreach (string enumString in enumTypes)
            {
                Console.WriteLine("{0} - {1}", counter, enumString);
                counter++;
            }
        }

        public string GetBooleanType(string i_MemberName)
        {
            Console.Write(string.Format("{0}{1}1 - Yes{1}2 - No{1}", i_MemberName, Environment.NewLine));
            string UserInput = GetInputFromUser("Your Choice: ");
            while (UserInput != "1" && UserInput != "2")
            {
                UserInput = GetInputFromUser("The input is invalid, please choose 1 or 2: ");
            }

            UserInput = UserInput == "1" ? "true" : "false";

            return UserInput;
        }

        private void ShowVehicleLicenseNumber()
        {
            List<string> vehiclesInGarage;

            if (ToFilterByStatus())
            {
                eVehicleStatusInGarage statusToFilterBy = GetStatusFromUser();
                vehiclesInGarage = m_Garage.ListOfLicenseNumbersByStatus(statusToFilterBy);
            }
            else
            {
                vehiclesInGarage = m_Garage.ListOfAllLicenseNumbersInGarage();
            }

            if (vehiclesInGarage.Count == 0)
            {
                Console.WriteLine("There are no matched vehicles in the garage at the moment.");
            }
            else
            {
                Console.WriteLine("The vehicles license number that currently in garage are:");
                foreach (string licenseNumberOfVehicle in vehiclesInGarage)
                {
                    Console.WriteLine(licenseNumberOfVehicle);
                }
            }
        }

        private bool ToFilterByStatus()
        {
            string toFilterByStatusAnswer = GetBooleanType("Do you want to filter by status ?");

            return toFilterByStatusAnswer == "true";
        }

        private eVehicleStatusInGarage GetStatusFromUser()
        {
            ShowEnumOptions<eVehicleStatusInGarage>();
            string vehicleStatus = GetFieldFromUser("Vehicle status: ", !v_LettersNumbersOnly, v_NumbersOnly, !v_LettersOnly);
            eVehicleStatusInGarage vehicleStatusEnum = ParseEnum<eVehicleStatusInGarage>(vehicleStatus);

            return vehicleStatusEnum;
        }

        private void ChangeVehicleStatus()
        {
            string licenseNumber = GetLicenseNumberFromUser();

            if (!m_Garage.IsVehicleInGarage(licenseNumber))
            {
                Console.WriteLine("Vehicle is not in garage, nothing to do.");
            }
            else
            {
                eVehicleStatusInGarage statusInGarage = GetStatusFromUser();
                m_Garage.ChangeVehiclStatus(licenseNumber, statusInGarage);
                Console.WriteLine("Status Changed.");
            }
        }

        private void InflateAirPressureToMax()
        {
            string vehicleLicenseNumber = GetLicenseNumberFromUser();

            if (m_Garage.IsVehicleInGarage(vehicleLicenseNumber))
            {
                m_Garage.InflateAirPressureToMax(vehicleLicenseNumber);
            }
        }

        private void FuelVehicle()
        {
            try
            {
                string vehicleLicenseNumber = GetLicenseNumberFromUser();
                if (!m_Garage.IsVehicleInGarage(vehicleLicenseNumber))
                {
                    Console.WriteLine("Vehicle is not in garage, nothing to do.");
                }
                else
                {
                    Vehicle vehicleToFuel = m_Garage.GetRecordByLicenseNumber(vehicleLicenseNumber).VehicleInGarage;
                    if (vehicleToFuel.EnergyPercentage == 100.0f)
                    {
                        Console.WriteLine("The vehicle is load to maximum energy.");
                    }
                    else
                    {
                        ShowEnumOptions<eFuelType>();
                        Console.Write("Please enter the type of fuel: ");
                        string FuelTypeToAddInString = Console.ReadLine();
                        eFuelType FuelTypeToAdd = ParseEnum<eFuelType>(FuelTypeToAddInString);
                        Console.Write("Please enter how many liters of fuel you would like to add: ");
                        string litersToAddInString = Console.ReadLine();
                        float litersToAdd = ConvertStringToFloat(litersToAddInString);
                        m_Garage.FuelVehicle(vehicleLicenseNumber, litersToAdd, FuelTypeToAdd);
                    }
                }
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                Console.WriteLine(valueOutOfRangeException.Message);
                if (TryActionAgain())
                {
                    FuelVehicle();
                }
            }
            catch (ArgumentException wrongArgExeption)
            {
                Console.WriteLine(wrongArgExeption.Message);
                if (TryActionAgain())
                {
                    FuelVehicle();
                }
            }
        }

        private bool TryActionAgain()
        {
            bool isToTryAgain = false;
            string userAnswer = GetBooleanType("Do you want to try again? ");
            if (userAnswer == "true")
            {
                isToTryAgain = true;
            }

            return isToTryAgain;
        }

        private void ChargeElectricVehicle()
        {
            try
            {
                string vehicleLicenseNumber = GetLicenseNumberFromUser();
                if (!m_Garage.IsVehicleInGarage(vehicleLicenseNumber))
                {
                    Console.WriteLine("Vehicle is not in garage, nothing to do.");
                }
                else
                {
                    Vehicle vehicleToCharge = m_Garage.GetRecordByLicenseNumber(vehicleLicenseNumber).VehicleInGarage;
                    if (vehicleToCharge.EnergyPercentage == 100.0f)
                    {
                        Console.Write("The vehicle is load to maximum energy.");
                    }
                    else
                    {
                        Console.Write("Please enter how many minutes you would like to load: ");
                        string numberOfMinutesToAddInString = Console.ReadLine();
                        float numberOfHourToAdd = ConvertStringToFloat(numberOfMinutesToAddInString);
                        m_Garage.ChargeElectricVehicle(vehicleLicenseNumber, numberOfHourToAdd);
                    }
                }
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                Console.WriteLine(valueOutOfRangeException.Message);
                if (TryActionAgain())
                {
                    ChargeElectricVehicle();
                }
            }
            catch (ArgumentException wrongVehicleTypeExeption)
            {
                Console.WriteLine(wrongVehicleTypeExeption.Message);
                if (TryActionAgain())
                {
                    ChargeElectricVehicle();
                }
            }
        }

        private void ShowVehicleDataAccordingLicenseNumber()
        {
            string vehicleLicenseNumber = GetLicenseNumberFromUser();
            if (!m_Garage.IsVehicleInGarage(vehicleLicenseNumber))
            {
                Console.WriteLine("Vehicle is not in garage, nothing to do.");
            }
            else
            {
                string vehicleDetails;
                vehicleDetails = m_Garage.VehicleDataAccordingLicenseNumberToString(vehicleLicenseNumber);
                Console.WriteLine(vehicleDetails);
            }
        }

        private string GetLicenseNumberFromUser()
        {
            string vehicleLicenseNumber = GetFieldFromUser("Vehicle license number: ", !v_LettersNumbersOnly, v_NumbersOnly);
            return vehicleLicenseNumber;
        }
    }
}