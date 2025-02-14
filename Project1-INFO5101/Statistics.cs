/*
 * Name: Joy Owoeye, Mariam Abushammala, Yomna, Farid
 * Date: February 14, 2025
 * Purpose: Statistics class for manipulating Dictionary data 
 */
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using Microsoft.VisualBasic;
using System.Xml.Linq;

namespace Project1_INFO5101
{
    public class Statistics
    {

        //holds all the city  information returned from the DataModeler class.
        private Dictionary<string, List<CityInfo>> citiesDictionary;


        /// <summary>
        /// Uses the constructor to initialize the dictionary property by calling the DataModeler.Parse method
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filetype"></param>
        public Statistics(string fileName, int filetype)
        {
            DataModeler dataModeler = new DataModeler();
            dataModeler.ParseFile(fileName, filetype);
            citiesDictionary = dataModeler.CityDictionary;
        }


        /// <summary>
        /// Reports all the city information in the dictionary for a selected city name.
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public bool ReportCityInformation(string cityName)
        {
            if (citiesDictionary.ContainsKey(cityName))
            {
                List<CityInfo> list = citiesDictionary[cityName];

                //Printing the number of matches
                Console.WriteLine($"\nNumber of matches: {list.Count}");

                int count = 1;
                foreach (CityInfo cityInfo in list)
                {

                    Console.WriteLine($"\n{count}. {cityInfo.Name}, {cityInfo.StateAbbrev}");

                    Console.WriteLine("{0,-20} {1,-15}", "State:", cityInfo.State);
                    Console.WriteLine("{0,-20} {1,-15}", "Population:", cityInfo.Population.ToString("N0"));
                    Console.WriteLine("{0,-20} {1,-15}", "Pop. Density:", cityInfo.Density);
                    Console.WriteLine("{0,-20} {1,-15}", "Longitude:", cityInfo.Longitude);
                    Console.WriteLine("{0,-20} {1,-15}", "Latitude:", cityInfo.Latitude);
                    Console.WriteLine("{0,-20} {1,-15}", "Time Zone:", cityInfo.TimeZone);
                    Console.WriteLine("{0,-20} {1,-15}", "Capital:", cityInfo.Capital == null || cityInfo.Capital.Equals("") ? "No" : cityInfo.Capital);

                    count++;
                }
                return true;
            }
            //If city is not found
            else
            {
                Console.WriteLine($"'{cityName}' not found.");
                return false;
            }
        }


        public bool isNotVaildCityName(string cityName)
        {
            if (citiesDictionary.ContainsKey(cityName))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// checks to see if there are muiliple cites in the list
        /// </summary>
        /// <param name="cityNameA"></param>
        /// <param name="cityNameB"></param>
        public int checkForMuilipleCities(List<CityInfo> list)
        {

            if (list.Count > 1)
            {
                int count = 0;
                Console.WriteLine("\nMatching cities....\n");
                foreach (CityInfo cityInfo in list)
                {
                    Console.WriteLine($"{++count}. {cityInfo.Name}, {cityInfo.State}");


                }
                Console.WriteLine();
                while (true)
                {

                    Console.Write("Enter you selection:");

                    int.TryParse(Console.ReadLine(), out int selection);

                    if (selection == 1)
                    {
                        return 1;
                    }
                    else if (selection == 2)
                    {
                        return 2;
                    }
                    else
                    {
                        Console.WriteLine("Error! Invaid Number! Please try again");
                        continue;
                    }
                }

            }
            return 0; //if there is only one city

        }

        /// <summary>
        /// reports the population density for each city while indicating which city has the higher population density.
        /// </summary>
        /// <param name="cityNameA"></param>
        /// <param name="cityNameB"></param>
        public bool ComparePopulationDensity(string cityNameA, string cityNameB)
        {

            double densityA = 0, densityB = 0;
            string cityA = "", cityB = "", stateAbbrevA = "", stateAbbrevB = "";



            if (citiesDictionary.ContainsKey(cityNameA))
            {
                List<CityInfo> listA = citiesDictionary[cityNameA];

                int selection = checkForMuilipleCities(listA);

                foreach (CityInfo cityInfo in listA)
                {
                    if (selection == 1 || selection == 0)
                    {
                        cityA = listA.ElementAt(0).Name;
                        stateAbbrevA = listA.ElementAt(0).StateAbbrev;
                        densityA = listA.ElementAt(0).Density;
                        break;
                    }
                    else if (selection == 2)
                    {
                        cityA = listA.ElementAt(1).Name;
                        stateAbbrevA = listA.ElementAt(1).StateAbbrev;
                        densityA = listA.ElementAt(1).Density;
                        break;
                    }


                }





                Console.WriteLine($"{cityA}, {stateAbbrevA}  has a population density of {densityA.ToString("N0")} people per sq. km");
             
            }
            else
            {
                Console.WriteLine($"'{cityNameA}' not found.");
                return false;
            }


            if (citiesDictionary.ContainsKey(cityNameB))
            {
                List<CityInfo> listB = citiesDictionary[cityNameB];

                int selection = checkForMuilipleCities(listB);

                foreach (CityInfo cityInfo in listB)
                {
                    if (selection == 1 || selection == 0)
                    {
                        cityB = listB.ElementAt(0).Name;
                        stateAbbrevB = listB.ElementAt(0).StateAbbrev;
                        densityB = listB.ElementAt(0).Density;
                        break;
                    }
                    else if (selection == 2)
                    {
                        cityB = listB.ElementAt(1).Name;
                        stateAbbrevB = listB.ElementAt(1).StateAbbrev;
                        densityB = listB.ElementAt(1).Density;
                        break;
                    }


                }
                Console.WriteLine($"{cityB}, {stateAbbrevB}  has a population density of {densityB.ToString("N0")} people per sq. km");
            }
            else
            {
                Console.WriteLine($"'{cityNameA}' not found.");
                return false;
            }

            string nameAndState = densityA > densityB ? cityNameA + ", " + stateAbbrevA : cityNameB + ", " + stateAbbrevB;
            Console.WriteLine($"\n{nameAndState} has the higher population density");
            
            return false;
        }
        /// <summary>
        /// Uses Haversine Formula to calculate distance between two latitude/longitude points https://en.wikipedia.org/wiki/Haversine_formula
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        
        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double Radius = 6371; 
            double distanceLat = (lat2 - lat1) * Math.PI / 180;
            double distanceLon = (lon2 - lon1) * Math.PI / 180;
            double a = Math.Sin(distanceLat / 2) * Math.Sin(distanceLat / 2) + Math.Cos(lat1 * Math.PI / 180) * Math.Cos((lat2) * Math.PI / 180) * Math.Sin(distanceLon / 2) * Math.Sin(distanceLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return Radius * c;
        }


        /// <summary>
        /// reports the distance between any two cities using the latitude and longitude of the input cities stored in the cities dictionary.
        /// </summary>
        /// <param name="cityNameA"></param>
        /// <param name="cityNameB"></param>

        public void ReportDistanceBetweenCities(string cityNameA, string cityNameB)
        {
            double distanceALng = 0;
            double distanceALat = 0;
            double distanceBLng = 0;
            double distanceBLat = 0;
            string stateAbbrevA = "";
            string stateAbbrevB = "";






            if (citiesDictionary.ContainsKey(cityNameA))
            {
                List<CityInfo> listA = citiesDictionary[cityNameA];

                int selection = checkForMuilipleCities(listA);

                foreach (CityInfo cityInfo in listA)
                {
                    if (selection == 1 || selection == 0)
                    {

                        distanceALng = listA.ElementAt(0).Longitude;
                        distanceALat = listA.ElementAt(0).Latitude;
                        stateAbbrevA = listA.ElementAt(0).StateAbbrev;

                        break;
                    }
                    else if (selection == 2)
                    {

                        distanceALng = listA.ElementAt(1).Longitude;
                        distanceALat = listA.ElementAt(1).Latitude;
                        stateAbbrevA = listA.ElementAt(1).StateAbbrev;
                        break;
                    }


                }

            }


            if (citiesDictionary.ContainsKey(cityNameB))
            {
                List<CityInfo> listB = citiesDictionary[cityNameA];

                int selection = checkForMuilipleCities(listB);

                foreach (CityInfo cityInfo in listB)
                {
                    if (selection == 1 || selection == 0)
                    {

                        distanceBLng = listB.ElementAt(0).Longitude;
                        distanceBLat = listB.ElementAt(0).Latitude;
                        stateAbbrevB = listB.ElementAt(0).StateAbbrev;

                        break;
                    }
                    else if (selection == 2)
                    {

                        distanceBLng = listB.ElementAt(1).Longitude;
                        distanceBLat = listB.ElementAt(1).Latitude;
                        stateAbbrevB = listB.ElementAt(1).StateAbbrev;
                        break;
                    }


                }

            }


            double calulatedDistance = CalculateDistance(distanceALat, distanceALng, distanceBLat, distanceBLng);
            double roundedDistance = Math.Round(calulatedDistance, 1);

            Console.WriteLine($"The distance between {cityNameA}, {stateAbbrevA} and  {cityNameB}, {stateAbbrevB} is {roundedDistance} km ");
        }




        /// <summary>
        /// Uses calculates CalculateDistance() the distance between a city selected by the user and the state capital for the same state.
        /// </summary>
        /// <param name="cityName"></param>

        public void ReportDistanceFromCapital(string cityName)
        {
            string stateAbbrevA = "";
            string stateAbbrevB = "";
            string cityA = "";
            string cityB = "";


            double distanceALng = 0;
            double distanceALat = 0;
            double distanceBLng = 0;
            double distanceBLat = 0;



            if (citiesDictionary.ContainsKey(cityName))
            {
                List<CityInfo> list = citiesDictionary[cityName];
                int selection = checkForMuilipleCities(list);

                foreach (CityInfo cityInfo in list)
                {
                    if (selection == 1 || selection == 0)
                    {

                        distanceALng = list.ElementAt(0).Longitude;
                        distanceALat = list.ElementAt(0).Latitude;
                        stateAbbrevA = list.ElementAt(0).StateAbbrev;
                        cityA = list.ElementAt(0).Name;

                        break;
                    }
                    else if (selection == 2)
                    {
                        distanceALng = list.ElementAt(1).Longitude;
                        distanceALat = list.ElementAt(1).Latitude;
                        stateAbbrevA = list.ElementAt(1).StateAbbrev;
                        cityA = list.ElementAt(1).Name;
                        break;
                    }



                }

            }
            //foreach (CityInfo cityInfo in list)
            //{


            //    stateAbbrevA = cityInfo.StateAbbrev;
            //    distanceALng = cityInfo.Longitude;
            //    distanceALat = cityInfo.Latitude;
            //    cityA = cityInfo.Name;
            //    break;
            //}



            foreach (var cityList in citiesDictionary.Values)
            {
                CityInfo? capitalCity = cityList.FirstOrDefault(city => city.StateAbbrev == stateAbbrevA && city.Capital != "");

                if (capitalCity != null)
                {
                    distanceBLng = capitalCity.Longitude;
                    distanceBLat = capitalCity.Latitude;
                    stateAbbrevB = capitalCity.StateAbbrev;
                    cityB = capitalCity.Name;
                    break;
                }
            }

            double calulatedDistance = CalculateDistance(distanceALat, distanceALng, distanceBLat, distanceBLng);
            double roundedDistance = Math.Round(calulatedDistance, 1);

            Console.WriteLine($"The distance between {cityA}, {stateAbbrevA} and  {cityB}, {stateAbbrevB} is {roundedDistance} km ");
        }

        /// <summary>
        ///  Uses the name of the city and state to display the  city on a map
        /// </summary>
        /// <param name="city"></param>
        //Finds cir
        public void ShowCityOnMap(string city)
        {

            double distanceALng = 0;
            double distanceALat = 0;
            string state = "";


            if (citiesDictionary.ContainsKey(city))
            {
                List<CityInfo> list = citiesDictionary[city];

                foreach (CityInfo cityInfo in list)
                {
                        state = cityInfo.State;
                        distanceALng = cityInfo.Longitude;
                        distanceALat = cityInfo.Latitude;
                        break;
                    


                }
            }

            //Opens map based on the lon and lat values from the city and state 
            string url = $"https://www.latlong.net/c/?lat={distanceALat}&long={distanceALng}";
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    
                    FileName = url,
                    UseShellExecute = true
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening the map: {ex.Message}");
            }


        }



        /// <summary>
        /// Generates a list of all cities of the selected state  from the cities dictionary
        /// </summary>
        /// <param name="stateAbv"></param>

        public void ReportAllCities(string stateAbv)
        {
            string stateName = "";
            uint count  = 0;
            CityInfo? state = null;
            foreach (var cityList in citiesDictionary.Values)
            {
                state = cityList.FirstOrDefault(city => city.StateAbbrev == stateAbv && city.Capital != "");
          
                if (state != null)
                {
                    stateName = state!.State;
                    break;
                }

            }
            if (state == null)
            {
                Console.WriteLine($"{stateAbv} not found");
                return;
            }
            else
            {
                Console.WriteLine($"The following cities are in {stateName}");

                List<CityInfo?> allCities = new List<CityInfo?>();
                foreach (var cityList in citiesDictionary.Values)
                {
                    allCities = cityList.FindAll(c => c.StateAbbrev == stateAbv)!;
                    foreach (var city in allCities)
                    {
                        Console.WriteLine($"{city.Name}");
                        count++;
                    }
                }
                Console.WriteLine($"\n{count} cities found.");

            }
                
        }
        /// <summary>
        /// reports the largest city by population in the selected state
        /// </summary>
        /// <param name="stateAbv"></param>
        public void ReportLargestCity(string stateAbv)
        {
            string stateName = ""; 
            CityInfo? state =null;
            foreach (var cityList in citiesDictionary.Values)
            {
             state = cityList.FirstOrDefault(city => city.StateAbbrev == stateAbv);

                if (state != null)
                {
                    stateName = state!.State;
                    break;
                }

            }

            if (state == null)
            {
                Console.WriteLine($"{stateAbv} not found");
                return;
            }
            List<CityInfo?> allCities = new List<CityInfo?>();
            List<int> allPopulations = new();
            string cityName = "";
            int largestPop = 0;
            foreach (var cityList in citiesDictionary.Values)
            {
                allCities = cityList.FindAll(c => c.StateAbbrev == stateAbv)!;
                foreach (var city in allCities)
                {
                    allPopulations.Add(city!.Population);
                }

            }

            largestPop = allPopulations.Max();
            foreach (var cityList in citiesDictionary.Values)
            {
                allCities = cityList.FindAll(c => c.StateAbbrev == stateAbv)!;
                foreach (var city in allCities)
                {

                    if (city!.Population == largestPop)
                    {
                        cityName = city.Name;
                        break;
                    }
                }
            }

            if (state == null)
            {
                Console.WriteLine($"{stateAbv} not found");
                return;
            }
            else
            {
                Console.WriteLine($"The largest city in {stateName} is {cityName} with a population of {largestPop.ToString("N0")} ");

            }
        }

        /// <summary>
        /// Reports the smallest city by population in the  selected state.
        /// </summary>
        /// <param name="stateAbv"></param>
        public void ReportSmallestCity(string stateAbv)
        {
            string stateName = "";
            CityInfo? state = null;
            foreach (var cityList in citiesDictionary.Values)
            {
                state = cityList.FirstOrDefault(city => city.StateAbbrev == stateAbv);

                if (state != null)
                {
                    stateName = state!.State;
                    break;
                }

            }
            if (state == null)
            {
                Console.WriteLine($"{stateAbv} not found");
                return;
            }

            List<CityInfo?> allCities = new List<CityInfo?>();
            List<int> allPopulations = new();
            string cityName = "";
            int smallestPop = 0;
            foreach (var cityList in citiesDictionary.Values)
            {
                allCities = cityList.FindAll(c => c.StateAbbrev == stateAbv)!;
                foreach (var city in allCities)
                {
                    allPopulations.Add(city!.Population);
                }

            }

            smallestPop = allPopulations.Min();
            foreach (var cityList in citiesDictionary.Values)
            {
                allCities = cityList.FindAll(c => c.StateAbbrev == stateAbv)!;
                foreach (var city in allCities)
                {

                    if (city!.Population == smallestPop)
                    {
                        cityName = city.Name;
                        break;
                    }
                }
            }

            if (state == null)
            {
                Console.WriteLine($"{stateAbv} not found");
                return;
            }
            else
            {
                Console.WriteLine($"The smallest city in {stateName} is {cityName} with a population of {smallestPop.ToString("N0")} ");
            }
          
        }



        /// <summary>
        /// Reports the capital of the selected state along  with its latitude and longitude.
        /// </summary>
        /// <param name="stateAbv"></param>
        public void ReportCapital(string stateAbv)
        {
            string stateName = "";
            string capCity = "";
            double lon = 0, lat = 0;
            CityInfo? state = null;
            foreach (var cityList in citiesDictionary.Values)
            {
                  state = cityList.FirstOrDefault(city => city.StateAbbrev == stateAbv);

               
                if (state != null)
                {
                    stateName = state!.State;
                    capCity = state!.Name;
                    lon = state!.Longitude;
                    lat = state!.Latitude;
                   
                    break;
                }
                

            }
            if(state == null) {
                Console.WriteLine($"{stateAbv} not found");
                return;
            }
            else
            {
                Console.WriteLine($"The capital city of {stateName} is {capCity}.\r\nIt's coordinates are {lat} degrees lattitude, {lon} degrees longitude.");
            }
          

         
        }


        /// <summary>
        /// Reports the sum of the populations of all cities of the selected state that are stored in the cities dictionary
        /// </summary>
        /// <param name="stateAbv"></param>
        public void ReportStatePopulation(string stateAbv)
        {
            string stateName = "";
            CityInfo? state = null;
            foreach (var cityList in citiesDictionary.Values)
            {
                state = cityList.FirstOrDefault(city => city.StateAbbrev == stateAbv);

                if (state != null)
                {
                    stateName = state!.State;
                    break;
                }

            }
            if (state == null)
            {
                Console.WriteLine($"{stateAbv} not found");
                return;
            }

            List<CityInfo?> allCities = new List<CityInfo?>();
            int allPopulationsTotal = 0;
            uint count = 0;
            foreach (var cityList in citiesDictionary.Values)
            {
                allCities = cityList.FindAll(c => c.StateAbbrev == stateAbv)!;
                foreach (var city in allCities)
                {
                    allPopulationsTotal += city!.Population;
                    count++;
                }

            }

          

            if (state == null)
            {
                Console.WriteLine($"{stateAbv} not found");
                return;
            }
            else
            {
                Console.WriteLine($"The total population of the {count} cities in {stateName} is {allPopulationsTotal.ToString("N0")}. ");
            }

        }














































    }



    /*
   // Define Statistics class
       Class Statistics:
           Property:
               citiesDictionary (Dictionary<CityName, CityInfo>)
           Constructor(fileName, fileType):
               citiesDictionary = DataModeler.ParseFile(fileName, fileType)
           Methods:
               ReportCityInformation(cityName):
                   // Display all details of the city

               ComparePopulationDensity(city1, city2):
                   // Compare and display density of both cities

               ReportDistanceBetweenCities(city1, city2):
                   // Calculate and display distance using latitude & longitude

               ReportDistanceFromCapital(cityName):
                   // Calculate distance from city to its state capital

               ShowCityOnMap(cityName, stateAbbrev):
                   // Open web browser with Google Maps URL

               ReportAllCities(stateAbbrev):
                   // List all cities in the state

               ReportLargestCity(stateAbbrev):
                   // Find and display largest city in the state

               ReportSmallestCity(stateAbbrev):
                   // Find and display smallest city in the state

               ReportCapital(stateAbbrev):
                   // Display capital city details

               ReportStatePopulation(stateAbbrev):
                   // Sum and display population of all cities in state


       */



}