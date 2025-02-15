/*
 * Name:    Joy Owoeye, Mariam Abushammala, Yomna Farid
 * Date:    February 14, 2025
 * Purpose: Program class is the main class that will run the U.S. Cities Information System.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Project1_INFO5101
{
    public class Program
    {
        public static string DataFormat { get; private set; } = "NONE";
        public static string FileName { get; private set; } = "";
        public static string MenuTitle { get; private set; } = "";
        private static DataModeler? _dataModeler;
        private static Statistics? _statistics;

        static void Main(string[] args)
        {
            _dataModeler = new DataModeler();
            DataSourceSelection();
        }

        /// <summary>
        /// Program title including the menu title and data source format.
        /// </summary>
        private static void ProgramTitle()
        {
            Clear();
            string title = $"U.S. Cities Information System v1.0           {MenuTitle}          Data Format: {DataFormat}";
            string dash = new string('-', title.Length);
            Console.WriteLine(title + "\n" + dash + "\n");
        }

        /// <summary>
        /// Message at the end of each menu option to continue.
        /// </summary>
        private static void ConsoleMessage()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Clears the console screen after each menu selection.
        /// </summary>
        private static void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// Exits the program.
        /// </summary>
        public static void ExitProgram()
        {
            Console.WriteLine("\nThank you for using the U.S. Cities Information System.");
            Environment.Exit(0);
        }

        /// <summary>
        /// Gets the user's menu selection and validates the input.
        /// </summary>
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

        /// <summary>
        /// Gets the user's data source selection and validates the input.
        /// Updates the data format and file name based on the selection.
        /// </summary>
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

                //Updating Dataformat for Porgram title
                switch (choice) 
                {
                    case 1: DataFormat = "CSV";
                            FileName = "usacities.csv"; break;

                    case 2: DataFormat = "JSON";
                            FileName = "usacities.json"; break;

                    case 3: DataFormat = "XML";
                             FileName = "usacities.xml"; break;

                    default: DataFormat = "NONE";
                               FileName = ""; break;

                }
                _dataModeler!.ParseFile(FileName!, choice);
                _statistics = new Statistics(FileName, choice);
                MainOptions(); // Proceed to main menu
            }
        }

        /// <summary>
        /// Gets users selection for main options and validates the input.
        /// </summary>
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

        /// <summary>
        /// Gets users selection for city options and validates the input.
        /// Updates the menu title based on the selection.
        /// </summary>
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

        /// <summary>
        /// Gets users selection for state options and validates the input.
        /// Updates the menu title based on the selection.
        /// </summary>
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


        /// <summary>
        /// Gets user input and calls the ReportCity() from Statistics class
        /// and displays city information if user input is valid. 
        /// </summary>
        private static void CityInformation()
        {
            Clear();
            ProgramTitle();

            bool isValidCity = false;  
            while (!isValidCity)
            {
                Console.Write("\nEnter city name: ");
                string? cityName = Console.ReadLine();
                isValidCity = _statistics!.ReportCityInformation(cityName!);
            }

            ConsoleMessage();
        }

        /// <summary>
        /// Gets user input for 2 cities and calls the ComparePopulationDensity() from Statistics class.
        /// Displays population density for each city and which one has a higher density only if
        /// user input is valid for both cities. 
        /// </summary>
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
                isNotValidCity = _statistics!.isNotVaildCityName(city1!);
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
                isNotValidCity = _statistics.isNotVaildCityName(city2!);
                if (isNotValidCity)
                {
                    Console.WriteLine("Error! Not a city. Please try again");
                    continue;
                }
                break;
            }
            _statistics.ComparePopulationDensity(city1!, city2!);

            ConsoleMessage();
        }

        /// <summary>
        /// Gets user input for 2 cities and calls the DistanceBetweenCities() from Statistics class.
        /// Displays distance between cities if user input is valid for both cities. 
        /// </summary>
        private static void DistanceBetweenCities()
        {
            Clear();
            ProgramTitle();

            Console.Write("\nEnter first city name: ");
            string? city1 = Console.ReadLine();

            Console.Write("Enter second city name: ");
            string? city2 = Console.ReadLine();

            _statistics!.ReportDistanceBetweenCities(city1!, city2!);

            ConsoleMessage();
        }

        /// <summary>
        /// Gets user input for city and calls the DistanceFromCapital() from Statistics class.
        /// Displays distance from capital if user input is valid. 
        /// </summary>
        private static void DistanceFromCapital()
        {
            Clear();
            ProgramTitle();

            Console.Write("\nEnter city name: ");
            string? cityName = Console.ReadLine();
            _statistics!.ReportDistanceFromCapital(cityName!);

            ConsoleMessage();
        }

        /// <summary>
        /// Gets user input for city and calls the ShowCityOnMap() from Statistics class.
        /// Populates link for city on map if user input is valid. 
        /// </summary>
        private static void ShowCityOnMap()
        {
            Clear();
            ProgramTitle();

            Console.Write("\nEnter city name: ");
            string? cityName = Console.ReadLine();

            //REPLACE WITH ACTUAL METHOD TO OPEN MAP
            Console.WriteLine($"Opening map for {cityName}...");
            _statistics!.ShowCityOnMap(cityName!);    
            ConsoleMessage();
        }

        /// <summary>
        /// Gets user input for city and calls the AdjustCityPopulation() from Statistics class.
        /// If city selected is valid, current city population is printed and user is prompted to enter new population.
        /// If new population is valid, population is updated and density is recalculated.
        /// </summary>
        private static void AdjustCityPopulation()
        {
            Clear();
            ProgramTitle();

            PopulationChangeEvent populationChangeEvent = new PopulationChangeEvent();

            // Subscribe to the PopulationChanged event
            populationChangeEvent.PopulationChanged += (message) => Console.WriteLine(message);

            bool isValidCity = false;
            while (!isValidCity) 
            {
                Console.Write("\nEnter city name: ");
                string? cityName = Console.ReadLine();
                isValidCity = populationChangeEvent.UpdatePopulation(cityName!, FileName);
            }

            ConsoleMessage();
        }

        /// <summary>
        /// Gets user input state abbreviation and calls the ListAllCities() from Statistics class.
        /// Displays all cities in the state if user input is valid.
        /// </summary>
        private static void ListAllCities()
        {
            Clear();
            ProgramTitle();

            bool isValidState = false;

            while (!isValidState)
            {
                Console.Write("\nEnter state abbreviation: ");
                string? state = Console.ReadLine();
                isValidState = _statistics!.ReportAllCities(state!);
            }
            ConsoleMessage();
        }

        /// <summary>
        /// Gets user input state abbreviation and calls the LargestCity() from Statistics class.
        /// Displays largest city in the state if user input is valid.
        /// </summary>
        private static void LargestCity()
        {
            Clear();
            ProgramTitle();

            bool isValidState = false;
            while (!isValidState)
            {
                Console.Write("\nEnter state abbreviation: ");
                string? state = Console.ReadLine();
                isValidState = _statistics!.ReportLargestCity(state!);
            }
            ConsoleMessage();
        }

        /// <summary>
        /// Gets user input state abbreviation and calls the SmallestCity() from Statistics class.
        /// Displays smallest city in the state if user input is valid.
        /// </summary>
        private static void SmallestCity()
        {
            Clear();
            ProgramTitle();

            bool isValidState = false;

            while (!isValidState)
            {
                Console.Write("\nEnter state abbreviation: ");
                string? state = Console.ReadLine();
                isValidState = _statistics!.ReportSmallestCity(state!);
            }
            ConsoleMessage();
        }

        /// <summary>
        /// Gets user input state abbreviation and calls the CapitalCity() from Statistics class.
        /// Displays capital city in the state if user input is valid.
        /// </summary>
        private static void CapitalCity()
        {
            Clear();
            ProgramTitle();

            bool isValidState = false;

            while (!isValidState)
            {
                Console.Write("\nEnter state abbreviation: ");
                string? state = Console.ReadLine();
                isValidState = _statistics!.ReportCapital(state!);
            }
            ConsoleMessage();
        }

        /// <summary>
        /// Gets user input state abbreviation and calls the StatePopulation() from Statistics class.
        /// Displays state population if user input is valid.
        /// </summary>
        private static void StatePopulation()
        {
            Clear();
            ProgramTitle();

            bool isValidState = false;

            while (!isValidState)
            {
                Console.Write("\nEnter state abbreviation: ");
                string? state = Console.ReadLine();
                isValidState = _statistics!.ReportStatePopulation(state!);
            }
            ConsoleMessage();
        }
    }
}


