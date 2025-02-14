using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Project1_INFO5101
{
    public class Program
    {
        public static string DataFormat { get; private set; } = "NONE";
        public static string MenuTitle { get; private set; } = "";
        private static DataModeler _dataModeler;
        private static Statistics _statistics;

        static void Main(string[] args)
        {
            _dataModeler = new DataModeler();
            DataSourceSelection();
        }

        //FIXMEEE Adjust the title --- to when MenuTitle is set everytime.
        private static void ProgramTitle()
        {
            Clear();
            Console.WriteLine($"U.S. Cities Information System v1.0    {MenuTitle}   Data Format: {DataFormat}");

            Console.WriteLine("----------------------------------------------------------------------------------\n");
        }

        private static void ConsoleMessage()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void Clear()
        {
            Console.Clear();
        }

        public static void ExitProgram()
        {
            Console.WriteLine("\nThank you for using the U.S. Cities Information System.");
            Environment.Exit(0);
        }

        private static int GetMenuSelection(int min, int max)
        {
            while (true)
            {
                Console.Write("Enter your selection: ");

                //checking if input is a number
                if (int.TryParse(Console.ReadLine(), out int selection) && selection >= min && selection <= max)
                {
                    return selection;
                }
                Console.WriteLine($"Invalid input. Please enter a selection between {min} and {max}");
            }
        }

        private static void DataSourceSelection()
        {
            while (true)
            {
                ProgramTitle();
                Console.WriteLine("Select Data Source Format:\n");
                Console.WriteLine("1. CSV - usacities.csv");
                Console.WriteLine("2. JSON - usacities.json");
                Console.WriteLine("3. XML - usacities.xml");

                //selection has to be between 1 and 3 for data source selection
                int choice = GetMenuSelection(1, 3);

                string? fileName;

                //Updating Dataformat for Porgram title
                switch (choice) 
                {
                    case 1: DataFormat = "CSV";
                            fileName = "usacities.csv"; break;

                    case 2: DataFormat = "JSON";
                            fileName = "usacities.json"; break;

                    case 3: DataFormat = "XML";
                            fileName = "usacities.xml"; break;

                    default: DataFormat = "NONE";
                            fileName = ""; break;

                }
                _dataModeler.ParseFile(fileName, choice);
                _statistics = new Statistics(fileName, choice);
                MainOptions(); // Proceed to main menu
            }
        }

        private static void MainOptions()
        {
            while (true)
            {
                ProgramTitle();
                Console.WriteLine("Main Options:");
                Console.WriteLine("1. Query by City");
                Console.WriteLine("2. Query by State");
                Console.WriteLine("3. Change Data Source");
                Console.WriteLine("4. Exit");

                int choice = GetMenuSelection(1, 4);
                switch (choice)
                {
                    case 1: CityOptions(); break;
                    case 2: StateOptions(); break;
                    case 3: DataFormat = "NONE";  DataSourceSelection(); break;
                    case 4: ExitProgram(); break;
                }
            }
        }

        private static void CityOptions()
        {
            while (true)
            {
                ProgramTitle();
                Console.WriteLine("City Options:");
                Console.WriteLine("1. City Information");
                Console.WriteLine("2. Compare Population Density");
                Console.WriteLine("3. Distance Between Cities");
                Console.WriteLine("4. Distance from Capital");
                Console.WriteLine("5. Show City on Map");
                Console.WriteLine("6. Adjust Population");
                Console.WriteLine("7. Back to Main Options");

                int choice = GetMenuSelection(1, 7);
                switch (choice)
                {
                    case 1: MenuTitle = "CITY INFORMATION"; CityInformation(); break;
                    case 2: MenuTitle = "COMPARE POPULATION DENSITY"; ComparePopulationDensity(); break;
                    case 3: MenuTitle = "DISTANCE BETWEEN CITIES"; DistanceBetweenCities(); break;
                    case 4: MenuTitle = "DISTANCE FROM CAPITAL"; DistanceFromCapital(); break;
                    case 5: MenuTitle = "SHOW CITY ON MAP"; ShowCityOnMap(); break;
                    case 6: MenuTitle = "ADJUST CITY POPULATION"; AdjustCityPopulation(); break;
                    case 7: return;
                }
            }
        }

        private static void StateOptions()
        {
            while (true)
            {
                ProgramTitle();
                Console.WriteLine("State Options:");
                Console.WriteLine("1. List All Cities");
                Console.WriteLine("2. Largest City");
                Console.WriteLine("3. Smallest City");
                Console.WriteLine("4. Capital City");
                Console.WriteLine("5. State Population");
                Console.WriteLine("6. Back to Main Options");

                int choice = GetMenuSelection(1, 6);
                switch (choice)
                {
                    case 1: MenuTitle = "LIST ALL CITIES";  ListAllCities();  break;
                    case 2: MenuTitle = "LARGEST CITY"; LargestCity(); break;
                    case 3: MenuTitle = "SMALLEST CITY"; SmallestCity(); break;
                    case 4: MenuTitle = "CAPITAL CITY"; CapitalCity(); break;
                    case 5: MenuTitle = "STATE POPULATION"; StatePopulation(); break;
                    case 6: return;
                }
            }
        }


        //ALL METHODS HERE NOT TO BE ADJUSTED FOR VALIDATION AND IN STATS TO RETURN BOOL FLAG
        //All City Option methods
        private static void CityInformation()
        {
            Clear();
            ProgramTitle();

            bool isValidCity = false;  
            while (!isValidCity)
            {
                Console.Write("\nEnter city name: ");
                string? cityName = Console.ReadLine();
                isValidCity = _statistics.ReportCityInformation(cityName);
            }

            ConsoleMessage();
        }

        private static void ComparePopulationDensity()
        {
            Clear();
            ProgramTitle();

            string? city2 = "";
            string? city1 = "";

            bool isNotValidCity = false;
            while (true)
            {
                Console.Write("\nEnter first city name: ");
                 city1 = Console.ReadLine();
                isNotValidCity = _statistics.isNotVaildCityName(city1);
                if (isNotValidCity)
                {
                    Console.WriteLine("Error! Not a city. Please try again");
                    continue;
                }
                break;
            }
          
            while (true)
            {
                Console.Write("\nEnter second city name: ");
                city2 = Console.ReadLine();
                isNotValidCity = _statistics.isNotVaildCityName(city2);
                if (isNotValidCity)
                {
                    Console.WriteLine("Error! Not a city. Please try again");
                    continue;
                }
                break;
            }
            _statistics.ComparePopulationDensity(city1, city2);

            ConsoleMessage();
        }

        private static void DistanceBetweenCities()
        {
            Clear();
            ProgramTitle();

            Console.Write("\nEnter first city name: ");
            string? city1 = Console.ReadLine();

            Console.Write("Enter second city name: ");
            string? city2 = Console.ReadLine();

            _statistics.ReportDistanceBetweenCities(city1, city2);

            ConsoleMessage();
        }

        private static void DistanceFromCapital()
        {
            Clear();
            ProgramTitle();

            Console.Write("\nEnter city name: ");
            string? cityName = Console.ReadLine();
            _statistics.ReportDistanceFromCapital(cityName);

            ConsoleMessage();
        }

        private static void ShowCityOnMap()
        {
            Clear();
            ProgramTitle();

            Console.Write("\nEnter city name: ");
            string? cityName = Console.ReadLine();

            //REPLACE WITH ACTUAL METHOD TO OPEN MAP
            Console.WriteLine($"Opening map for {cityName}...");
            _statistics.ShowCityOnMap(cityName);    
            ConsoleMessage();
        }

        private static void AdjustCityPopulation()
        {
            Clear();
            ProgramTitle();

            PopulationChangeEvent populationChangeEvent = new PopulationChangeEvent();
            Console.Write("\nEnter city name: ");
            string? cityName = Console.ReadLine();
            
            Console.Write("Enter new population: ");

            if (int.TryParse(Console.ReadLine(), out int newPopulation))
            {
                //REPLACE WITH ACTUAL METHOD TO ADJUST POPULATION

                ConsoleMessage();
            }
            else
            {
                Console.WriteLine("Invalid population input.");
            }
        }


        //All State Option methods no 
        private static void ListAllCities()
        {
            Clear();
            ProgramTitle();

            bool isValidState = false;

            while (!isValidState)
            {
                Console.Write("\nEnter state abbreviation: ");
                string? state = Console.ReadLine();
                isValidState = _statistics.ReportAllCities(state);
            }
            ConsoleMessage();
        }

        private static void LargestCity()
        {
            Clear();
            ProgramTitle();

            bool isValidState = false;
            while (!isValidState)
            {
                Console.Write("\nEnter state abbreviation: ");
                string? state = Console.ReadLine();
                isValidState = _statistics.ReportLargestCity(state);
            }
            ConsoleMessage();
        }

        private static void SmallestCity()
        {
            Clear();
            ProgramTitle();

            bool isValidState = false;

            while (!isValidState)
            {
                Console.Write("\nEnter state abbreviation: ");
                string? state = Console.ReadLine();
                isValidState = _statistics.ReportSmallestCity(state);
            }
            ConsoleMessage();
        }

        private static void CapitalCity()
        {
            Clear();
            ProgramTitle();

            bool isValidState = false;

            while (!isValidState)
            {
                Console.Write("\nEnter state abbreviation: ");
                string? state = Console.ReadLine();
                isValidState = _statistics.ReportCapital(state);
            }
            ConsoleMessage();
        }

        private static void StatePopulation()
        {
            Clear();
            ProgramTitle();

            bool isValidState = false;

            while (!isValidState)
            {
                Console.Write("\nEnter state abbreviation: ");
                string? state = Console.ReadLine();
                isValidState = _statistics.ReportStatePopulation(state);
            }
            ConsoleMessage();
        }
    }
}


