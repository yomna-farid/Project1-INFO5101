﻿/*
 * Name: Joy Owoeyeb
 */

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Project1_INFO5101
{
    public class Statistics
    {
        private Dictionary<string, List<CityInfo>> citiesDictionary;

        public Statistics(string fileName, int filetype)
        {
            DataModeler dataModeler = new DataModeler();
            dataModeler.ParseFile(fileName, filetype);
            citiesDictionary = dataModeler.CityDictionary;
        }

        public void ReportCityInformation(string cityName)
        {
            if (citiesDictionary.ContainsKey(cityName))
            {
                List<CityInfo> list = citiesDictionary[cityName];
                int count = 1;
                foreach (CityInfo cityInfo in list)
                {

                    Console.WriteLine($"{count}. {cityInfo.Name}, {cityInfo.StateAbbrev}");

                    Console.WriteLine("{0,-20} {1,-15}", "State:", cityInfo.State);
                    Console.WriteLine("{0,-20} {1,-15}", "Population:", cityInfo.Population.ToString("N0"));
                    Console.WriteLine("{0,-20} {1,-15}", "Pop. Density:", cityInfo.Density);
                    Console.WriteLine("{0,-20} {1,-15}", "Longitude:", cityInfo.Longitude);
                    Console.WriteLine("{0,-20} {1,-15}", "Latitude:", cityInfo.Latitude);
                    Console.WriteLine("{0,-20} {1,-15}", "Time Zone:", cityInfo.TimeZone);
                    Console.WriteLine("{0,-20} {1,-15}", "Capital:", cityInfo.Capital.Equals("") ? "No" : cityInfo.Capital);

                    count++;
                }
            }
        }

        public void ComparePopulationDensity(string cityNameA, string cityNameB)
        {

            double densityA=0, densityB = 0;
            string cityA = "", cityB = "", stateAbbrevA = "",  stateAbbrevB = "";

            if (citiesDictionary.ContainsKey(cityNameA))
            {
                List<CityInfo> listA = citiesDictionary[cityNameA];
                foreach (CityInfo cityInfo in listA)
                {
                    cityA = cityInfo.Name;
                    stateAbbrevA = cityInfo.StateAbbrev;
                    densityA = cityInfo.Density;


                }

                Console.WriteLine($"{cityA}, {stateAbbrevA}  has a population density of {densityA.ToString("N0")} people per sq. km");
            }
            if (citiesDictionary.ContainsKey(cityNameB))
            {
                List<CityInfo> listB = citiesDictionary[cityNameB];
                foreach (CityInfo cityInfo in listB)
                {
                    cityB = cityInfo.Name;
                    stateAbbrevB = cityInfo.StateAbbrev;
                    densityB = cityInfo.Density;

                }

                Console.WriteLine($"{cityB}, {stateAbbrevB}  has a population density of {densityB.ToString("N0")} people per sq. km");
            }
           
           string nameAndState = densityA > densityB ? cityNameA + ", " + stateAbbrevA : cityNameB + ", " + stateAbbrevB;
            Console.WriteLine($"\n{nameAndState} has the higher population density");
            
        }

        // Haversine Formula to calculate distance between two latitude/longitude points https://en.wikipedia.org/wiki/Haversine_formula
        static double HaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Radius of Earth in km
            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }
        static double DegreesToRadians(double degrees) => degrees * Math.PI / 180;

        public void ReportDistanceBetweenCities(string cityNameA, string cityNameB)
        {
            double distanceALng = 0;
            double distanceALat = 0;
            double distanceBLng = 0;
            double distanceBLat = 0;
            string stateAbbrevA= "";
            string stateAbbrevB= "";
            if (citiesDictionary.ContainsKey(cityNameA))
            {
                List<CityInfo> list = citiesDictionary[cityNameA];

                foreach (CityInfo cityInfo in list)
                {
                    distanceALng = cityInfo.Longitude;
                    distanceALat = cityInfo.Latitude;
                    stateAbbrevA = cityInfo.StateAbbrev;
                }
            }    
            
            
            if (citiesDictionary.ContainsKey(cityNameB))
            {
                List<CityInfo> list = citiesDictionary[cityNameB];

                foreach (CityInfo cityInfo in list)
                {
                    distanceBLng = cityInfo.Longitude;
                    distanceBLat = cityInfo.Latitude;
                    stateAbbrevB = cityInfo.StateAbbrev;

                }
            }


          double calulatedDistance =  HaversineDistance(distanceALat, distanceALng, distanceBLat, distanceBLng);
            double roundedDistance = Math.Round(calulatedDistance, 1);

            Console.WriteLine($"The distance between {cityNameA}, {stateAbbrevA} and  {cityNameB}, {stateAbbrevB} is {roundedDistance} km ");
        }

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

                foreach (CityInfo cityInfo in list)
                {
                    stateAbbrevA = cityInfo.StateAbbrev;
                    distanceALng = cityInfo.Longitude;
                    distanceALat = cityInfo.Latitude;
                    cityA = cityInfo.Name;
                    break;
                }

            }

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



            double calulatedDistance = HaversineDistance(distanceALat, distanceALng, distanceBLat, distanceBLng);
            double roundedDistance = Math.Round(calulatedDistance, 1);

            Console.WriteLine($"The distance between {cityA}, {stateAbbrevA} and  {cityB}, {stateAbbrevB} is {roundedDistance} km ");
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